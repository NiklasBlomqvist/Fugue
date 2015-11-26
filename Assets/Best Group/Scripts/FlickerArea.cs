using UnityEngine;
using System.Collections;

public class FlickerArea : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        GameObject.Find("Flashlight").GetComponent<Flashlight>().flicker();
    }
}
