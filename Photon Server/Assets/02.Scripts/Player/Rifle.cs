using Photon.Pun;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    [SerializeField] int atk = 20;
    [SerializeField] Camera cam;
    [SerializeField] LayerMask mask;

    void Update()
    {
        Launch();
    }

    public void Launch()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ray = cam.ViewportPointToRay(Vector2.one / 2);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                PhotonView photonView = hit.collider.GetComponent<PhotonView>();

                photonView.GetComponent<Metalon>().Health -= atk;

                if (photonView.GetComponent<Metalon>().Health <= 0)
                {
                    if (photonView.IsMine)
                    {
                        PhotonNetwork.Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
    }
}