// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Raygeas/Water"
{
	Properties
	{
		[NoScaleOffset][Normal]_WavesNormal("Waves Normal", 2D) = "white" {}
		_Color1("Color 1", Color) = (0,0,0,0)
		_Color2("Color 2", Color) = (0,0,0,0)
		_Opacity("Opacity", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_WavesTile("Waves Tile", Float) = 1
		_WavesSpeed("Waves Speed", Range( 0 , 1)) = 0
		_WavesNormalIntensity("Waves Normal Intensity", Range( 0 , 2)) = 1
		_FoamContrast("Foam Contrast", Range( 0 , 1)) = 0
		_FoamDistance("Foam Distance", Range( 0 , 5)) = 0
		_FoamDensity("Foam Density", Range( 0.1 , 1)) = 0.5
		_DepthDistance("Depth Distance", Range( 0 , 1)) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:premul keepalpha noshadow exclude_path:deferred 
		struct Input
		{
			float3 worldPos;
			float4 screenPos;
		};

		uniform sampler2D _WavesNormal;
		uniform float _WavesNormalIntensity;
		uniform float _WavesSpeed;
		uniform float _WavesTile;
		uniform float4 _Color2;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _DepthDistance;
		uniform float4 _Color1;
		uniform float _FoamDensity;
		uniform float _FoamContrast;
		uniform float _FoamDistance;
		uniform float _Smoothness;
		uniform float _Opacity;


		float2 voronoihash61( float2 p )
		{
			
			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
			return frac( sin( p ) *43758.5453);
		}


		float voronoi61( float2 v, float time, inout float2 id, float smoothness )
		{
			float2 n = floor( v );
			float2 f = frac( v );
			float F1 = 8.0;
			float F2 = 8.0; float2 mr = 0; float2 mg = 0;
			for ( int j = -1; j <= 1; j++ )
			{
				for ( int i = -1; i <= 1; i++ )
			 	{
			 		float2 g = float2( i, j );
			 		float2 o = voronoihash61( n + g );
					o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = g - f + o;
					float d = 0.5 * dot( r, r );
			 //		if( d<F1 ) {
			 //			F2 = F1;
			 			float h = smoothstep(0.0, 1.0, 0.5 + 0.5 * (F1 - d) / smoothness); F1 = lerp(F1, d, h) - smoothness * h * (1.0 - h);mg = g; mr = r; id = o;
			 //		} else if( d<F2 ) {
			 //			F2 = d;
			 //		}
			 	}
			}
			return F1;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float temp_output_132_0 = ( _WavesSpeed * 0.1 );
			float mulTime43 = _Time.y * temp_output_132_0;
			float2 _Vector0 = float2(1,1);
			float3 ase_worldPos = i.worldPos;
			float2 appendResult106 = (float2(ase_worldPos.x , ase_worldPos.z));
			float2 WorldSpaceTile68 = ( appendResult106 * _WavesTile );
			float2 panner112 = ( mulTime43 * _Vector0 + WorldSpaceTile68);
			float2 panner114 = ( ( 1.0 - mulTime43 ) * _Vector0 + WorldSpaceTile68);
			float3 Normal89 = ( UnpackScaleNormal( tex2D( _WavesNormal, panner112 ), _WavesNormalIntensity ) + UnpackScaleNormal( tex2D( _WavesNormal, panner114 ), _WavesNormalIntensity ) );
			o.Normal = Normal89;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float clampResult140 = clamp( ( _DepthDistance * 3 ) , 0.1 , 3.0 );
			float screenDepth83 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth83 = abs( ( screenDepth83 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( clampResult140 ) );
			float4 Depth158 = saturate( ( saturate( ( _Color2 * distanceDepth83 ) ) + saturate( ( ( 1.0 - distanceDepth83 ) * _Color1 ) ) ) );
			float mulTime3 = _Time.y * ( temp_output_132_0 * 100 );
			float time61 = mulTime3;
			float voronoiSmooth0 = ( 1.0 - _FoamDensity );
			float2 coords61 = WorldSpaceTile68 * 50.0;
			float2 id61 = 0;
			float voroi61 = voronoi61( coords61, time61,id61, voronoiSmooth0 );
			float clampResult138 = clamp( _FoamContrast , 0.0 , 0.95 );
			float screenDepth17 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth17 = abs( ( screenDepth17 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _FoamDistance ) );
			float Foam183 = saturate( ( pow( saturate( voroi61 ) , ( 1.0 - clampResult138 ) ) + ( 1.0 - distanceDepth17 ) ) );
			o.Albedo = ( Depth158 + Foam183 ).rgb;
			o.Smoothness = _Smoothness;
			o.Alpha = _Opacity;
		}

		ENDCG
	}
}
/*ASEBEGIN
Version=18000
272;73;1775;903;7376.146;1933.17;4.000669;True;False
Node;AmplifyShaderEditor.CommentaryNode;95;-4843.347,763.7183;Inherit;False;970.8306;441.1572;;5;68;107;106;100;66;World Space Tile;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;66;-4780.381,860.8544;Inherit;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.CommentaryNode;157;-4844.658,-37.79259;Inherit;False;2232.14;734.4948;;14;158;229;227;85;83;140;139;84;211;236;86;235;228;230;Depth;1,1,1,1;0;0
Node;AmplifyShaderEditor.DynamicAppendNode;106;-4509.767,884.9265;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;84;-4807.368,323.9843;Inherit;False;Property;_DepthDistance;Depth Distance;11;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;58;-5550.533,-993.7839;Inherit;False;Property;_WavesSpeed;Waves Speed;6;0;Create;True;0;0;False;0;0;0.1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;100;-4528.591,1060.102;Inherit;False;Property;_WavesTile;Waves Tile;5;0;Create;True;0;0;False;0;1;0.15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleNode;132;-5237.797,-987.9325;Inherit;False;0.1;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;107;-4312.72,962.6305;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScaleNode;139;-4492.897,329.3033;Inherit;False;3;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;179;-4846.875,-1463.306;Inherit;False;2068.099;656.4753;;18;183;188;186;19;6;64;17;136;18;61;138;135;3;7;5;130;131;63;Foam;1,1,1,1;0;0
Node;AmplifyShaderEditor.ScaleNode;131;-4702.088,-1301.849;Inherit;False;100;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;68;-4134.862,958.0616;Inherit;False;WorldSpaceTile;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ClampOpNode;140;-4282.897,327.3033;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0.1;False;2;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;63;-4793.106,-1121.849;Inherit;False;Property;_FoamDensity;Foam Density;10;0;Create;True;0;0;False;0;0.5;0.392;0.1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;3;-4513.123,-1301.942;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-4281.874,-1121.359;Inherit;False;Property;_FoamContrast;Foam Contrast;8;0;Create;True;0;0;False;0;0;0.7;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;83;-4070.467,302.2844;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;130;-4546.644,-1387.98;Inherit;False;68;WorldSpaceTile;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-4512.252,-1214.334;Inherit;False;Constant;_VoronoiScale;Voronoi Scale;7;0;Create;True;0;0;False;0;50;50;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;135;-4494.521,-1117.147;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;227;-3812.819,491.8506;Inherit;False;Property;_Color1;Color 1;1;0;Create;True;0;0;False;0;0,0,0,0;0,0.310371,0.6886792,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;18;-4159.086,-947.4217;Inherit;False;Property;_FoamDistance;Foam Distance;9;0;Create;True;0;0;False;0;0;0.7;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.VoronoiNode;61;-4247.94,-1289.851;Inherit;False;0;0;1;0;1;False;1;False;True;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;2;FLOAT;0;FLOAT;1
Node;AmplifyShaderEditor.ClampOpNode;138;-3956.907,-1116.104;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.95;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;85;-3812.418,56.36393;Inherit;False;Property;_Color2;Color 2;2;0;Create;True;0;0;False;0;0,0,0,0;0,0.7075471,0.7008876,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;94;-4849.317,-741.4441;Inherit;False;2219.204;635.6195;;16;89;51;46;38;112;114;88;129;128;53;127;116;101;117;43;113;Normal;1,1,1,1;0;0
Node;AmplifyShaderEditor.OneMinusNode;229;-3759.916,397.9956;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;136;-3761.058,-1115.657;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;17;-3845.474,-967.9393;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;64;-3746.012,-1290.282;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;43;-4790.86,-565.7832;Inherit;False;1;0;FLOAT;0.01;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;86;-3552.866,131.2841;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;228;-3550.141,440.9694;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector2Node;113;-4569.425,-456.7563;Inherit;False;Constant;_Vector0;Vector 0;15;0;Create;True;0;0;False;0;1,1;1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.OneMinusNode;53;-4566.438,-250.0213;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;19;-3574.874,-968.2001;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;117;-4388.332,-353.4807;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;6;-3572.226,-1219.038;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;101;-4360.976,-458.9648;Inherit;False;68;WorldSpaceTile;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SaturateNode;236;-3365.304,169.7426;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;235;-3363.304,389.7425;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;127;-4095.972,-422.4999;Inherit;False;Property;_WavesNormalIntensity;Waves Normal Intensity;7;0;Create;True;0;0;False;0;1;0.15;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;116;-4387.03,-496.4804;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;186;-3371.75,-1105.804;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;129;-3822.972,-527.4999;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;230;-3172.141,257.9694;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TexturePropertyNode;88;-3770.253,-469.6925;Inherit;True;Property;_WavesNormal;Waves Normal;0;2;[NoScaleOffset];[Normal];Create;True;0;0;False;0;None;58a059b07b093a745b47c2191525ddce;True;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.WireNode;128;-3822.972,-252.4999;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;112;-4087.049,-613.376;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;114;-4085.262,-297.6844;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SaturateNode;188;-3187.473,-1107.528;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;211;-3009.974,257.9755;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;38;-3437.942,-644.7739;Inherit;True;Property;_TextureSample0;Texture Sample 0;8;1;[Normal];Create;True;0;0;False;0;-1;None;58a059b07b093a745b47c2191525ddce;True;0;True;bump;Auto;True;Instance;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;46;-3435.147,-328.2323;Inherit;True;Property;_TextureSample1;Texture Sample 1;9;1;[Normal];Create;True;0;0;False;0;-1;None;58a059b07b093a745b47c2191525ddce;True;0;True;bump;Auto;True;Instance;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;183;-3005.991,-1112.722;Inherit;False;Foam;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;51;-3071.296,-480.7129;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;158;-2825.167,253.1071;Inherit;False;Depth;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;209;-2476.831,-711.9362;Inherit;False;158;Depth;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;89;-2874.999,-485.6454;Inherit;False;Normal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;191;-2476.194,-604.3131;Inherit;False;183;Foam;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;-2244.937,-675.1191;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;103;-2258.891,-508.4796;Inherit;False;89;Normal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-2363.41,-414.5797;Inherit;False;Property;_Smoothness;Smoothness;4;0;Create;True;0;0;False;0;0;0.9;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;194;-2364.162,-222.7081;Inherit;False;Property;_Opacity;Opacity;3;0;Create;True;0;0;False;0;0;0.8;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-2007.479,-565.3766;Float;False;True;-1;2;;0;0;Standard;Raygeas/Water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Premultiply;0.5;True;False;0;False;Transparent;;Transparent;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;3;1;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;106;0;66;1
WireConnection;106;1;66;3
WireConnection;132;0;58;0
WireConnection;107;0;106;0
WireConnection;107;1;100;0
WireConnection;139;0;84;0
WireConnection;131;0;132;0
WireConnection;68;0;107;0
WireConnection;140;0;139;0
WireConnection;3;0;131;0
WireConnection;83;0;140;0
WireConnection;135;0;63;0
WireConnection;61;0;130;0
WireConnection;61;1;3;0
WireConnection;61;2;5;0
WireConnection;61;3;135;0
WireConnection;138;0;7;0
WireConnection;229;0;83;0
WireConnection;136;0;138;0
WireConnection;17;0;18;0
WireConnection;64;0;61;0
WireConnection;43;0;132;0
WireConnection;86;0;85;0
WireConnection;86;1;83;0
WireConnection;228;0;229;0
WireConnection;228;1;227;0
WireConnection;53;0;43;0
WireConnection;19;0;17;0
WireConnection;117;0;113;0
WireConnection;6;0;64;0
WireConnection;6;1;136;0
WireConnection;236;0;86;0
WireConnection;235;0;228;0
WireConnection;116;0;113;0
WireConnection;186;0;6;0
WireConnection;186;1;19;0
WireConnection;129;0;127;0
WireConnection;230;0;236;0
WireConnection;230;1;235;0
WireConnection;128;0;127;0
WireConnection;112;0;101;0
WireConnection;112;2;116;0
WireConnection;112;1;43;0
WireConnection;114;0;101;0
WireConnection;114;2;117;0
WireConnection;114;1;53;0
WireConnection;188;0;186;0
WireConnection;211;0;230;0
WireConnection;38;0;88;0
WireConnection;38;1;112;0
WireConnection;38;5;129;0
WireConnection;46;0;88;0
WireConnection;46;1;114;0
WireConnection;46;5;128;0
WireConnection;183;0;188;0
WireConnection;51;0;38;0
WireConnection;51;1;46;0
WireConnection;158;0;211;0
WireConnection;89;0;51;0
WireConnection;25;0;209;0
WireConnection;25;1;191;0
WireConnection;0;0;25;0
WireConnection;0;1;103;0
WireConnection;0;4;16;0
WireConnection;0;9;194;0
ASEEND*/
//CHKSM=6EFA3390280066617E072ED3C9225BAE62551F92