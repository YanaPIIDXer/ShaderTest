Shader "WebCameraEffect/Wave"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BufferTex ("Texture", 2D) = "white" {}
        _WaveMap ("Texture" , 2D) = "white" {}
        _TexelX("Texel X", Float) = 0
        _TexelY("Texel Y", Float) = 0
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

            #include "UnityCG.cginc"

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
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _BufferTex;

            float4 frag (v2f i) : COLOR0
            {
                float4 output = float4(0, 0, 0, 1);
                float4 main = tex2D(_MainTex, i.uv);
                float4 buf = tex2D(_BufferTex, i.uv);
                float th = 0.5f;
                if (abs(main.r - buf.r) > th || abs(main.g - buf.g) > th || abs(main.b - buf.b) > th)
                {
                    output.r = 1;
                }
                return output;
            }
            ENDCG
        }

        GrabPass { "_DiffMap" }
        
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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _DiffMap;
            sampler2D _WaveMap;
            float _TexelX;
            float _TexelY;

            float4 frag (v2f i) : COLOR0
            {
                float4 col = tex2D(_WaveMap, i.uv);
                col.r *= 0.8f;

                // 波紋の動作
                if (col.r <= 0.01f)
                {
                    float wave1 = tex2D(_WaveMap, i.uv + float2(_TexelX,   _TexelY)).r;
                    float wave2 = tex2D(_WaveMap, i.uv + float2(_TexelX,  -_TexelY)).r;
                    float wave3 = tex2D(_WaveMap, i.uv + float2(-_TexelX,  _TexelY)).r;
                    float wave4 = tex2D(_WaveMap, i.uv + float2(-_TexelX, -_TexelY)).r;
                    float power = (wave1 + wave2 + wave3 + wave4);
                    col.r = power;
                }
                
                // 動いた個所から波紋を発生させる
                float4 diff = tex2D(_DiffMap, i.uv);
                if (diff.r > 0)
                {
                    col.r = 0.1f;
                }
                return col;
            }
            ENDCG
        }

        GrabPass { "_WaveGrab" }

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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _WaveGrab;

            float4 frag (v2f i) : COLOR0
            {
                float4 col = tex2D(_MainTex, i.uv);
                float wave = tex2D(_WaveGrab, i.uv).r;
                if (wave > 0)
                {
                    col *= wave;
                }
                return col;
            }
            ENDCG
        }

    }
}
