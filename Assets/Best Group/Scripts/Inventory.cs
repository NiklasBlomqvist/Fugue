using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
    private bool flashlight;
    private bool knife;

	// Use this for initialization
	void Start () {
        flashlight = false;
        knife = false;
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

    public void pickupKnife()
    {
        knife = true;
    }

    public bool hasKnife()
    {
        return knife;
    }
}
