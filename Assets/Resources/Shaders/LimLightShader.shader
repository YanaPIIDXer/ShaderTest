Shader "WebCameraEffect/LimLight"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BufferTex ("Texture", 2D) = "white" {}
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
                float th = 0.05f;
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
            float _TexelX;
            float _TexelY;

            float4 frag (v2f i) : COLOR0
            {
                float4 col = tex2D(_DiffMap, i.uv);
                float left = tex2D(_DiffMap, i.uv - float2(_TexelX, 0.0f)).r;
                float right = tex2D(_DiffMap, i.uv + float2(_TexelX, 0.0f)).r;
                float up = tex2D(_DiffMap, i.uv - float2(0.0, _TexelY)).r;
                float down = tex2D(_DiffMap, i.uv + float2(0.0f, _TexelY)).r;
                if (left > 0 || right > 0 || up > 0 || down > 0)
                {
                    col.r = 1.0f;
                }
                return col;
            }
            ENDCG
        }

        GrabPass { "_PowerMap" }

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
            sampler2D _PowerMap;

            float4 frag (v2f i) : COLOR0
            {
                float4 col = tex2D(_MainTex, i.uv);
                float power = tex2D(_PowerMap, i.uv).r;
                if (power > 0)
                {
                    col *= 3.0f;
                }
                return col;
            }
            ENDCG
        }
    }
}
