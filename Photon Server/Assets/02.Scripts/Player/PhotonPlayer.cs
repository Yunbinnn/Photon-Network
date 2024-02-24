using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonPlayer : MonoBehaviourPun
{
    [SerializeField] float speed;
    [SerializeField] float mouseX;
    [SerializeField] float rotateSpeed;

    [SerializeField] Vector3 direction;
    [SerializeField] Camera temporaryCamera;

    void Start()
    {
        // 현재 플레이어가 나 자신이라면,
        if (photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            temporaryCamera.enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        Movement();

        Rotation();
    }

    public void Movement()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        direction.Normalize();

        // TransformDirection : 자기가 바라보고 있는 방향으로 이동하는 함수입니다.
        transform.position += speed * Time.deltaTime * transform.TransformDirection(direction);
    }

    public void Rotation()
    {
        mouseX += rotateSpeed * Time.deltaTime * Input.GetAxisRaw("Mouse X");

        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }
}