using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform muzzlePoint;
    public GameObject bulletPrefab;
    public Transform turret;
    public float rotationSpeed;
    public float maxAngle;
    public float raycastRange;
    public float fireInterval;

    private bool rotatingRight = true;
    private float currentAngle = 0f;
    private float fireCooldown;

    private void Update()
    {
        RotateBody();
        ShootRaycast();
    }

    private void RotateBody()
    {
        // ���� ��� (-maxAngle���� maxAngle����)
        float rotation = rotationSpeed * Time.deltaTime;
        if (rotatingRight)
        {
            currentAngle += rotation;
            if (currentAngle >= maxAngle)
            {
                rotatingRight = false;
            }
        }
        else
        {
            currentAngle -= rotation;
            if (currentAngle <= -maxAngle)
            {
                rotatingRight = true;
            }
        }

        // ȸ��
        turret.localRotation = Quaternion.Euler(0f, currentAngle, 0f);
    }

    private void ShootRaycast()
    {
        // ����ĳ��Ʈ �߻�
        Ray ray = new Ray(muzzlePoint.position, muzzlePoint.forward);
        RaycastHit hit;

        // �÷��̾� �Ǵ� �� �߻�
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (fireCooldown <= 0f)
                {
                    FireCannon(hit.point);
                    fireCooldown = fireInterval;
                }
            }
        }

        if (fireCooldown > 0f)
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    private void FireCannon(Vector3 targetPosition)
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
        Rigidbody rigid = bullet.GetComponent<Rigidbody>();

        Vector3 direction = (targetPosition - muzzlePoint.position).normalized;
        rigid.AddForce(direction * 5000f);
    }
}
