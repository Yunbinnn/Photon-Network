using Photon.Pun;
using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    WaitForSeconds waitForSeconds = new(5f);

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
            StartCoroutine(SpawnObject());
    }

    public Vector3 RandomPosition(float distance)
    {
        Vector3 direction = Random.insideUnitSphere;

        direction.Normalize();

        direction *= distance;

        direction.y = 0f;

        return direction;
    }

    public IEnumerator SpawnObject()
    {
        while (true)
        {
            PhotonNetwork.InstantiateRoomObject("Metalon", RandomPosition(15), Quaternion.identity);

            yield return waitForSeconds;
        }
    }
}
