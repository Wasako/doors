﻿Shader "Shaders101/Colored UV"
{
	SubShader
	{
		Tags
	{
		"PreviewType" = "Plane"
	}
		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

	struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float4 vertex : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.uv;
		return o;
	}

	float4 frag(v2f i) : SV_Target
	{
		float4 color = float4(i.uv.x, i.uv.y, 1, 0);
		return color;
	}
		ENDCG
	}
	}
}