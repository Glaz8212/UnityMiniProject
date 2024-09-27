using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform linkedPortal; // 연결된 포탈 (다른 포탈의 Transform)
    public float offsetFromExit; // 포탈 출구에서 얼마나 떨어진 위치로 나올지 설정
    private bool isPlayerOverlapping = false;

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 포탈에 진입하면
        if (other.CompareTag("Player") && !isPlayerOverlapping)
        {
            // 다른 포탈의 위치로 플레이어 이동
            TeleportPlayer(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 플레이어가 포탈을 완전히 빠져나오면 중복 충돌 방지 플래그 초기화
        if (other.CompareTag("Player"))
        {
            isPlayerOverlapping = false;
        }
    }

    private void TeleportPlayer(Collider player)
    {
        // CharacterController를 비활성화하여 위치 변경을 허용
        CharacterController playerController = player.GetComponent<CharacterController>();
        if (playerController != null)
        {
            playerController.enabled = false; // 이동 중에는 CharacterController를 비활성화
        }

        // 새로운 위치로 플레이어 이동
        Vector3 newPosition = linkedPortal.position + linkedPortal.forward * offsetFromExit;
        player.transform.position = newPosition;

        // 포탈의 방향에 맞춰 카메라(플레이어) 회전
        Camera playerCamera = player.GetComponentInChildren<Camera>();
        if (playerCamera != null)
        {
            playerCamera.transform.rotation = Quaternion.LookRotation(linkedPortal.forward);
        }
        
        // 이동이 끝나면 CharacterController 다시 활성화
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // 중복 이동 방지
        isPlayerOverlapping = true;
    }
}