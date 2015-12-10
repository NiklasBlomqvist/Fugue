using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{

    public Texture2D crosshairTextureNonActive;
    public Texture2D crosshairTextureActive;

    private Texture2D crosshairTexture;
    public float crosshairScale = 1;

    // Information about what is hit
    private RaycastHit hitInfo;

    private ModalPanel modalPanel;
    private EndPanel endPanel;

    public AudioSource audioSource;
    private AudioClip lampOnSound;
    private AudioClip lampOffSound;
    private AudioClip doorLockedSound;
    private AudioClip itemPickupSound;
    private AudioClip keyPickupSound;
    private AudioClip knifePickupSound;
    private AudioClip singleBreathSound1;
    private AudioClip singleBreathSound2;

    // Values for the chair in the basement
    private int chairClicked = 0;
    private bool canIntChair = true;
    private bool canInteract = false;


    // Use this for initialization
    void Awake()
    {
        modalPanel = ModalPanel.Instance();
        endPanel = EndPanel.Instance();
        lampOnSound = Resources.Load<AudioClip>("lampOn");
        lampOffSound = Resources.Load<AudioClip>("lampOff");
        doorLockedSound = Resources.Load<AudioClip>("doorLocked");
        itemPickupSound = Resources.Load<AudioClip>("itemPickup");
        keyPickupSound = Resources.Load<AudioClip>("keyPickup1");
        knifePickupSound = Resources.Load<AudioClip>("knifePickup1");
        singleBreathSound1 = Resources.Load<AudioClip>("singleBreath1");
        singleBreathSound2 = Resources.Load<AudioClip>("singleBreath2");
    }

    void OnGUI()
    {
        //if not paused
        if (Time.timeScale != 0)
        {
            if (crosshairTexture != null)
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
    void Start()
    {

        // Locks and hides the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Initiate the crosshair
        crosshairTexture = crosshairTextureNonActive;
    }

    // Update is called once per frame
    void Update()
    {

        // Saves the vector the fps-controller is pointing at
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hitInfo, 2.5f) && hitInfo.collider.CompareTag("Clickable") && (RenderSettings.ambientIntensity > 0 || (RenderSettings.ambientIntensity == 0 && GameObject.Find("Flashlight").GetComponent<Flashlight>().isOn()) || canInteract))
        {
            // Updates the crosshair if the ray hits a object tagged "Clickable"
            crosshairTexture = crosshairTextureActive;

            // If you target a door,  "doors_wing_a"
            if ((hitInfo.collider.name == "doors_wing_a") && Input.GetButtonDown("Interact"))
            {
                // If that door is the basement door
                if (hitInfo.collider.transform.parent.name == "BasementDoor")
                {
                    // And you have the key
                    if (GameObject.Find("Inventory").GetComponent<Inventory>().hasKeyBasement())
                    {
                        hitInfo.collider.GetComponentInParent<OpenDoor>().unlockDoor();
                    }
                }
                if(hitInfo.collider.transform.parent.name == "BedroomDoor")
                {
                    modalPanel.Choice("I feel cold.. is she here?", 3f, 1f);
                }
                if (!hitInfo.collider.GetComponentInParent<OpenDoor>().isLocked())
                {
                    hitInfo.collider.GetComponentInParent<OpenDoor>().openDoor();
                }
                else
                {
                    // Play "cannot open sound"
                    audioSource.clip = doorLockedSound;
                    audioSource.Play();
                }
            }

            // If you target bookStand_Hall
            if ((hitInfo.collider.name == "bookStand_Hall") && Input.GetButtonDown("Interact"))
            {
                modalPanel.Choice("She used to read.", 2f, 1f);
            }

            // If you target Picture of woman in Guestroom
            if ((hitInfo.collider.name == "WomanPicture") && Input.GetButtonDown("Interact"))
            {
                modalPanel.Choice("This is her.", 2f, 1f);
                audioSource.PlayOneShot(singleBreathSound2, 0.3f);
            }

            // If you target a TableLamp -> turn on/off
            if ((hitInfo.collider.name == "TableLamp") && Input.GetButtonDown("Interact"))
            {
                if (!hitInfo.collider.GetComponentInChildren<Light>().enabled)
                {
                    audioSource.PlayOneShot(lampOnSound, 0.4f);
                }
                else
                {
                    audioSource.PlayOneShot(lampOffSound, 0.4f);
                }

                hitInfo.collider.GetComponentInChildren<Light>().enabled = !hitInfo.collider.GetComponentInChildren<Light>().enabled;
            }

            // If you target the Drawer in workroom (get flashlight)
            if ((hitInfo.collider.name == "FlashlightDrawer") && Input.GetButtonDown("Interact") && !GameObject.Find("Inventory").GetComponent<Inventory>().hasFlashlight())
            {
                modalPanel.Choice("A flashlight. This could become useful.", 3f, 1f);
                audioSource.clip = itemPickupSound;
                audioSource.Play();
                GameObject.Find("Inventory").GetComponent<Inventory>().pickupFlashlight();
            }

            // If you target the armchairs in the livingroom
            if ((hitInfo.collider.name == "Armchair_Livingroom") && Input.GetButtonDown("Interact"))
            {
                modalPanel.Choice("I remember I used to sit here.. with her..", 3f, 1f);
                GameObject.Find("Light_Livingroom").GetComponent<LightFlicker>().flicker();
                audioSource.PlayOneShot(singleBreathSound1, 0.3f);
            }

            // If you target the knife on the desk in the workroom
            if ((hitInfo.collider.name == "Knife") && Input.GetButtonDown("Interact"))
            {
                GameObject.Find("Inventory").GetComponent<Inventory>().pickupKnife();
                audioSource.PlayOneShot(knifePickupSound, 0.7f);
                GameObject.Find("Knife").SetActive(false);
                modalPanel.Choice("This is what I killed her with. Maybe now she can't keep me from leaving my own home..", 5f, 1f);

            }

            // If you target the wardrobe in the bedroom
            if ((hitInfo.collider.name == "Wardrobe") && Input.GetButtonDown("Interact"))
            {
                modalPanel.Choice("I hear something in there.. I remember a knife.. and blood.", 3f, 1f);
            }

            // If you target the right door in the desk in the workroom
            if ((hitInfo.collider.name == "Workroom_Desk_RightDoor") && Input.GetButtonDown("Interact") && !GameObject.Find("Inventory").GetComponent<Inventory>().hasKeyBasement())
            {
                GameObject.Find("Inventory").GetComponent<Inventory>().pickupKeyBasement();
                modalPanel.Choice("A key. Where does this lead?", 3f, 1f);
                audioSource.PlayOneShot(keyPickupSound, 0.8f);
            }

            // If you target the desk in the workroom
            if ((hitInfo.collider.name == "Workroom_Desk") && Input.GetButtonDown("Interact"))
            {
                modalPanel.Choice("I used to sit here and work..", 3f, 1f);
            }

            // If you target the chair in the basement 
            if ((hitInfo.collider.name == "HangChair") && Input.GetButtonDown("Interact"))
            {
                if (chairClicked == 0 && canIntChair)
                {
                    modalPanel.Choice("I need to get away from her.", 3f, 1f);
                    canIntChair = false;
                    StartCoroutine(chairClickedNext());

                }
                else if (chairClicked == 1 && canIntChair)
                {
                    modalPanel.Choice("Hanging myself is the only way.", 3f, 1f);
                    canIntChair = false;
                    StartCoroutine(chairClickedNext());

                }
                else if (chairClicked == 2 && canIntChair)
                {
                    modalPanel.Choice("No going back now..", 3f, 1f);
                    canIntChair = false;
                    StartCoroutine(chairClickedNext());
                }
                else if (chairClicked == 3 && canIntChair)
                {
                    GameObject.Find("Plane").GetComponent<Fade>().fadeOut(3f, false);
                    endPanel.ShowEndCard("I couldn't stand it anymore, being stuck in this house with her presence. They said 'until death do us part' and I hope my death will ensure that.", 0f, 8f, 0f);
                }

            }

            // If you target the front door
            if ((hitInfo.collider.name == "FrontDoor") && Input.GetButtonDown("Interact"))
            {
                if(GameObject.Find("Inventory").GetComponent<Inventory>().hasKnife())
                {
                    GameObject.Find("Plane").GetComponent<Fade>().fadeOut(3f, false);
                    endPanel.ShowEndCard("“I finally escaped..”", 2f, 8f, 0f);
                }
                else
                {
                    modalPanel.Choice("It's locked, I cannot escape..", 3f, 1f);
                    // Play "cannot open sound"
                    audioSource.clip = doorLockedSound;
                    audioSource.Play();
                }
            }

        }
        else
        {

            // Updates the crosshair if the ray doesn't hit an object tagged "Clickable"
            crosshairTexture = crosshairTextureNonActive;
        }

    }

    IEnumerator chairClickedNext()
    {
        yield return new WaitForSeconds(4.0f);
        chairClicked += 1;
        canIntChair = true;
    }

    public void setInteract(bool val)
    {
        canInteract = val;
    }
}
