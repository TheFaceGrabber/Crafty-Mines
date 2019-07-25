// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/VertexColorDiffuse"
{
	Properties
	{
		_TerrainTexture ("Terrain Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "LightMode"="ForwardBase" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 color : COLOR;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float4 color : COLOR;
			};

			sampler2D _TerrainTexture;
			float4 _TerrainTexture_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _TerrainTexture);
                o.normal = mul(float4(v.normal, 0), unity_WorldToObject);
                o.color = v.color;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_TerrainTexture, i.uv) * i.color;
                
                fixed4 n = normalize(float4(i.normal,1));
                fixed4 l = normalize(_WorldSpaceLightPos0);
                
                fixed4 ndotl = saturate(dot(n, l));
                
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
                
				return saturate(col * (ndotl));
			}
			ENDCG
		}
	}
}
