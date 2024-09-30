using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // ��Ż�� �浹���� �� ó��
        if (other.gameObject.layer == LayerMask.NameToLayer("Portal"))
        {
            Portal portal = other.GetComponent<Portal>();
            if (portal != null)
            {
                // ��Ż ��ũ��Ʈ�� TeleportPlayer �޼��� ȣ��
                portal.HandleTeleport(transform.parent.gameObject);  // �÷��̾� ������Ʈ ����
            }
        }
    }
}
