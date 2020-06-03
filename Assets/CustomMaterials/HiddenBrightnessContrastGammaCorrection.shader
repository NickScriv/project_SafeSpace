
Shader "Hidden/BrightnessContrastGammaCorrection" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

		SubShader{
			Pass {
				ZTest Always Cull Off ZWrite Off

		CGPROGRAM
		#pragma vertex vert_img
		#pragma fragment frag
		#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
		half4 _MainTex_ST;

		uniform fixed _ContrastAdjustment;
		uniform fixed _BrightnessAdjustment;
		uniform fixed _GammaAdjustment;

		fixed4 frag(v2f_img i) : SV_Target
		{

			fixed4 original_n = tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv, _MainTex_ST));
			fixed4 original256 = original_n * 255;

			fixed r;
			fixed g;
			fixed b;
			fixed4 outColor;
			fixed contrastCorrectionFactor = (259.f * (_ContrastAdjustment + 255.f)) / (255.f * (259.f - _ContrastAdjustment));

			r = contrastCorrectionFactor * (original256.r - 128) + 128;
			g = contrastCorrectionFactor * (original256.g - 128) + 128;
			b = contrastCorrectionFactor * (original256.b - 128) + 128;

			//Brigntess
			r += _BrightnessAdjustment;
			g += _BrightnessAdjustment;
			b += _BrightnessAdjustment;

			//Clamp
			r = clamp(r, 0, 255);
			g = clamp(g, 0, 255);
			b = clamp(b, 0, 255);

			//Gamma
			fixed gammaCorrection = 1.f / (_GammaAdjustment + 1);

			r = 255 * pow(r / 255.f, gammaCorrection);
			g = 255 * pow(g / 255.f, gammaCorrection);
			b = 255 * pow(b / 255.f, gammaCorrection);

			//Convert to [0,1] range
			r /= 255;
			g /= 255;
			b /= 255;

			//Clamp
			r = clamp(r, 0, 1);
			g = clamp(g, 0, 1);
			b = clamp(b, 0, 1);

			outColor.x = r;
			outColor.y = g;
			outColor.z = b;
			outColor.w = 1;
			return (outColor);
		}
		ENDCG

			}
	}

		Fallback off

}