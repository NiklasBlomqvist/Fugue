using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

    public Texture2D crosshairTextureNonActive;
    public Texture2D crosshairTextureActive;

    private Texture2D crosshairTexture;
    public float crosshairScale = 1;

    // Information about what is hit
    private RaycastHit hitInfo;

    void OnGUI()
    {
        //if not paused
        if (Time.timeScale != 0)
        {
            if(crosshairTexture != null)
            {
                GUI.DrawTexture(new Rect((Screen.width - crosshairTexture.width * crosshairScale) / 2, (Screen.height - crosshairTexture.height * crosshairScale) / 2, crosshairTexture.width * crosshairScale, crosshairTexture.height * crosshairScale), crosshairTexture);
            }
            else
            {
                Debug.Log("No crosshair texture set in the Inspector");
            }

        }
    }

    // Use this for initialization
    void Start () {

        // Locks and hides the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initiate the crosshair
        crosshairTexture = crosshairTextureNonActive;
    }
	
	// Update is called once per frame
	void Update () {

        // Saves the vector the fps-controller is pointing at
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hitInfo, 2.0f) && hitInfo.collider.CompareTag("Clickable"))
        {
            // Updates the crosshair if the ray hits a object tagged "Clickable"
            crosshairTexture = crosshairTextureActive;

            // If you hit "doors_wing_a" open it
            if ((hitInfo.collider.name == "doors_wing_a") && Input.GetButtonDown("Interact"))
            {
                hitInfo.collider.GetComponentInParent<OpenDoor>().openDoor();
            }

            // If you target Cube1
            if ((hitInfo.collider.name == "Cube1") && Input.GetButtonDown("Interact"))
            {
                GameObject.Find("Player").GetComponent<PlayerController>().restart();
            }

            // If you target flickerCube
            if ((hitInfo.collider.name == "FlickerCube") && Input.GetButtonDown("Interact"))
            {
                print("FlickerCube");
            }

            // If you target FlashlightSphere
            if ((hitInfo.collider.name == "FlashlightSphere") && Input.GetButtonDown("Interact"))
            {
                GameObject.Find("Inventory").GetComponent<Inventory>().pickupFlashlight();
                print("You picked up a flashlight!");
            }
        }
        else {

            // Updates the crosshair if the ray doesn't hit an object tagged "Clickable"
            crosshairTexture = crosshairTextureNonActive;
        }

    }
}
