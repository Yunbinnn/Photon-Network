using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject nickNamePanel;
    [SerializeField] TMP_InputField nickNameInputField;

    private void Awake()
    {
        CreatePlayer();
        CheckNickName();
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

        direction.y = 1f;

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

    public void CreateNickName()
    {
        PlayerPrefs.SetString("Nick Name", nickNameInputField.text);

        PhotonNetwork.NickName = nickNameInputField.text;

        nickNamePanel.SetActive(false);
    }

    public void CheckNickName()
    {
        string name = PlayerPrefs.GetString("Nick Name");

        PhotonNetwork.NickName = name;

        if(string.IsNullOrEmpty(name))
        {
            nickNamePanel.SetActive(true);
        }
        else nickNamePanel.SetActive(false);
    }
}