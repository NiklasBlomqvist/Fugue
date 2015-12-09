using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {
    private Light flashlight;
    private bool flashlightOn = false;
    //AudioSource lightOn;
    //AudioSource lightOff;
    public AudioSource audioSource;
    private AudioClip lightOn;
    private AudioClip lightOff;
    private AudioClip flickerOn;
    private AudioClip flickerOff;

    private bool flashlightFunctioning = true;

    // Use this for initialization
    void Start () {
        // Gets the flashlight
        flashlight = GameObject.Find("Flashlight").GetComponent<Light>();
        lightOn = Resources.Load<AudioClip>("flashlightOn");
        lightOff = Resources.Load<AudioClip>("flashlightOff");
        flickerOn = Resources.Load<AudioClip>("flickerOn");
        flickerOff = Resources.Load<AudioClip>("flickerOff");
    }

    // Update is called once per frame
    void Update () {
        // Turning on/off flashlight
        if (Input.GetButtonDown("Flashlight"))
        {
            // If you have a flashlight in your inventory
            if (GameObject.Find("Inventory").GetComponent<Inventory>().hasFlashlight() && flashlightFunctioning)
            {
                // Toggle the flashlight
                flashlight.enabled = !flashlight.enabled;
                flashlightOn = !flashlightOn;

                if(flashlight.enabled)
                {
                    audioSource.clip = lightOn;
                    audioSource.Play();
                }
                else
                {
                    audioSource.clip = lightOff;
                    audioSource.Play();
                }
            }

        }
    }

    public void flicker()
    {
        // Only flicker if the flashlight is on
        if(flashlightOn)
        {
            StartCoroutine(flickerWait());
        }

    }

    IEnumerator flickerWait()
    {

        flashlight.enabled = !flashlight.enabled;
        audioSource.PlayOneShot(flickerOff);
        yield return new WaitForSeconds(0.8f);

        flashlight.enabled = !flashlight.enabled;
        audioSource.PlayOneShot(flickerOn);
        yield return new WaitForSeconds(0.1f);

        flashlight.enabled = !flashlight.enabled;
        audioSource.PlayOneShot(flickerOff);
        yield return new WaitForSeconds(0.8f);

        flashlight.enabled = !flashlight.enabled;
        audioSource.PlayOneShot(flickerOn);
        yield return new WaitForSeconds(0.1f);

        flashlight.enabled = !flashlight.enabled;
        audioSource.PlayOneShot(flickerOff);
        yield return new WaitForSeconds(0.2f);

        flashlight.enabled = !flashlight.enabled;
        audioSource.PlayOneShot(flickerOn);
        yield return new WaitForSeconds(0.3f);

        flashlight.enabled = !flashlight.enabled;
        audioSource.PlayOneShot(flickerOff);
        yield return new WaitForSeconds(0.8f);

        flashlight.enabled = !flashlight.enabled;
        audioSource.PlayOneShot(flickerOn);
        yield return new WaitForSeconds(0.1f);

        flashlight.enabled = !flashlight.enabled;
        audioSource.PlayOneShot(flickerOff);
        yield return new WaitForSeconds(0.8f);

        flashlight.enabled = !flashlight.enabled;
        audioSource.PlayOneShot(flickerOn);
        yield return new WaitForSeconds(0.1f);



        /*
        flashlight.enabled = !flashlight.enabled;
        audioSource.PlayOneShot(flickerOff);
        yield return new WaitForSeconds(0.8f);

        flashlight.enabled = !flashlight.enabled;
        audioSource.PlayOneShot(flickerOn);
        yield return new WaitForSeconds(0.6f);

        flashlight.enabled = !flashlight.enabled;
        audioSource.PlayOneShot(flickerOff);
        yield return new WaitForSeconds(0.3f);

        flashlight.enabled = !flashlight.enabled;
        audioSource.PlayOneShot(flickerOn);
        yield return new WaitForSeconds(0.4f);

        flashlight.enabled = !flashlight.enabled;
        audioSource.PlayOneShot(flickerOff);
        yield return new WaitForSeconds(0.6f);

        flashlight.enabled = !flashlight.enabled;
        audioSource.PlayOneShot(flickerOn);
        */

    }

    public bool isOn()
    {
        return flashlightOn;
    }

    public void disableFlashlight()
    {
        if(flashlightOn)
        {
            // Toggle the flashlight
            flashlight.enabled = !flashlight.enabled;
            flashlightOn = !flashlightOn;

            flashlightFunctioning = false;
        }

    }

    public void enableFlashlight()
    {
        if (!flashlightOn)
        {
            // Toggle the flashlight
            flashlight.enabled = !flashlight.enabled;
            flashlightOn = !flashlightOn;

            flashlightFunctioning = true;
        }
    }
}
