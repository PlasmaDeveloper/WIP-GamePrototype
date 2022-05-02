using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //objects and components
    [SerializeField] Transform playerTransform;

    //values
    float mouseSensitivity = 120.0f;    //for turning and looking up and down for example.
    float rotationAxisX = 0.0f;     //to get the look up/down value
    float lockView = 85.0f;     //how far up/down the player can look


    // Start is called before the first frame update
    void Start()
    {
        LockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        MouseRotateVision();
    }

    //keeps cursor locked, so the mouse dont moves around ==> !!Deactivate in inventory!!
    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    //rotates the player and the camera based on the mouse movement
    void MouseRotateVision()
    {
        float mouseAxisX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseAxisY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationAxisX -= mouseAxisY;
        //clamp the maximal look up/down rotation
        rotationAxisX = Mathf.Clamp(rotationAxisX, -lockView, lockView);

        transform.localRotation = Quaternion.Euler(rotationAxisX, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseAxisX);
    }
}
