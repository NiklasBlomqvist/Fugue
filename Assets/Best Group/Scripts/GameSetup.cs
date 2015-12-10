using UnityEngine;
using System.Collections;

public class GameSetup : MonoBehaviour {

    private bool shown;

    // Use this for initialization
    void Awake () {
        GameObject.Find("Plane").GetComponent<Fade>().fadeIn(2f, false);
        shown = false;
    }
	
	// Update is called once per frame
	void Update () {

    }
}
