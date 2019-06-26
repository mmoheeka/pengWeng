// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "RockShader"
{
	Properties
	{
		_BendOrigin("BendOrigin", Vector) = (0,0,0,0)
		_BendDirection("BendDirection", Vector) = (0,0,0,0)
		_BendAmount("BendAmount", Range( 0 , 5)) = 0
		_BendMultiplier("BendMultiplier", Float) = 0
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Color0("Color 0", Color) = (0,0,0,0)
		_Color1("Color 1", Color) = (0,0,0,0)
		_Float1("Float 1", Float) = 0
		_Float2("Float 2", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float3 _BendOrigin;
		uniform float _BendAmount;
		uniform float _BendMultiplier;
		uniform float3 _BendDirection;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float4 _Color0;
		uniform float4 _Color1;
		uniform float _Float1;
		uniform float _Float2;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float4 transform10_g2 = mul(unity_ObjectToWorld,float4( ase_vertex3Pos , 0.0 ));
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float4 transform12_g2 = mul(unity_WorldToObject,( transform10_g2 + float4( ( pow( distance( ase_worldPos , _BendOrigin ) , _BendAmount ) * _BendMultiplier * _BendDirection ) , 0.0 ) ));
			v.vertex.xyz = transform12_g2.xyz;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			o.Albedo = ( tex2D( _TextureSample0, uv_TextureSample0 ) * _Color0 ).rgb;
			o.Emission = _Color1.rgb;
			o.Metallic = _Float1;
			o.Smoothness = _Float2;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
25;878;1322;700;1046.308;54.31527;1.297042;True;False
Node;AmplifyShaderEditor.SamplerNode;1;-507.9115,-108.8799;Float;True;Property;_TextureSample0;Texture Sample 0;5;0;Create;True;0;0;False;0;None;181b72417a7b16e4b9ed81ae0a97737d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;2;-409.3991,93.7609;Float;False;Property;_Color0;Color 0;6;0;Create;True;0;0;False;0;0,0,0,0;0.3559096,0.4623444,0.5849056,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;44;-39.60452,490.3722;Float;False;Property;_Float2;Float 2;9;0;Create;True;0;0;False;0;0;1.56;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;80;-75.2475,606.4802;Float;False;WorldBend;0;;2;4efd8027f333f4ec3b11aaf7bc0866b1;0;0;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;43;-43.36817,398.8159;Float;False;Property;_Float1;Float 1;8;0;Create;True;0;0;False;0;0;-0.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-37.81316,89.59519;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;37;-48.03106,223.0645;Float;False;Property;_Color1;Color 1;7;0;Create;True;0;0;False;0;0,0,0,0;0.0245639,0.04951604,0.0754717,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;297.6578,289.9861;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;RockShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Absolute;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;45;0;1;0
WireConnection;45;1;2;0
WireConnection;0;0;45;0
WireConnection;0;2;37;0
WireConnection;0;3;43;0
WireConnection;0;4;44;0
WireConnection;0;11;80;0
ASEEND*/
//CHKSM=7B1E84FB9D9F2E92391941A4556DF0D4993E562E