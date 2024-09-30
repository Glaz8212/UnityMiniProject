using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed; // ī�޶� ȸ���ӵ��� ����� ���콺 ���� ����
    private float mouseX;
    private float mouseY;

    private bool RotateToPortal = false;
    private Quaternion targetRotation;

    private void Update()
    {
        if (RotateToPortal)
        {
            // ��ǥ�� ī�޶� ȸ��
            transform.rotation = targetRotation;

            //  ���콺�� �Է��� ȸ���������� ����
            Vector3 eulerRotation = targetRotation.eulerAngles;
            mouseX = eulerRotation.x;
            mouseY = eulerRotation.y;

            RotateToPortal = false;
        }
        else
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

    public void SetTargetRotation(Quaternion newRotation)
    {
        targetRotation = newRotation;
        RotateToPortal = true;
    }
}
