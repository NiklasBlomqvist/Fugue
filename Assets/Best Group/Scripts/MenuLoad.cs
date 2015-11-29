using UnityEngine;
using System.Collections;

public class MenuLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
