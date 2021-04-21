// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Custom/Infrared" {
	Properties {
		_MainTex ("MainTex", 2D) = "white" {}
		_Heat ("Heat", Range(0.0, 2.0)) = 2
	}
	SubShader {
		Tags {"Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent"}
	    //LOD 100
			
		Pass {
		   // ZWrite On
		 //Blend SrcAlpha OneMinusSrcAlpha 
		//   ColorMaterial AmbientAndDiffuse
		    Lighting Off
		    Cull Off

			Blend SrcAlpha One

	
	BindChannels {
		Bind "Color", color
		Bind "Vertex", vertex
		Bind "TexCoord", texcoord
	}
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float _Heat;
			
			struct appdata {
	            float4 vertex : POSITION;
	            fixed4 color : COLOR;
	            float2 texcoord : TEXCOORD0;
	        };
			
			struct v2f 
			{
				float4 pos : SV_POSITION;
				float4 col : COLOR;
				float2 uv : TEXCOORD0;
			};
			
			v2f vert(appdata v)
			{
				v2f o;
				
				o.pos = UnityObjectToClipPos(v.vertex);
				o.col = v.color;
				o.uv = v.texcoord;
				
				return o;
			}

			half4 frag (v2f IN) : COLOR 
			{
				float4 pixcol = tex2D (_MainTex, IN.uv);
				float4 heatPixcol = float4(_Heat * pixcol.rgb, pixcol.a);
				
 				float4 colors[3];
 				colors[0] = float4(0.0, 0.0, 1.0, 1.0);
 				colors[1] = float4(1.0, 1.0, 0.0, 1.0);
 				colors[2] = float4(1.0 ,0.0 ,0.0 ,1.0);
 				float grey = dot(heatPixcol.rgb, float3(0.3, 0.59, 0.11));
 				
 				float ix = (grey < 0.5) ? 0.0 : 1.0;
 				
				float4 res1 = lerp(colors[0], colors[1], (grey - ix * 0.5) / 0.5);
				float4 res2 = lerp(colors[1], colors[2], (grey - ix * 0.5) / 0.5);
				
				float4 thermal = lerp(res1, res2, ix);

				

 				return float4(thermal.rgb, pixcol.a * IN.col.a);
			}
			ENDCG
		} 
	}
	FallBack "Diffuse"
}
