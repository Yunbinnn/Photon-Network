using Photon.Pun;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class NickName : MonoBehaviourPun
{
    [SerializeField] TextMeshProUGUI nickName;
    [SerializeField] Camera playerCamera;

    void Start()
    {
        nickName.text = photonView.Owner.NickName;
    }

    private void Update()
    {
        transform.forward = playerCamera.transform.forward;
    }
}