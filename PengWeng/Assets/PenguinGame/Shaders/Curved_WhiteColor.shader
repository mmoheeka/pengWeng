// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'


Shader "Custom/Curved_WhiteColor" {
	    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Illumi ("Illumi Color", Color) = (1,1,1,1)
        _Intensity ("Intensity", Float) = 1
        
        //_Bump ("Bump", 2D) = "bump" {}
        _SnowAngle("Angle of snow buildup", Vector) = (0,1,0)
        _SnowSize("Snow Amount", Range(-2,2)) = 1
        _Height("Snow Height", Range(0,0.2)) = 0.1
        
        
        _Snow ("Snow Level", Range(0,1) ) = 0
        _SnowColor ("Snow Color", Color) = (1.0,1.0,1.0,1.0)
        _SnowDirection ("Snow Direction", Vector) = (0,1,0)
        _SnowDepth ("Snow Depth", Range(0,0.2)) = 0.1

    }
 
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200
        Cull Off
        
        
        
 
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert addshadow
 
        // Global Shader values
        uniform float2 _BendAmount;
        uniform float3 _BendOrigin;
        uniform float _BendFalloff;
 
        sampler2D _MainTex;
        fixed4 _Color;
        fixed4 _Illumi;
        float _Intensity;
        
        float4 _SnowAngle;
        float _SnowSize;
        float _Height;
        
        //sampler2D _Bump;
        float _Snow;
        float4 _SnowColor;
        float4 _SnowDirection;
        float _SnowDepth;
        
        
 
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
 
 
 
 
        void vert(inout appdata_full v, out Input o)
        {            
              UNITY_INITIALIZE_OUTPUT(Input, o);
              
              //o.lightDir = WorldSpaceLightDir(v.vertex); // light direction for snow ramp
              
              float4 snowC = mul(_SnowAngle , unity_ObjectToWorld); // snow direction convertion to worldspace
              
              if (dot(v.normal, snowC.xyz) >= _SnowSize ) 
                {
                    v.vertex.xyz += v.normal * _Height;// scale vertices along normal
                }
                
                
              //v.vertex = Curve(v.vertex);
        }
 
        void surf(Input IN, inout SurfaceOutput o)
        {
              fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
              
              //o.Normal = UnpackNormal (tex2D (_Bump, IN.uv_Bump));
              
              if(dot(WorldNormalVector(IN, o.Normal), _SnowDirection.xyz)>=lerp(1,-1,_Snow))
                {
                    o.Albedo = _SnowColor.rgb;
                }
                else {
                    o.Albedo = c.rgb;
                }
                o.Alpha = 1;
              
              
              //o.Emission = _Illumi * _Intensity;
              //o.Albedo = c.rgb;
              //o.Alpha = c.a;
              
        }
 
        ENDCG
    }
 
      Fallback "Mobile/Diffuse"
}
