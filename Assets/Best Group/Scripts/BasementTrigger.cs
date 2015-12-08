using UnityEngine;
using System.Collections;

public class BasementTrigger : MonoBehaviour {
    private bool triggerHappened = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if (!triggerHappened)
        {
            GameObject.Find("Flashlight").GetComponent<Flashlight>().flicker();
            GameObject.Find("BasementDoor2").GetComponent<OpenDoor>().closeThisDoor();
            GameObject.Find("BasementDoor2").GetComponent<OpenDoor>().unlockDoor();
            triggerHappened = true;

            StartCoroutine(waitToDisappear());

        }

    }

    IEnumerator waitToDisappear()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("Man").SetActive(false);

    }

}
