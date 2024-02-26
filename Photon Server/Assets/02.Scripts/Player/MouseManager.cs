using UnityEngine;

public enum MOUSETYPE
{
    LOCK,
    NONE,
}

public class MouseManager : MonoBehaviour
{
    void Start()
    {
        SetMouse(MOUSETYPE.LOCK);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetMouse(MOUSETYPE.NONE);
        }
    }

    public void LockMouse()
    {
        SetMouse(MOUSETYPE.LOCK);
    }

    public void SetMouse(MOUSETYPE type)
    {
        switch (type)
        {
            case MOUSETYPE.LOCK:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;

            case MOUSETYPE.NONE:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }
}