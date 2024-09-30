using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform linkedPortal; // ����� ��Ż (�ٸ� ��Ż�� Transform)
    public float offsetFromExit; // ��Ż �ⱸ���� �󸶳� ������ ��ġ�� ������ ����
    private bool isPlayerOverlapping = false;
    public Transform forwardPoint;

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ ��Ż�� �����ϸ�
        if (other.CompareTag("Player") && !isPlayerOverlapping)
        {
            // �ٸ� ��Ż�� ��ġ�� �÷��̾� �̵�
            StartCoroutine(TeleportPlayer(other));
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

    private IEnumerator TeleportPlayer(Collider player)
    {
        // �ߺ� �̵� ����
        isPlayerOverlapping = true;

        // CharacterController�� ��Ȱ��ȭ�Ͽ� ��ġ ������ ���
        CharacterController playerController = player.GetComponent<CharacterController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // ���ο� ��ġ�� �÷��̾� �̵�
        Vector3 newPosition = linkedPortal.position + linkedPortal.forward * offsetFromExit;
        player.transform.position = newPosition;

        CameraController camera = player.GetComponentInChildren<CameraController>();
        if (camera != null)
        {
            Transform linkedForwardPoint = linkedPortal.GetComponent<Portal>().forwardPoint;
            Quaternion targetRotation = Quaternion.LookRotation(linkedForwardPoint.forward);
            camera.SetTargetRotation(targetRotation);
        }

        // �̵��� ������ CharacterController �ٽ� Ȱ��ȭ
        playerController.enabled = true;

        yield return null;
    }
}