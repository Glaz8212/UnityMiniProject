using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 200f; // 카메라 회전속도에 사용할 마우스 감도 변수
    private float mouseX;
    private float mouseY;

    private void Update()
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
