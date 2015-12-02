using UnityEngine;
using System.Collections;

public class BedroomDarknessTrigger : MonoBehaviour
{
    private float originalAmbientIntensity;
    private bool triggerHappened = false;
    GameObject ghost;

    // Use this for initialization
    void Start()
    {
        originalAmbientIntensity = RenderSettings.ambientIntensity;
        ghost = GameObject.Find("Ghost");
        ghost.SetActive(false);
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

        ghost.SetActive(true);
        yield return new WaitForSeconds(1f);
        ghost.SetActive(false);

        yield return new WaitForSeconds(20.0f);

        if (GameObject.Find("Flashlight").GetComponent<Flashlight>().flashlightOn)
        {
            GameObject.Find("BedroomDoor").GetComponent<OpenDoor>().unlockDoor();
            GameObject.Find("BedroomDoor").GetComponent<OpenDoor>().openDoor();
        }
        else
        {
            GameObject.Find("Player").GetComponent<PlayerController>().restart();
        }

        RenderSettings.ambientIntensity = originalAmbientIntensity;
    }
}
