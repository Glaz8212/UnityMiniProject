using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Camera playerCam; // 플레이어의 방향을 결정하기 위해 참조할 메인 카메라

    [SerializeField] float speed;
    [SerializeField] float gravity;
    [SerializeField] float jumpHeight;
    
    private Vector3 velocity;
    private bool isGrounded;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundLayerMask;

    private void Start()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
    }
    private void Update()
    {
        GroundCheck();
        Move();
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void Move()
    {
        // 이동 입력
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 카메라의 전방과 우측 참조
        Vector3 forward = playerCam.transform.forward;
        Vector3 right = playerCam.transform.right;

        // 카메라 방향 기준 으로 움직이기
        Vector3 move = (forward * z + right * x).normalized;

        characterController.Move(move * speed * Time.deltaTime);

        // 점프
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 중력 적용
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
