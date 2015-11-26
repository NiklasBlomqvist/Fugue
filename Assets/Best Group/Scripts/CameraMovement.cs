using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 1.0f;
    private bool frozen = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        frozen = GameObject.Find("Player").GetComponent<PlayerController>().isFrozen();

        if(!frozen)
        {
            if (Input.GetAxis("Mouse X") != 0)
            {
                transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * mouseSensitivity, Space.World);
                transform.parent.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * mouseSensitivity, Space.World);
            }
            if (Input.GetAxis("Mouse Y") != 0)
            {
                transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, 0) * mouseSensitivity, Space.Self);
            }
        }
    }
}
