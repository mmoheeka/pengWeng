Shader "Custom/CrystalNew" 
{
    Properties {
		_Normal("Normal Map", 2D) = "bump" {}
        _Color ("Color", Color) = (1,1,1,1)

        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _RefPow ("Refraction Distortion", Range(0,250)) = 15.0

		_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
      	_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
		_Alpha ("Alpha", Range(0,1)) = 0.5
		_RefractionOffset ("Refraction Offset", Range(0,10)) = 0.5
		

    }
    SubShader {
        GrabPass {"_Refraction"}
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200
     
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Gem fullforwardshadows vertex:vert alpha
 
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.5

        // Global Shader values
        uniform float2 _BendAmount;
        uniform float3 _BendOrigin;
        uniform float _BendFalloff;
 
        sampler2D _Refraction;
        float4 _Refraction_TexelSize;
		sampler2D _Normal;

        float3 _FresnelColor;
        float _FresnelExponent;
        half3 _Emission;
 
        struct Input 
		{
            float4 screenPos;
            float3 worldRefl; INTERNAL_DATA
			float2 uv_Normal;
            float3 worldNormal;
			float3 viewDir;
			float3 lightDir;
        };
 
        half _Glossiness;
        half _RefPow;
        fixed4 _Color;
		
		float4 _RimColor;
      	float _RimPower;
		float _Alpha;
		float _RefractionOffset;
 
        half4 LightingGem (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) 
		{
            half3 h = normalize (viewDir + lightDir);
            half nh = max (0, dot (normalize (s.Normal), h));
            half spec = pow (nh, (pow (s.Specular, 2) * 256.0) + 0.1) * s.Specular * 4;
 
            half4 c;
            c.rgb = spec * _LightColor0 * atten;
            c.a = _Alpha;
            return c;
        }

        float4 Curve(float4 v)
        {
              //HACK: Considerably reduce amount of Bend
              _BendAmount *= .0001;
              float4 world = mul(unity_ObjectToWorld, v);
              float dist = length(world.xz - _BendOrigin.xz);
              dist = max(0, dist - _BendFalloff);
              // Distance squared
              dist = dist*dist;
              world.xy += dist*_BendAmount;
              return mul(unity_WorldToObject, world);
        }

		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.lightDir = WorldSpaceLightDir(v.vertex); // get the worldspace lighting direction 
            v.vertex = Curve(v.vertex);
		}

 
        void surf (Input IN, inout SurfaceOutput o) 
		{
			float3 normal = UnpackNormal(tex2D(_Normal, IN.uv_Normal));
			o.Normal = normal;

            half2 offset = o.Normal * _RefPow * _Refraction_TexelSize.xy;
            IN.screenPos.xy /= IN.screenPos.w;
            IN.screenPos.xy = IN.screenPos.z * (offset * _RefractionOffset) + IN.screenPos.xy;
            float3 refract = (tex2D (_Refraction, IN.screenPos.xy) + DecodeHDR (UNITY_SAMPLE_TEXCUBE (unity_SpecCube0, IN.worldRefl), unity_SpecCube0_HDR)) * 0.5 * _Color;

			half rim = 1 - saturate(dot (normalize(IN.viewDir), o.Normal));
          	float3 rimPower = _RimColor.rgb * pow (rim, _RimPower);

			o.Emission += rimPower + refract;

			o.Specular = _Glossiness;
            
        }
        ENDCG
    }
    FallBack "Diffuse"
}
