Shader "Custom/PortalStencil"
{
    SubShader
    {
        Tags { "Queue" = "Geometry" }

        Pass
        {
            // ���� ��� x ���ٽ� ���� ����
            ColorMask 0

            Stencil
            {
                Ref 1
                Comp Always   // �׻� ���
                Pass Replace  // ����� �� ���ٽ� �� 1
                
            }
        }
    }
}