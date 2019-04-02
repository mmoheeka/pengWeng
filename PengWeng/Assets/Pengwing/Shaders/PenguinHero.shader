Shader "Custom/PenguinHero" 
{
	Properties 
    {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}
        _Amount ("Extrusion Amount", Range(-1,1)) = 0.5

	}
	SubShader 
    {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
        
        #pragma surface surf ToonRamp vertex:vert
        //#pragma target 3.0

		sampler2D _Ramp;
        

        
        #pragma lighting ToonRamp exclude_path:prepass
        
        half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half atten)
        {
           
           #ifndef USING_DIRECTIONAL_LIGHT
           lightDir = normalize(lightDir);
           #endif
           
           half NdotL = dot (s.Normal, lightDir);
           half diff = NdotL * 0.5 + 0.5;
           half3 ramp = tex2D (_Ramp, float(diff)).rgb;
           half4 c;
           c.rgb = s.Albedo * _LightColor0.rgb * ramp * atten;
           c.a = s.Alpha;
           return c;
        }

		struct Input 
        {
			float2 uv_MainTex : TEXCOORD0;
		};
        
        
        float _Amount;
        void vert (inout appdata_full v) 
        {
        
          v.vertex.xyz += v.normal * _Amount;
          
        }
        
        
        
        sampler2D _MainTex;
        //half _Glossiness;
		//half _Metallic;
		fixed4 _Color;
        

		void surf (Input IN, inout SurfaceOutput o) 
        {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
