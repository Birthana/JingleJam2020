﻿Shader "Unlit/SimpleBackground"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1("Color1", Color) = (1, 0, 0)
        _Color2("Color2", Color) = (0, 1, 0)
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color1, _Color2;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float InvLerp(float a, float b, float value) {
                return (value - a) / (b - a);
            }

            float Posterize(float steps, float value) {
                return floor(value * steps) / steps;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float t = Posterize(8, uv.y);
                float3 blend = lerp(_Color1, _Color2, t);
                return float4(blend, 0);
            }
            ENDCG
        }
    }
}