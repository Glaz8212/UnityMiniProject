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
        // 플레이어와 충돌하면
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
            // 플레이어 사망 구현 필요
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
