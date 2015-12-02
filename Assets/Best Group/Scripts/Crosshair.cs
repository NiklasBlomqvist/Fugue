﻿using UnityEngine;
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

            // If you target a door,  "doors_wing_a"
            if ((hitInfo.collider.name == "doors_wing_a") && Input.GetButtonDown("Interact"))
            {
                // If that door is the basement door
                if(hitInfo.collider.transform.parent.name == "BasementDoor")
                {
                    // And you have the key
                    if (GameObject.Find("Inventory").GetComponent<Inventory>().hasKeyBasement())
                    {
                        hitInfo.collider.GetComponentInParent<OpenDoor>().unlockDoor();
                    }
                }
                if (!hitInfo.collider.GetComponentInParent<OpenDoor>().isLocked())
                {
                    hitInfo.collider.GetComponentInParent<OpenDoor>().openDoor();
                }
                else
                {
                    // Play "cannot open sound"
                }
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
                modalPanel.Choice("A flashlight. This could become useful.", 3f, 1f);
                GameObject.Find("Inventory").GetComponent<Inventory>().pickupFlashlight();
            }

            // If you target the armchairs in the livingroom
            if ((hitInfo.collider.name == "Armchair_Livingroom") && Input.GetButtonDown("Interact"))
            {
                modalPanel.Choice("I remember I used to sit here.. why is there two chairs?", 3f, 1f);
                GameObject.Find("Light_Livingroom").GetComponent<LightFlicker>().flicker();
            }

            // If you target the knife on the desk in the workroom
            if ((hitInfo.collider.name == "Knife") && Input.GetButtonDown("Interact"))
            {
                GameObject.Find("Inventory").GetComponent<Inventory>().pickupKnife();
                GameObject.Find("Knife").SetActive(false);
                modalPanel.Choice("A knife..", 3f, 1f);
            }

            // If you target the wardrobe in the bedroom
            if ((hitInfo.collider.name == "Wardrobe") && Input.GetButtonDown("Interact"))
            {
                if(GameObject.Find("Inventory").GetComponent<Inventory>().hasKnife())
                {
                    GameObject.Find("Plane").GetComponent<Fade>().fadeOut(3f, false);
                }

                else
                {
                    modalPanel.Choice("There's something in there..", 3f, 1f);
                }
            }

            // If you target the right door in the desk in the workroom
            if ((hitInfo.collider.name == "Workroom_Desk_RightDoor") && Input.GetButtonDown("Interact"))
            {
                GameObject.Find("Inventory").GetComponent<Inventory>().pickupKeyBasement();
                modalPanel.Choice("A key. Where does this lead?", 3f, 1f);
            }

            // If you target the desk in the workroom
            if ((hitInfo.collider.name == "Workroom_Desk") && Input.GetButtonDown("Interact"))
            {
                modalPanel.Choice("I used to sit here and work..", 3f, 1f);
            }



        }
        else {

            // Updates the crosshair if the ray doesn't hit an object tagged "Clickable"
            crosshairTexture = crosshairTextureNonActive;
        }

    }
}
