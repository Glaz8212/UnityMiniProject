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
        Ray ray = new Ray(muzzlePoint.position, playerCam.transform.forward);
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

                    // ��Ż ����
                    portal = Instantiate(portalPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
        }
    }
}
