using UnityEngine;
using System.Collections;

// When entering the room at the end of the hallway in the basement
public class BasementTrigger2 : MonoBehaviour
{
    private bool triggerHappened = false;
    private float originalAmbientIntensity;

    // Use this for initialization
    void Start()
    {
        originalAmbientIntensity = RenderSettings.ambientIntensity;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (!triggerHappened)
        {

            GameObject.Find("BasementDoor2").GetComponent<OpenDoor>().closeThisDoor();
            triggerHappened = true;

            GameObject.Find("Camera").GetComponent<Crosshair>().setInteract(true);

            StartCoroutine(triggerDarkness());

        }

    }

    IEnumerator triggerDarkness()
    {
        yield return new WaitForSeconds(0.5f);

        RenderSettings.ambientIntensity = 0;

        // Disable the flashlight
        if (GameObject.Find("Inventory").GetComponent<Inventory>().hasFlashlight())
        {
            GameObject.Find("Flashlight").GetComponent<Flashlight>().disableFlashlight();
        }

        // Enable the point light on the chair
        GameObject.Find("HangLight").GetComponent<LightFlicker>().flicker();

    }

}