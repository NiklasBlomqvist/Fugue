using UnityEngine;
using System.Collections;

public class GameSetup : MonoBehaviour {

    private bool shown;

    // Use this for initialization
    void Awake () {
        GameObject.Find("Plane").GetComponent<Fade>().fadeIn(1f, false);
        shown = false;
    }
	
	// Update is called once per frame
	void Update () {

    }
}
