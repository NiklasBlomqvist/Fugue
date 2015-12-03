using UnityEngine;
using System.Collections;

public class jumpScare : MonoBehaviour
{

    public Texture2D jumpScareImage;
    public float textureScale = 2;
    private bool scare = false;
    private AudioClip scream;
    public AudioSource audioSource;

    void OnGUI()
    {
        if (scare)
        {
            if (jumpScareImage != null)
            {
                GUI.DrawTexture(new Rect((Screen.width - jumpScareImage.width * textureScale) / 2, (Screen.height - jumpScareImage.height * textureScale) / 2, jumpScareImage.width * textureScale, jumpScareImage.height * textureScale), jumpScareImage);
            }
            else
            {
                Debug.Log("No crosshair texture set in the Inspector");
            }
        }
    }


    // Use this for initialization
    void Start()
    {
        scream = Resources.Load<AudioClip>("scream");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            scare = true;
            audioSource.clip = scream;
            audioSource.Play();
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        scare = false;
    }
}
