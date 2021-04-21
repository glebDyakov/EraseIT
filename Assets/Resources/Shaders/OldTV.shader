// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/OldTV" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_MaskTex ("Mask", 2D) = "white" {}
		_Offset("Offset",Vector) = (0,0,0,0)
		_Alpha("Alpha",Float) = 1
	}
SubShader
	{
	    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	    LOD 100
	    
	    ZWrite Off
	    //Blend SrcAlpha OneMinusSrcAlpha 
		Blend SrcAlpha One 
	    ColorMaterial AmbientAndDiffuse
	    Lighting Off
	    Cull Off

		Pass
		{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				
				#include "UnityCG.cginc"
	
				struct appdata_t
				{
					float4 vertex : POSITION;
					float2 texcoord : TEXCOORD0;
					fixed4 color : COLOR;
				};
	
				struct v2f
				{
					float4 vertex : SV_POSITION;
					half2 texcoord : TEXCOORD0;
					fixed4 color : COLOR;
				};
	
				sampler2D _MainTex;
				sampler2D _MaskTex;
				float4 _Offset;
				float4 _MainTex_ST;
				float _Alpha;
				
				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
					o.color = v.color;
					return o;
				}
				
				fixed4 frag (v2f i) : COLOR
				{
				    float2 txcoord = i.texcoord*_Offset.z;
					txcoord.y+=_Offset.y;
					fixed4 col = tex2D(_MainTex,txcoord ) ;
					fixed4 m = tex2D(_MaskTex, i.texcoord) ;
					return col * m.a * _Alpha;
				}
			ENDCG
		}

	}


}
