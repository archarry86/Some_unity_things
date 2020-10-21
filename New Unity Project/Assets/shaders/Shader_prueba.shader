// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "MyShader/Diffuse With LightProbes" {
	Properties{ [NoScaleOffset] _MainTex("Texture", 2D) = "white" {} }
		SubShader{
		Pass {
		Tags {
		"LightMode" = "ForwardBase"
		}
		CGPROGRAM
		#pragma vertex v
		#pragma fragment f
		#include "UnityCG.cginc"
		sampler2D _MainTex;
		struct v2f {
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;


		//half4   pos : SV_POSITION;
		//half2   uv : TEXCOORD0;
		fixed3  vlight : TEXCOORD1;
		};
		fixed4 _MainTex_ST;


		v2f v(appdata_base vertex_data) {
			v2f o;
			o.vertex = UnityObjectToClipPos(vertex_data.vertex);
			o.uv = vertex_data.texcoord;
		

			float3x3 unity_ObjectToWorld_f3x3 = (float3x3)unity_ObjectToWorld;

			half3 worldN = mul(unity_ObjectToWorld_f3x3, vertex_data.normal  *1.0);
			
			half3 shlight = ShadeSH9(float4(worldN, 1.0));

			o.vlight = shlight;

			return o;

		
		}

		fixed4 f (v2f input_fragment) : SV_Target {
			fixed4 col = tex2D(_MainTex, input_fragment.uv);
			col.rgb *= input_fragment.vlight;
			return col;
		}

		
		ENDCG
		}
	}
}