using Photon.Pun;
using TMPro;
using UnityEngine;

public class NickName : MonoBehaviourPun
{
    [SerializeField] TextMeshProUGUI nickName;

    void Start()
    {
        nickName.text = photonView.Owner.NickName;
    }
}