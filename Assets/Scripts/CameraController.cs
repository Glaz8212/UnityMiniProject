using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 200f; // ī�޶� ȸ���ӵ��� ����� ���콺 ���� ����
    private float mouseX;
    private float mouseY;

    private void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        // x�� �¿� ȸ��
        mouseX += x * speed * Time.deltaTime;

        // y�� ���� ȸ��
        mouseY -= y * speed * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        // ī�޶� ȸ�� ����
        transform.eulerAngles = new Vector3(mouseY, mouseX, 0);
    }
}
