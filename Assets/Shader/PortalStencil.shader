Shader "Custom/PortalStencil"
{
    SubShader
    {
        Tags { "Queue" = "Geometry" }

        Pass
        {
            // 색상 출력 x 스텐실 값만 설정
            ColorMask 0

            Stencil
            {
                Ref 1
                Comp Always   // 항상 기록
                Pass Replace  // 통과할 때 스텐실 값 1
                
            }
        }
    }
}