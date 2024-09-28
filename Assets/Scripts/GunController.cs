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
        // ��Ŭ������ �Ա� ���� 
        if (Input.GetMouseButton(0))
        {
            ShootPortal(ref portalA, portalAprefab);
        }

        // ��Ŭ������ �ⱸ ����
        if (Input.GetMouseButton(1))
        {
            ShootPortal(ref portalB, portalBprefab);
        }

        // ��Ż A�� B�� ������ �� ����
        if (portalA != null && portalB != null)
        {
            portalA.GetComponent<Portal>().linkedPortal = portalB.transform;
            portalB.GetComponent<Portal>().linkedPortal = portalA.transform;
        }
    }

    private void ShootPortal(ref GameObject portal, GameObject portalPrefab)
    {
        // ����ĳ��Ʈ �߻�
        Ray ray = playerCam.ScreenPointToRay(new Vector3(Screen.width /2 , Screen.height /2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // ����ĳ��Ʈ�� ���� ����� ���̸� ��Ż ����
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    // ���� ��Ż�� �����ϸ�
                    if (portal != null)
                    {
                        // ���� ��Ż ��ġ �����ϰ� ���� ��ġ
                        Destroy(portal);
                    }

                    // ��Ż Quad �� ���� �Ĺ����� ������ ���� ����, ������ ����� ��ġ
                    Vector3 portalPosition = hit.point + hit.normal * portalOffset;
                    Quaternion portalRotation = Quaternion.LookRotation(-hit.normal) * Quaternion.Euler(0f, 180f, 0f);

                    // ��Ż ����
                    portal = Instantiate(portalPrefab, portalPosition, portalRotation);
                }
            }
        }
    }
}
