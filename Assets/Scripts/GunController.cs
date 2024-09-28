using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject portalAprefab;
    public GameObject portalBprefab;
    public Camera playerCam;
    public Transform muzzlePoint;
    public float maxDistance;
    public float portalOffset;

    private GameObject portalA;
    private GameObject portalB;

    private void Update()
    {
        // 좌클릭으로 입구 생성 
        if (Input.GetMouseButton(0))
        {
            ShootPortal(ref portalA, portalAprefab);
        }

        // 우클릭으로 출구 생성
        if (Input.GetMouseButton(1))
        {
            ShootPortal(ref portalB, portalBprefab);
        }

        // 포탈 A와 B가 생성된 후 연결
        if (portalA != null && portalB != null)
        {
            portalA.GetComponent<Portal>().linkedPortal = portalB.transform;
            portalB.GetComponent<Portal>().linkedPortal = portalA.transform;
        }
    }

    private void ShootPortal(ref GameObject portal, GameObject portalPrefab)
    {
        // 레이캐스트 발사
        Ray ray = playerCam.ScreenPointToRay(new Vector3(Screen.width /2 , Screen.height /2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // 레이캐스트가 닿은 대상이 벽이면 포탈 생성
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    // 기존 포탈이 존재하면
                    if (portal != null)
                    {
                        // 기존 포탈 위치 삭제하고 새로 배치
                        Destroy(portal);
                    }

                    // 포탈 Quad 가 벽에 파묻히는 현상을 막기 위해, 벽에서 띄워서 배치
                    Vector3 portalPosition = hit.point + hit.normal * portalOffset;
                    Quaternion portalRotation = Quaternion.LookRotation(-hit.normal) * Quaternion.Euler(0f, 180f, 0f);

                    // 포탈 생성
                    portal = Instantiate(portalPrefab, portalPosition, portalRotation);
                }
            }
        }
    }
}
