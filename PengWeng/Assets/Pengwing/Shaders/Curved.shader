﻿// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'


Shader "Custom/Curved" {
	    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Illumi ("Illumi Color", Color) = (1,1,1,1)
        Intensity ("Intensity", Float) = 1
        
    }
 
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200
 
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert addshadow
 
        // Global Shader values
        uniform float2 _BendAmount;
        uniform float3 _BendOrigin;
        uniform float _BendFalloff;
 
        sampler2D _MainTex;
        fixed4 _Color;
        fixed4 _Illumi;
        float Intensity;
 
        struct Input
        {
              float2 uv_MainTex;
        };
 
        float4 Curve(float4 v)
        {
              //HACK: Considerably reduce amount of Bend
              _BendAmount *= .0001;
 
              float4 world = mul(unity_ObjectToWorld, v);
 
              float dist = length(world.xz-_BendOrigin.xz);
 
              dist = max(0, dist-_BendFalloff);
 
              // Distance squared
              dist = dist*dist;
 
              world.xy += dist*_BendAmount;
              return mul(unity_WorldToObject, world);
        }
 
        void vert(inout appdata_full v)
        {
              v.vertex = Curve(v.vertex);
        }
 
        void surf(Input IN, inout SurfaceOutput o)
        {
              fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
              o.Emission = _Illumi * Intensity;
              o.Albedo = c.rgb;
              o.Alpha = c.a;
        }
 
        ENDCG
    }
 
      Fallback "Mobile/Diffuse"
}
