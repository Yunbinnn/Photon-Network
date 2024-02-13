using Photon.Pun;
using TMPro;
using UnityEngine;
using Photon.Realtime;

public class ConnectServer : MonoBehaviourPunCallbacks
{
    [SerializeField] Canvas canvasRoom;
    [SerializeField] Canvas canvasLobby;
    [SerializeField] TMP_Dropdown server;

    private void Awake()
    {
        server.options[0].text = "Asia";
        server.options[1].text = "Europe";
        server.options[2].text = "America";

        if (PhotonNetwork.IsConnected)
        {
            canvasLobby.gameObject.SetActive(false);
        }
    }

    public void SelectServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedLobby()
    {
        canvasRoom.sortingOrder = 1;
    }

    public override void OnConnectedToMaster()
    {
        // JoinLobby : Ư�� �κ� �����Ͽ� �����ϴ� ���
        PhotonNetwork.JoinLobby
        (
            new TypedLobby
            (
                server.options[server.value].text,
                LobbyType.Default
            )
       );
    }
}