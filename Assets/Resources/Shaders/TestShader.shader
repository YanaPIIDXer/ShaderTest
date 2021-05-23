Shader "Custom/TestShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BufferTex ("Texture", 2D) = "white" {}
        _RenderTex ("Texture", 2D) = "white" {}
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

            struct FragOut
            {
                float4 col1 : COLOR0;
            };

            sampler2D _MainTex;

            FragOut frag (v2f i)
            {
                FragOut o;
                float4 col = tex2D(_MainTex, i.uv);
                o.col1 = col;
                return o;
            }
            ENDCG
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

            struct FragOut
            {
                float4 col1 : COLOR1;
            };

            sampler2D _MainTex;

            FragOut frag (v2f i)
            {
                FragOut o;
                float4 col = tex2D(_MainTex, i.uv);
                o.col1 = col;
                return o;
            }
            ENDCG
        }
    }
}
