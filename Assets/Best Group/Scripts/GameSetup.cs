using UnityEngine;
using System.Collections;

public class GameSetup : MonoBehaviour {

    private bool shown;
    private ModalPanel modalPanel;

    // Use this for initialization
    void Awake () {
        GameObject.Find("Plane").GetComponent<Fade>().fadeIn(3f, false);
        modalPanel = ModalPanel.Instance();
        shown = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.U))
        {

            modalPanel.Choice("Testing", 1f, 1f);

        }

    }
}
