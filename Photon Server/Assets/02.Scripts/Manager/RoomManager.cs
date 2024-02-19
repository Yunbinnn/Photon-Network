using Photon.Pun;
using Photon.Realtime; // 어느 서버에 접속했을 때 이벤트를 호출하는 라이브러리
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button roomCreateButton;
    public Transform roomParentTransform;
    public TMP_InputField roomNameInputField;
    public TMP_InputField roomPersonalInputField;

    // 룸 목록을 저장하기 위한 자료구조
    Dictionary<string, RoomInfo> roomDictionary = new();

    private void Update()
    {
        if (roomNameInputField.text.Length > 0 && roomPersonalInputField.text.Length > 0)
        {
            roomCreateButton.interactable = true;
        }
        else roomCreateButton.interactable = false;
    }

    // 룸에 입장한 후 호출되는 CallBack 함수
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void InstantiateRoom()
    {
        // 룸 옵션을 설정합니다.
        RoomOptions roomOptions = new()
        {
            // 최대 접속자의 수를 설정합니다.
            MaxPlayers = byte.Parse(roomPersonalInputField.text),

            // 룸의 오픈 여부를 설정합니다.
            IsOpen = true,

            // 로비에서 룸 목록을 노출시킬 지 설정합니다.
            IsVisible = true,
        };

        // 룸을 생성하는 함수
        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);
    }

    // 해당 로비에 방 목록의 변경 사항이 있으면 호출(추가, 삭제, 참가)되는 함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // 1. 룸을 삭제합니다.
        RemoveRoom();

        // 2. 룸을 업데이트 합니다.
        UpdateRoom(roomList);

        // 3. 룸을 생성합니다.
        CreateRoomObject();
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
            // 해당 이름이 roomDictionary의 key 값으로 설정되어 있다면,
            if (roomDictionary.ContainsKey(roomList[i].Name))
            {
                // RemovedFromList : (true) 룸에서 삭제되었을 때
                if (roomList[i].RemovedFromList)
                {
                    // roomDictionary에서도 삭제합니다.
                    roomDictionary.Remove(roomList[i].Name);
                }
                else // 삭제 되지 않았다면,
                {
                    // roomDictionary에 다시 넣어줍니다.
                    roomDictionary[roomList[i].Name] = roomList[i];
                }
            }
            else // key 값이 설정되어 있지 않다면,
            {
                // roomDictionary에 key값을 넣습니다.
                roomDictionary[roomList[i].Name] = roomList[i];
            }
        }
    }

    public void CreateRoomObject()
    {
        // roomDictionary에 여러 개의 Values값이 들어있다면, roomInfo에 넣어줍니다.
        foreach (RoomInfo roomInfo in roomDictionary.Values)
        {
            // room 오브젝트를 생성합니다.
            GameObject room = Instantiate(Resources.Load<GameObject>("Room"));

            // room 오브젝트의 부모 오브젝트를 설정합니다.
            room.transform.SetParent(roomParentTransform);

            // room에 대한 정보를 입력합니다.
            room.GetComponent<Information>().RoomData(roomInfo.Name, roomInfo.PlayerCount, roomInfo.MaxPlayers);
        }
    }
}