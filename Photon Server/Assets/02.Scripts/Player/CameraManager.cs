using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float currentRotationX;
    [SerializeField] float sensitivity = 3f;
    readonly float cameraRotationLimit = 90;

    void Update()
    {
        RotateCamera();
    }

    public void RotateCamera()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");

        float cameraRotationX = xRotation * sensitivity;

        currentRotationX -= cameraRotationX;

        currentRotationX = Mathf.Clamp(currentRotationX, -cameraRotationLimit, cameraRotationLimit);

        transform.localEulerAngles = new Vector3(currentRotationX, 0, 0);
    }
}