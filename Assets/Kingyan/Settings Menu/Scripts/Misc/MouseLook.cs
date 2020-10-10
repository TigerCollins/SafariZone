using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//author: Kingyan
/// <summary>
/// code for camera movement
/// </summary>
public class MouseLook1 : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    //if cursor is locked
    public bool lockCursor = true;
    //key that activates the settings menu
    public KeyCode settingsMenuKey;
    public GameObject settingsMenu;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        settingsMenuKey = PlayerPrefsExtra.GetKeyCode("settingsMenu", KeyCode.Escape);

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } 
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown(settingsMenuKey))
        {
            settingsMenu.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
