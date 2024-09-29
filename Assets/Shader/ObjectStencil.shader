Shader "Custom/ObjectStencil"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Geometry" }

        Pass
        {
            // Stencil 설정: 스텐실 값이 1과 같을 때만 렌더링
            Stencil
            {
                Ref 1          // 스텐실 참조 값
                Comp Equal     // 스텐실 값이 1일 때만 렌더링
                Pass Keep      // 통과할 때 스텐실 값 유지
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
