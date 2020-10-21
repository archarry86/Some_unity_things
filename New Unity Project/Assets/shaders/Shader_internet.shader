Shader "Unlit/LightProbesOnly" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}

	}

		Subshader{
			Tags { "RenderType" = "Opaque" }
			Fog { Mode Off }

			Pass {
				Name "FORWARD"
				Tags { "LightMode" = "ForwardBase" }

				CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag
					#pragma fragmentoption ARB_precision_hint_fastest
					#include "UnityCG.cginc"               


					struct v2f {
						half4   pos : SV_POSITION;
						half2   uv : TEXCOORD0;
						fixed3  vlight : TEXCOORD1;
					};



					v2f vert(appdata_full v)
					{
						v2f o;
						o.pos = UnityObjectToClipPos(v.vertex);
						o.uv = v.texcoord;
						half3 worldN = mul((float3x3)unity_ObjectToWorld, SCALED_NORMAL);
						half3 shlight = ShadeSH9(float4(worldN,1.0));
						o.vlight = shlight;
						return o;
					}

					sampler2D _MainTex;

					fixed4 frag(v2f i) : COLOR
					{
						fixed4 mainTextColor = tex2D(_MainTex,i.uv);
						fixed4  c = fixed4(i.vlight[0], i.vlight[1], i.vlight[2], 1.0);
						c.rgb = i.vlight;
						return mainTextColor * c;
					}
				ENDCG
			}
	}
}