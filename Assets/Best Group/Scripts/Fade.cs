using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

    private float timerStart = 5f; //Just some default value just in case
    public float timer;
    bool isFadingIn = false;
    bool isFadingOut = false;
    bool ctrlIsRestricted = false;
    Color color;


	// Use this for initialization
	void Start () {
        timer = timerStart;
    }

    //Fades from black to scene
    public void fadeIn(float time, bool canCtrl)
    {
        timerStart = time;
        isFadingIn = true;

        color = GetComponent<Renderer>().material.color;
        color.a = 1;
        GetComponent<Renderer>().material.color = color;

        setCanCtrl(canCtrl);
}

    //Fades from scene to black
    public void fadeOut(float time, bool canCtrl)
    {
        timerStart = time;
        isFadingOut = true;

        color = GetComponent<Renderer>().material.color;
        color.a = 0;
        GetComponent<Renderer>().material.color = color;

        setCanCtrl(canCtrl);
    }

    private void setCanCtrl(bool on)
    {
        GameObject.Find("Player").GetComponent<PlayerSound> ().enabled = on;
        GameObject.Find("Camera").GetComponent<CameraMovement>().enabled = on;
        ctrlIsRestricted = !on;
    }

    // Update is called once per frame
    void Update () {  

        if (isFadingIn)
        {
            fadeInUpdate();
        }
        else if (isFadingOut)
        {
            fadeOutUpdate();
        }
        else
        {
            return;
        }
        
        
        
        


    }

    private void fadeInUpdate()
    {
        timer -= Time.deltaTime;
        color = GetComponent<Renderer>().material.color;
        color.a = timer / timerStart;
        GetComponent<Renderer>().material.color = color;

        if (timer <= 0)
        {
            isFadingIn = false;
            isFadingOut = false;
            timer = timerStart;
            setCanCtrl(true);
        }

    }

    private void fadeOutUpdate()
    {
        timer -= Time.deltaTime;
        color = GetComponent<Renderer>().material.color;
        color.a = 1 - (timer / timerStart);
        GetComponent<Renderer>().material.color = color;

        if (timer <= 0)
        {
            isFadingIn = false;
            isFadingOut = false;
            timer = timerStart;
        }

    }
}
