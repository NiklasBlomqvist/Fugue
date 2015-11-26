using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
    private bool flashlight;

	// Use this for initialization
	void Start () {
        flashlight = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void pickupFlashlight()
    {
        flashlight = true;
    }

    public bool hasFlashlight()
    {
        return flashlight;
    }
}
