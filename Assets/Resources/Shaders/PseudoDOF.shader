Shader "WebCameraEffect/PseudoDOF"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BufferTex ("Texture", 2D) = "white" {}
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
                float4 col = float4(0, 0, 0, 1);
                float4 main = tex2D(_MainTex, i.uv);
                float4 buffer = tex2D(_BufferTex, i.uv);
                float th = 0.03f;
                if (abs(main.r - buffer.r) > th || abs(main.g - buffer.g) > th || abs(main.b - buffer.b) > th)
                {
                    col.r = 1;
                }
                return col;
            }
            ENDCG
        }

    }
}
