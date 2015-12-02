using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {
    bool isOpen = false;
    public bool locked = false;
    public AudioSource audioSource;
    private AudioClip doorOpenSound;
    private AudioClip doorCloseSound;
    private AudioClip doorSlamSound;

    // Use this for initialization

    void Start () {
        doorOpenSound = Resources.Load<AudioClip>("doorOpen");
        doorCloseSound = Resources.Load<AudioClip>("doorClose");
        doorSlamSound = Resources.Load<AudioClip>("doorSlam");
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

            audioSource.clip = doorOpenSound;
            audioSource.Play();

            isOpen = true;
        }

        // Close door
        else if (locked == false && isOpen == true && !GetComponent<Animation>().IsPlaying("OpenDoor"))
        {

            GetComponent<Animation>()["OpenDoor"].time = GetComponent<Animation>()["OpenDoor"].length;
            GetComponent<Animation>()["OpenDoor"].speed = -1.0f;
            GetComponent<Animation>().Play();

            audioSource.clip = doorCloseSound;
            audioSource.PlayDelayed(0.35f);

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

            audioSource.clip = doorSlamSound;
            audioSource.Play();
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
