using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalUI : MonoBehaviour
{
    public Image bluePortal;  // �Ķ��� ��Ż UI �̹���
    public Image orangePortal;  // ��Ȳ�� ��Ż UI �̹���

    // Ȱ��ȭ�� ��Ż ����
    public Color activeBlueColor = new Color(0.0f, 0.5f, 1.0f, 1.0f);  // ���� �Ķ���
    public Color inactiveBlueColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);  // ��ο� �Ķ���
    public Color activeOrangeColor = new Color(1.0f, 0.5f, 0.0f, 1.0f);  // ���� ��Ȳ��
    public Color inactiveOrangeColor = new Color(0.5f, 0.25f, 0.0f, 1.0f);  // ��ο� ��Ȳ��

    public void UpdatePortalStatus(bool bluePortalActive, bool orangePortalActive)
    {
        // �Ķ� ��Ż Ȱ��ȭ ���ο� ���� ���� ����
        bluePortal.color = bluePortalActive ? activeBlueColor : inactiveBlueColor;

        // ��Ȳ ��Ż Ȱ��ȭ ���ο� ���� ���� ����
        orangePortal.color = orangePortalActive ? activeOrangeColor : inactiveOrangeColor;
    }
}
