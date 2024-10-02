using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalUI : MonoBehaviour
{
    public Image bluePortal;  // �Ķ��� ��Ż UI �̹���
    public Image orangePortal;  // ��Ȳ�� ��Ż UI �̹���

    // Ȱ��ȭ�� ��Ż ����
    public Color activeBlueColor = new Color();
    public Color inactiveBlueColor = new Color();
    public Color activeOrangeColor = new Color();
    public Color inactiveOrangeColor = new Color();

    public void UpdatePortalStatus(bool bluePortalActive, bool orangePortalActive)
    {
        // �Ķ� ��Ż Ȱ��ȭ ���ο� ���� ���� ����
        bluePortal.color = bluePortalActive ? activeBlueColor : inactiveBlueColor;
            
        // ��Ȳ ��Ż Ȱ��ȭ ���ο� ���� ���� ����
        orangePortal.color = orangePortalActive ? activeOrangeColor : inactiveOrangeColor;
    }
}
