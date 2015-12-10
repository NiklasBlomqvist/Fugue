using UnityEngine;
using System.Collections;

public class BedroomDarknessTrigger : MonoBehaviour
{
    private float originalAmbientIntensity;
    private bool triggerHappened = false;
    public AudioSource audioSource;
    private AudioClip scream;
    private AudioClip breathing;

    private ModalPanel modalPanel;
    private EndPanel endPanel;



    // Use this for initialization
    void Start()
    {
        scream = Resources.Load<AudioClip>("scream");
        breathing = Resources.Load<AudioClip>("rampBreathing2");
        originalAmbientIntensity = RenderSettings.ambientIntensity;

        modalPanel = ModalPanel.Instance();
        endPanel = EndPanel.Instance();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider collider)
    {
        if(!triggerHappened)
        {
            // When the player enters the trigger area --> Lights go out and the door is closed
            RenderSettings.ambientIntensity = 0;
            //GameObject.Find("BedroomDoor").GetComponent<OpenDoor>().closeThisDoor();
            GameObject.Find("BedroomDoor").GetComponent<OpenDoor>().closeThisDoor();

            // GameObject.Find("Flashlight").GetComponent<Flashlight>().isOn()
            StartCoroutine(waitOpen());
            
        }

    }

    IEnumerator waitOpen()
    {
        triggerHappened = true;

        GameObject.Find("Flashlight").GetComponent<Flashlight>().flicker();

        yield return new WaitForSeconds(2.0f);

        // Death scene
        if (!GameObject.Find("Inventory").GetComponent<Inventory>().hasFlashlight())
        {
            audioSource.PlayOneShot(breathing, 0.8f);
            GameObject.Find("Wardrobe").GetComponent<AudioSource>().Play();
            modalPanel.Choice("Was that... the door to the wardrobe?", 2f, 1f);

            yield return new WaitForSeconds(12.0f);
            GameObject.Find("Camera").GetComponent<jumpScare>().scareStart();

            GameObject.Find("Plane").GetComponent<Fade>().fadeOut(3f, false);
            endPanel.ShowEndCard("I couldn't flee, I couldn't fight her... Now I'm stuck here with her... forever", 2f, 8f, 0f);

        }

        // You have a flashlight
        else
        {
            yield return new WaitForSeconds(10.0f);

            GameObject.Find("BedroomDoor").GetComponent<OpenDoor>().unlockDoor();
            GameObject.Find("BedroomDoor").GetComponent<OpenDoor>().openDoor();

            RenderSettings.ambientIntensity = originalAmbientIntensity;
        }


    }
}
