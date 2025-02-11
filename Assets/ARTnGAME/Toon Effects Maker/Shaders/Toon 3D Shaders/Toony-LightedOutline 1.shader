Shader "Toon Effects Master/Lighted Outline" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.002, 0.03)) = .005
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
	}

	/*SubShader {
		Tags { "RenderType"="Opaque" }
		UsePass "Toon Effects Master/Lighted/FORWARD"
		UsePass "Toon Effects Master/Basic Outline/OUTLINE"
	} */

		SubShader{
		Tags { "RenderType" = "Opaque" }
		Pass {
			Name "BASE"
			Cull Off
			SetTexture[_MainTex] {
				constantColor[_Color]
				Combine texture * constant
			}
			SetTexture[_ToonShade] {
				combine texture * previous DOUBLE, previous
			}
		}
	}
	
	Fallback "Toon Effects Master/Lighted"
}
