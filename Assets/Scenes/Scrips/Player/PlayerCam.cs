using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    private float _xMouse;
    private float _yMouse;
    private float xRotation = 0;
    private float yRotation = 0;

    [Range(1f, 1000f)] [SerializeField] private float SenMouse;
    [SerializeField] private Transform _orientation;

    // Start is called before the first frame update
   /* void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }*/

    // Update is called once per frame
    void Update()
    {
        _xMouse = Input.GetAxisRaw("Mouse X") * SenMouse * Time.deltaTime;
        _yMouse = Input.GetAxisRaw("Mouse Y") * SenMouse * Time.deltaTime;

        xRotation = Mathf.Lerp(xRotation, xRotation - _yMouse, Time.deltaTime * 8f);
        yRotation = Mathf.Lerp(yRotation, yRotation + _xMouse, Time.deltaTime * 8f);

        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        this.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        _orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
