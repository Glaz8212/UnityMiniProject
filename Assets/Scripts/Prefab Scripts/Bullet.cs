using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, bulletLifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �÷��̾�� �浹�ϸ�
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
            // �÷��̾� ��� ���� �ʿ�
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
