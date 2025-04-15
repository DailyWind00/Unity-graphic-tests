Shader "Unlit/GroundHeight"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_NoiseTex ("Noise Texture", 2D) = "white" {}
		_DisplaceStrength ("Displace Strength", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float _DisplaceStrength;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;

				// Get noise value from noise texture
				float noise = tex2Dlod(_NoiseTex, float4(v.uv, 0, 0)).r;
				noise *= _DisplaceStrength;

				// Displace the vertex position based on the noise value
				float3 displaced = v.vertex.xyz + float3(0, noise, 0);

				// Set the new vertex position
				o.vertex = UnityObjectToClipPos(float4(displaced, 1.0));

				// Pass through the UV coordinates
				o.uv = v.uv;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				// Use the main texture for color
				return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
