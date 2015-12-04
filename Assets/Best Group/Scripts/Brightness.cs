using UnityEngine;
using System.Collections;

public class Brightness : MonoBehaviour {

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.O))
        {
            RenderSettings.ambientIntensity += 0.01f;
        }
        if (Input.GetKey(KeyCode.L))
        {
            RenderSettings.ambientIntensity -= 0.01f;
        }
    }
}
