Shader "Custom/InvertColorShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // 이미지 텍스처
        _Color ("Main Color", Color) = (1,1,1,1) // material.color와 연결될 색상
        _InvertEffect ("Invert Effect", Range(0, 1)) = 1 // 0: 반전 없음, 1: 완전한 반전, 0.5: 반만 반전
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;  // 텍스처 샘플러
            fixed4 _Color;       // material.color와 연결될 색상
            float _InvertEffect; // 반전 효과 플래그 (0~1 범위)

            fixed4 frag(v2f_img i) : SV_Target
            {
                // 텍스처의 색상 샘플링
                fixed4 col = tex2D(_MainTex, i.uv);

                // 색상 반전 계산
                fixed3 invertedColor = 1.0 - col.rgb;

                // _InvertEffect 값에 따라 원본과 반전된 색상 간 보간
                col.rgb = lerp(col.rgb, invertedColor, _InvertEffect);

                // 텍스처 색상과 material.color를 곱하여 블렌딩
                col.rgb *= _Color.rgb;

                return col;
            }
            ENDCG
        }
    }
    FallBack Off
}
