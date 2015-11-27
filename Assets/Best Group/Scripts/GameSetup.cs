using UnityEngine;
using System.Collections;

public class GameSetup : MonoBehaviour {

    private PopUpScript popUpScript;
    private bool shown;

    // Use this for initialization
    void Start () {
        GameObject.Find("Plane").GetComponent<Fade>().fadeIn(3f, false);
        popUpScript = GameObject.Find("PopUp").GetComponent<PopUpScript>();
        popUpScript.Hide();
        shown = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.U))
        {

            if (!shown)
            {
                popUpScript.Show("test");
                Debug.Log("show");
                shown = true;
            }
            else
            {
                popUpScript.Hide();
                Debug.Log("hide");
                shown = false;
            }

        }

    }
}
