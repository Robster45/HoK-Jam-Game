using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform orientation;
    public Transform model;

    float xRot;
    float yRot;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * Options.Instance.xSens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * Options.Instance.ySens;

        // set the roations
        yRot += mouseX;
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f); // clamps vertical

        // Rotate the camera / model
        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
        model.rotation = Quaternion.Euler(0, yRot, 0);
    }
}
