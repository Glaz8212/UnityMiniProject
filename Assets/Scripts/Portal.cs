using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform linkedPortal; // ����� ��Ż (�ٸ� ��Ż�� Transform)
    public float offsetFromExit; // ��Ż �ⱸ���� �󸶳� ������ ��ġ�� ������ ����
    private bool isPlayerOverlapping = false;

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ ��Ż�� �����ϸ�
        if (other.CompareTag("Player") && !isPlayerOverlapping)
        {
            // �ٸ� ��Ż�� ��ġ�� �÷��̾� �̵�
            TeleportPlayer(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �÷��̾ ��Ż�� ������ ���������� �ߺ� �浹 ���� �÷��� �ʱ�ȭ
        if (other.CompareTag("Player"))
        {
            isPlayerOverlapping = false;
        }
    }

    private void TeleportPlayer(Collider player)
    {
        // CharacterController�� ��Ȱ��ȭ�Ͽ� ��ġ ������ ���
        CharacterController playerController = player.GetComponent<CharacterController>();
        if (playerController != null)
        {
            playerController.enabled = false; // �̵� �߿��� CharacterController�� ��Ȱ��ȭ
        }

        // ���ο� ��ġ�� �÷��̾� �̵�
        Vector3 newPosition = linkedPortal.position + linkedPortal.forward * offsetFromExit;
        player.transform.position = newPosition;

        // ��Ż�� ���⿡ ���� ī�޶�(�÷��̾�) ȸ��
        Camera playerCamera = player.GetComponentInChildren<Camera>();
        if (playerCamera != null)
        {
            playerCamera.transform.rotation = Quaternion.LookRotation(linkedPortal.forward);
        }
        
        // �̵��� ������ CharacterController �ٽ� Ȱ��ȭ
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // �ߺ� �̵� ����
        isPlayerOverlapping = true;
    }
}