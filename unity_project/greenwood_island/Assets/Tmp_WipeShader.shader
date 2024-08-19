Shader "Custom/TMP_WipeShader"
{
    Properties
    {
        _MainTex ("Font Atlas", 2D) = "white" {}
        _WipePosition ("Wipe Position", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags {"Queue"="Overlay" }
        Pass
        {
            ZWrite Off
            Cull Off
            Lighting Off
            BindChannels
            {
                Bind "vertex", vertex
                Bind "texcoord", texcoord
                Bind "color", color
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _WipePosition;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                float4 color : COLOR;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.texcoord) * i.color;

                // Wipe effect based on the x coordinate
                if (i.vertex.x < _WipePosition)
                {
                    col.a = 0;
                }

                return col;
            }
            ENDCG
        }
    }
}
