using Photon.Pun;
using Photon.Realtime; // ��� ������ �������� �� �̺�Ʈ�� ȣ���ϴ� ���̺귯��
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button roomCreateButton;
    public Transform roomParentTransform;
    public TMP_InputField roomNameInputField;
    public TMP_InputField roomPersonalInputField;

    // �� ����� �����ϱ� ���� �ڷᱸ��
    Dictionary<string, RoomInfo> roomDictionary = new();

    private void Update()
    {
        if (roomNameInputField.text.Length > 0 && roomPersonalInputField.text.Length > 0)
        {
            roomCreateButton.interactable = true;
        }
        else roomCreateButton.interactable = false;
    }

    // �뿡 ������ �� ȣ��Ǵ� CallBack �Լ�
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void InstantiateRoom()
    {
        // �� �ɼ��� �����մϴ�.
        RoomOptions roomOptions = new()
        {
            // �ִ� �������� ���� �����մϴ�.
            MaxPlayers = byte.Parse(roomPersonalInputField.text),

            // ���� ���� ���θ� �����մϴ�.
            IsOpen = true,

            // �κ񿡼� �� ����� �����ų �� �����մϴ�.
            IsVisible = true,
        };

        // ���� �����ϴ� �Լ�
        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);
    }

    // �ش� �κ� �� ����� ���� ������ ������ ȣ��(�߰�, ����, ����)�Ǵ� �Լ�
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // 1. ���� �����մϴ�.
        RemoveRoom();

        // 2. ���� ������Ʈ �մϴ�.
        //UpdateRoom();

        // 3. ���� �����մϴ�.
        InstantiateRoom();
    }

    public void RemoveRoom()
    {
        foreach (Transform roomTransform in roomParentTransform)
        {
            Destroy(roomTransform.gameObject);
        }
    }

    public void UpdateRoom(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            // �ش� �̸��� roomDictionary�� key ������ �����Ǿ� �ִٸ�,
            if (roomDictionary.ContainsKey(roomList[i].Name))
            {
                // RemovedFromList : (true) �뿡�� �����Ǿ��� ��
                if (roomList[i].RemovedFromList)
                {
                    // roomDictionary������ �����մϴ�.
                    roomDictionary.Remove(roomList[i].Name);
                }
                else // ���� ���� �ʾҴٸ�,
                {
                    // roomDictionary�� �ٽ� �־��ݴϴ�.
                    roomDictionary[roomList[i].Name] = roomList[i];
                }
            }
            else // key ���� �����Ǿ� ���� �ʴٸ�,
            {
                // roomDictionary�� key���� �ֽ��ϴ�.
                roomDictionary[roomList[i].Name] = roomList[i];
            }
        }
    }
}