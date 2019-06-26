// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "AmplifyCurveTest"
{
	Properties
	{
		_Float0("Float 0", Float) = 0
		_CurveSphereTip("CurveSphereTip", Vector) = (0,0,0,0)
		_Vector0("Vector 0", Vector) = (0,0,0,0)
		_Float1("Float 1", Float) = 0
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_Float3("Float 3", Float) = 0
		_Float2("Float 2", Float) = 0
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 2.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float3 _CurveSphereTip;
		uniform float _Float0;
		uniform float _Float1;
		uniform float3 _Vector0;
		uniform sampler2D _TextureSample1;
		uniform float4 _TextureSample1_ST;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float _Float2;
		uniform float _Float3;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float4 transform10_g1 = mul(unity_ObjectToWorld,float4( ase_vertex3Pos , 0.0 ));
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float4 transform12_g1 = mul(unity_WorldToObject,( transform10_g1 + float4( ( pow( distance( ase_worldPos , _CurveSphereTip ) , _Float0 ) * _Float1 * _Vector0 ) , 0.0 ) ));
			v.vertex.xyz = transform12_g1.xyz;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample1 = i.uv_texcoord * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
			o.Normal = tex2D( _TextureSample1, uv_TextureSample1 ).rgb;
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			o.Albedo = tex2D( _TextureSample0, uv_TextureSample0 ).rgb;
			float3 temp_cast_2 = (_Float2).xxx;
			o.Emission = temp_cast_2;
			o.Smoothness = _Float3;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
27;540;883;489;-1347.393;1006.248;1.043977;False;False
Node;AmplifyShaderEditor.RangedFloatNode;69;1939.152,-591.927;Float;False;Property;_Float3;Float 3;6;0;Create;True;0;0;False;0;0;2.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;68;1810.743,-647.2581;Float;False;Property;_Float2;Float 2;7;0;Create;True;0;0;False;0;0;0.06;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;33;1828.682,-491.3623;Float;False;WorldBend;0;;1;4efd8027f333f4ec3b11aaf7bc0866b1;0;0;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;66;1706.345,-999.9844;Float;True;Property;_TextureSample0;Texture Sample 0;8;0;Create;True;0;0;False;0;None;181b72417a7b16e4b9ed81ae0a97737d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;70;1453.703,-808.9364;Float;True;Property;_TextureSample1;Texture Sample 1;5;0;Create;True;0;0;False;0;None;9a4a55d8d2e54394d97426434477cdcf;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2074.979,-782.4418;Float;False;True;0;Float;ASEMaterialInspector;0;0;Standard;AmplifyCurveTest;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Absolute;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;66;0
WireConnection;0;1;70;0
WireConnection;0;2;68;0
WireConnection;0;4;69;0
WireConnection;0;11;33;0
ASEEND*/
//CHKSM=655DFD9E332E5A8497AF660BA03D93A8258B81D6