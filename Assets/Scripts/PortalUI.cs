using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalUI : MonoBehaviour
{
    public Image bluePortal;  // 파란색 포탈 UI 이미지
    public Image orangePortal;  // 주황색 포탈 UI 이미지

    // 활성화된 포탈 색상
    public Color activeBlueColor = new Color();
    public Color inactiveBlueColor = new Color();
    public Color activeOrangeColor = new Color();
    public Color inactiveOrangeColor = new Color();

    public void UpdatePortalStatus(bool bluePortalActive, bool orangePortalActive)
    {
        // 파란 포탈 활성화 여부에 따라 색상 변경
        bluePortal.color = bluePortalActive ? activeBlueColor : inactiveBlueColor;
            
        // 주황 포탈 활성화 여부에 따라 색상 변경
        orangePortal.color = orangePortalActive ? activeOrangeColor : inactiveOrangeColor;
    }
}
