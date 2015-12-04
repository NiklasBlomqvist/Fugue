using UnityEngine;
using System.Collections;

public class BedroomDarknessTrigger : MonoBehaviour
{
    private float originalAmbientIntensity;
    private bool triggerHappened = false;
    public AudioSource audioSource;
    private AudioClip scream;
    private AudioClip breathing;
    

    // Use this for initialization
    void Start()
    {
        scream = Resources.Load<AudioClip>("scream");
        breathing = Resources.Load<AudioClip>("femaleBreathing1");
        originalAmbientIntensity = RenderSettings.ambientIntensity;
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
            audioSource.clip = breathing;
            audioSource.Play();

            yield return new WaitForSeconds(10.0f);
            GameObject.Find("Camera").GetComponent<jumpScare>().scareStart();

            GameObject.Find("Plane").GetComponent<Fade>().fadeOut(3f, false);

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
