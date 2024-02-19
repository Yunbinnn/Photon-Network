using Photon.Pun;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class NickName : MonoBehaviourPun
{
    GetAccountInfoResult getAccountInfoResult;

    [SerializeField] TextMeshProUGUI nickName;
    [SerializeField] Camera playerCamera;

    void Start()
    {
        Debug.Log("Playfab ID : ");

        Debug.Log("Photon Network ID : " + photonView.Owner.NickName);

        nickName.text = photonView.Owner.NickName;
    }

    private void Update()
    {
        transform.forward = playerCamera.transform.forward;
    }
}