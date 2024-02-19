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
        // ���� �÷��̾ �� �ڽ��̶��,
        if (photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            temporaryCamera.enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Master Client");
            }
        }

        if(Input.GetKeyDown(KeyCode.V)) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        Movement();

        Rotation();
    }

    public void Movement()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        direction.Normalize();

        // TransformDirection : �ڱⰡ �ٶ󺸰� �ִ� �������� �̵��ϴ� �Լ��Դϴ�.
        transform.position += speed * Time.deltaTime * transform.TransformDirection(direction);
    }

    public void Rotation()
    {
        mouseX += rotateSpeed * Time.deltaTime * Input.GetAxisRaw("Mouse X");

        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }
}