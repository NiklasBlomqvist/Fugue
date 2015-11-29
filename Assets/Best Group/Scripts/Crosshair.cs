using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

    public Texture2D crosshairTextureNonActive;
    public Texture2D crosshairTextureActive;

    private Texture2D crosshairTexture;
    public float crosshairScale = 1;

    // Information about what is hit
    private RaycastHit hitInfo;

    private ModalPanel modalPanel;

    // Use this for initialization
    void Awake()
    {
        modalPanel = ModalPanel.Instance();
    }

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

        if (Physics.Raycast(transform.position, fwd, out hitInfo, 2.5f) && hitInfo.collider.CompareTag("Clickable"))
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

            // If you target bookStand_Hall
            if ((hitInfo.collider.name == "bookStand_Hall") && Input.GetButtonDown("Interact"))
            {
                modalPanel.Choice("I never read any books..", 2f, 1f);
            }

            // If you target Picture of woman in Guestroom
            if ((hitInfo.collider.name == "WomanPicture") && Input.GetButtonDown("Interact"))
            {
                modalPanel.Choice("Who is this woman? I remember.. something..", 2f, 1f);
            }

            // If you target a TableLamp -> turn on/off
            if ((hitInfo.collider.name == "TableLamp") && Input.GetButtonDown("Interact"))
            {
                hitInfo.collider.GetComponentInChildren<Light>().enabled = !hitInfo.collider.GetComponentInChildren<Light>().enabled;
            }

            // If you target the Drawer in workroom (get flashlight)
            if ((hitInfo.collider.name == "FlashlightDrawer") && Input.GetButtonDown("Interact"))
            {
                modalPanel.Choice("You picked up a flashlight. Press 'F' to use it. ", 3f, 1f);
                GameObject.Find("Inventory").GetComponent<Inventory>().pickupFlashlight();
            }

        }
        else {

            // Updates the crosshair if the ray doesn't hit an object tagged "Clickable"
            crosshairTexture = crosshairTextureNonActive;
        }

    }
}
