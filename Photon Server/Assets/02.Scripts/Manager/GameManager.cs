using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate("Player", RandomPosition(5f), Quaternion.identity);
    }

    public Vector3 RandomPosition(float distance)
    {
        Vector3 direction = Random.insideUnitSphere;

        direction.Normalize();

        direction *= distance;

        direction.y = 0f;

        return direction;
    }

    public void ExitGame()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
    }
}