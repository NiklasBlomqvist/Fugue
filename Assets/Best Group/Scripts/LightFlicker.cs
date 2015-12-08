using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

    private Light theLight;
    private bool hasBeenTriggered = false;

	// Use this for initialization
	void Start () {
        theLight = GetComponent<Light>();

    }

    // Update is called once per frame
    void Update () {
	
	}

    public void flicker()
    {
        // Only flicker if the flashlight is on
        if (!hasBeenTriggered)
        {
            StartCoroutine(flickerWait());
            hasBeenTriggered = true;
        }

    }

    IEnumerator flickerWait()
    {

        yield return new WaitForSeconds(0.5f);
        theLight.enabled = !theLight.enabled;
        yield return new WaitForSeconds(0.3f);
        theLight.enabled = !theLight.enabled;
        yield return new WaitForSeconds(0.2f);
        theLight.enabled = !theLight.enabled;
        yield return new WaitForSeconds(0.7f);
        theLight.enabled = !theLight.enabled;
        yield return new WaitForSeconds(0.3f);
        theLight.enabled = !theLight.enabled;
        yield return new WaitForSeconds(0.7f);
        theLight.enabled = !theLight.enabled;

    }
}
