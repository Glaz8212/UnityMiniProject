using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 포탈과 충돌했을 때 처리
        if (other.gameObject.layer == LayerMask.NameToLayer("Portal"))
        {
            Debug.Log("히트박스가 포탈과 충돌");
            Portal portal = other.GetComponent<Portal>();
            if (portal != null)
            {
                // 포탈 스크립트의 TeleportPlayer 메서드 호출
                portal.HandleTeleport(transform.parent.gameObject);  // 플레이어 오브젝트 전달
            }
        }
    }
}
