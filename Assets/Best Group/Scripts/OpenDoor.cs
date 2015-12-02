using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {
    bool isOpen = false;
    public bool locked = false;
    AudioSource doorOpen;
    AudioSource doorClose;

    // Use this for initialization

    void Start () {
        AudioSource[] audios = GetComponents<AudioSource>();
        doorOpen = audios[0];
        doorClose = audios[1];
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void openDoor()
    {
        // Open door
        if(locked == false && isOpen == false && !GetComponent<Animation>().IsPlaying("OpenDoor"))
        {

            // Sets time to start animation, what speed and then plays it
            GetComponent<Animation>()["OpenDoor"].time = 0.0f;
            GetComponent<Animation>()["OpenDoor"].speed = 1.0f;
            GetComponent<Animation>().Play();

            doorOpen.Play();

            isOpen = true;
        }

        // Close door
        else if (locked == false && isOpen == true && !GetComponent<Animation>().IsPlaying("OpenDoor"))
        {

            GetComponent<Animation>()["OpenDoor"].time = GetComponent<Animation>()["OpenDoor"].length;
            GetComponent<Animation>()["OpenDoor"].speed = -1.0f;
            GetComponent<Animation>().Play();

            doorClose.PlayDelayed(0.25f);

            isOpen = false;
        }
    }

    // Close door quickly (slam door)
    public void closeThisDoor()
    {
        if (isOpen && !locked)
        {
            GetComponent<Animation>()["OpenDoor"].time = GetComponent<Animation>()["OpenDoor"].length;
            GetComponent<Animation>()["OpenDoor"].speed = -3.0f;
            locked = true;
            GetComponent<Animation>().Play();
        }
        else
        {
            locked = true;
        }
    }

    public void unlockDoor()
    {
        locked = false;
    }

    public bool isLocked()
    {
        return locked;
    }
}
