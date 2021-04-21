// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/VideoWithAlpha" {
	Properties {
		_yMainTex ("_yMainTex", 2D) = "white" {}
		_uvMainTex ("_uvMainTex", 2D) = "white" {}
		_yAlphaTex ("_yAlphaTex", 2D) = "white" {}
	}
	SubShader {


    	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}

        LOD 100
		
		Pass {
		 //   ZWrite on
		//	Blend SrcAlpha OneMinusSrcAlpha
		//	ColorMaterial AmbientAndDiffuse
		 //   Lighting Off
	    //	Cull Off
		
	   Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Offset -1, -1
		Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _yMainTex;
			sampler2D _uvMainTex;
			sampler2D _yAlphaTex; 
			
			struct v2f 
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};
			
			v2f vert(appdata_base v)
			{
				v2f o;
				
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				
				return o;
			}

			half4 frag (v2f IN) : COLOR 
			{
				const float3x3 yuv2rgb = float3x3(
			    	1.0,  0.0, 1.403,
			    	1.0, -0.344, -0.714,
			    	1.0, 1.772,   0.0
				);
				
				float y = tex2D (_yMainTex, IN.uv).r;
				float u = tex2D (_uvMainTex, IN.uv).r - 0.5;
				float v = tex2D (_uvMainTex, IN.uv).a - 0.5;
				float a = 1.14 * tex2D (_yAlphaTex, IN.uv).r - 0.0625;

				float3 rgb = mul(yuv2rgb, float3(y, u, v)); 
				
				return float4(rgb, a);
			}
			ENDCG
		} 
	}
	FallBack "Diffuse"
}
