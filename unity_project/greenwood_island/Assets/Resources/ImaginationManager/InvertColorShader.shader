Shader "Custom/InvertColorShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // 이미지 텍스처
        _InvertEffect ("Invert Effect", Range(0, 1)) = 1 // 0: 반전 없음, 1: 완전한 반전
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" } // RenderType을 Transparent로 변경

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;  // 텍스처 샘플러
            float _InvertEffect;  // 반전 효과 플래그 (0~1 범위)

            fixed4 frag(v2f_img i) : SV_Target
            {
                // 텍스처의 색상 샘플링
                fixed4 col = tex2D(_MainTex, i.uv);

                // 색상 반전 계산
                fixed3 invertedColor = 1.0 - col.rgb;

                // _InvertEffect 값에 따라 원본과 반전된 색상 간 보간
                col.rgb = lerp(col.rgb, invertedColor, _InvertEffect);

                return col; // alpha는 기본적으로 텍스처에서 가져옴
            }
            ENDCG
        }
    }
    FallBack Off
}
