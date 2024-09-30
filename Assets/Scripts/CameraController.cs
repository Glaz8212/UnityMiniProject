using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed; // 카메라 회전속도에 사용할 마우스 감도 변수
    private float mouseX;
    private float mouseY;

    private bool RotateToPortal = false;
    private Quaternion targetRotation;

    private void Update()
    {
        if (RotateToPortal)
        {
            // 목표로 카메라 회전
            transform.rotation = targetRotation;

            //  마우스의 입력을 회전방향으로 설정
            Vector3 eulerRotation = targetRotation.eulerAngles;
            mouseX = eulerRotation.x;
            mouseY = eulerRotation.y;

            RotateToPortal = false;
        }
        else
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            // x축 좌우 회전
            mouseX += x * speed * Time.deltaTime;

            // y축 상하 회전
            mouseY -= y * speed * Time.deltaTime;
            mouseY = Mathf.Clamp(mouseY, -90f, 90f);

            // 카메라 회전 적용
            transform.eulerAngles = new Vector3(mouseY, mouseX, 0);
        }
    }

    public void SetTargetRotation(Quaternion newRotation)
    {
        targetRotation = newRotation;
        RotateToPortal = true;
    }
}
