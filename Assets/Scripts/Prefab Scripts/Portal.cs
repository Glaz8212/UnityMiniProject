using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform linkedPortal;  // 연결된 포탈 (다른 포탈의 Transform)
    public float offsetFromExit = 1f;  // 포탈 출구에서 얼마나 떨어진 위치로 나올지 설정 (기본값 1)
    public Transform forwardPoint;  // 포탈의 정면 방향을 나타내는 빈 오브젝트
    private bool isObjectOverlapping = false;  // 중복 충돌 방지 플래그

    public AudioClip portapTPsound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // 히트박스에서 호출되는 메서드
    public void HandleTeleport(GameObject obj)
    {
        if (!isObjectOverlapping)
        {
            // 중복 방지 플래그 설정
            isObjectOverlapping = true;

            audioSource.PlayOneShot(portapTPsound);

            // 코루틴 실행 (이동 및 회전 처리)
            StartCoroutine(TeleportPlayer(obj));
        }
    }

    private IEnumerator TeleportPlayer(GameObject obj)
    {
        // 플레이어의 CharacterController 비활성화
        CharacterController playerController = obj.GetComponent<CharacterController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // 플레이어를 새로운 위치로 이동 (linkedPortal의 forward 방향에 offset 적용)
        Vector3 newPosition = linkedPortal.position + linkedPortal.forward * offsetFromExit;
        obj.transform.position = newPosition;

        // 포탈의 ForwardPoint를 바라보도록 플레이어의 카메라 회전 처리
        CameraController camera = obj.GetComponentInChildren<CameraController>();
        if (camera != null)
        {
            Transform linkedForwardPoint = linkedPortal.GetComponent<Portal>().forwardPoint;
            Quaternion targetRotation = Quaternion.LookRotation(linkedForwardPoint.forward);
            camera.SetTargetRotation(targetRotation);
        }

        // 이동이 끝나면 CharacterController 다시 활성화
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        yield return null;

        // 중복 방지 플래그 해제
        isObjectOverlapping = false;
    }
}