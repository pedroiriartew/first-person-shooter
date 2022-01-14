using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCámara : MonoBehaviour
{

    private float mouseX = 0f, mouseY = 0f;
    private float sensibilidad = 130f;

    [SerializeField] private Transform myPlayer;

    private float rotacionEnX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        CameraMovement();
    }

    private void GetInput()
    {
        mouseX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

        rotacionEnX -= mouseY;
        rotacionEnX = Mathf.Clamp(rotacionEnX, -90f, 90f);
    }

    private void CameraMovement()
    {
        transform.localRotation = Quaternion.Euler(rotacionEnX, 0f, 0f);
        myPlayer.Rotate(Vector3.up * mouseX);
    }
}
