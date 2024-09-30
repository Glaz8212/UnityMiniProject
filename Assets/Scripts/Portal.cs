using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform linkedPortal;  // ����� ��Ż (�ٸ� ��Ż�� Transform)
    public float offsetFromExit = 1f;  // ��Ż �ⱸ���� �󸶳� ������ ��ġ�� ������ ���� (�⺻�� 1)
    public Transform forwardPoint;  // ��Ż�� ���� ������ ��Ÿ���� �� ������Ʈ
    private bool isObjectOverlapping = false;  // �ߺ� �浹 ���� �÷���

    // ��Ʈ�ڽ����� ȣ��Ǵ� �޼���
    public void HandleTeleport(GameObject obj)
    {
        if (!isObjectOverlapping)
        {
            // �ߺ� ���� �÷��� ����
            isObjectOverlapping = true;

            // �ڷ�ƾ ���� (�̵� �� ȸ�� ó��)
            StartCoroutine(TeleportPlayer(obj));
        }
    }

    private IEnumerator TeleportPlayer(GameObject obj)
    {
        // �÷��̾��� CharacterController ��Ȱ��ȭ
        CharacterController playerController = obj.GetComponent<CharacterController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // �÷��̾ ���ο� ��ġ�� �̵� (linkedPortal�� forward ���⿡ offset ����)
        Vector3 newPosition = linkedPortal.position + linkedPortal.forward * offsetFromExit;
        obj.transform.position = newPosition;

        // ��Ż�� ForwardPoint�� �ٶ󺸵��� �÷��̾��� ī�޶� ȸ�� ó��
        CameraController camera = obj.GetComponentInChildren<CameraController>();
        if (camera != null)
        {
            Transform linkedForwardPoint = linkedPortal.GetComponent<Portal>().forwardPoint;
            Quaternion targetRotation = Quaternion.LookRotation(linkedForwardPoint.forward);
            camera.SetTargetRotation(targetRotation);
        }

        // �̵��� ������ CharacterController �ٽ� Ȱ��ȭ
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        yield return null;

        // �ߺ� ���� �÷��� ����
        isObjectOverlapping = false;
    }
}