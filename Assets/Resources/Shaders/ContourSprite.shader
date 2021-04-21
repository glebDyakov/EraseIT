Shader "ContourSprite" {
	Properties {
	    _Color ("Tint (A = Opacity)", Color) = (1,1,1,1) 
	    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}
	
	SubShader {
	    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	    LOD 100
	    
	    ZWrite Off
	    Blend SrcAlpha OneMinusSrcAlpha 
	    ColorMaterial AmbientAndDiffuse
	    Lighting Off
	    Cull Off
	     
	    Pass {  
	        SetTexture [_MainTex] {
	            combine primary, texture
	        }
	    }
	}
}
