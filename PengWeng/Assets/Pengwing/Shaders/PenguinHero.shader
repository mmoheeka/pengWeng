Shader "Custom/PenguinHero" 
{
	Properties 
    {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}
        
        _Value ("Value", Range(0, 1)) = 0.5
       
	}
    

    
	SubShader 
    {

        
		Tags { "RenderType"="Opaque"}

    
        
		LOD 200
        
        CGPROGRAM
        #pragma surface surf ToonRamp 
        

		sampler2D _Ramp;
        sampler2D _MainTex;
        fixed4 _Color;
        float _Value;
        
        
        struct Input 
        {
           float4 vertex : POSITION;
           float3 normal : NORMAL;
           float2 uv_MainTex : TEXCOORD0;
        };
        
             
        
        half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half atten)
        {
           
           //#ifndef USING_DIRECTIONAL_LIGHT
           lightDir = normalize(lightDir);
           //#endif
           
           half NdotL = dot (s.Normal, lightDir);
           half diff = NdotL * _Value + _Value;
           half3 ramp = tex2D (_Ramp, float(diff)).rgb;
           half4 c;
           c.rgb = s.Albedo * _LightColor0.rgb * ramp * atten;
           c.a = s.Alpha;
           return c;
        }
        
       
        
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
