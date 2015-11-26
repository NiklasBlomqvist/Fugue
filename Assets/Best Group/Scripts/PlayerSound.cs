using UnityEngine;
using System.Collections;

public class PlayerSound : MonoBehaviour
{

    private CharacterController controller;
    private AudioClip[] clips;
    public AudioSource audio;
    private Vector3 moveDirection = Vector3.zero;
    private float travelled = 0.0f;
    public float stepLength = 0.5f;


    // Use this for initialization
    void Start()
    {

        controller = GetComponent<CharacterController>();
        clips = new AudioClip[4];
        clips[0] = Resources.Load<AudioClip>("stepC1");
        clips[1] = Resources.Load<AudioClip>("stepC2");
        clips[2] = Resources.Load<AudioClip>("stepC3");
        clips[3] = Resources.Load<AudioClip>("stepC4");

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            stepLength = 0.3f;
        }
        else
        {
            stepLength = 0.5f;
        }

        if (controller.isGrounded)
        {
            moveDirection = this.transform.forward * Input.GetAxis("Vertical");
            moveDirection += this.transform.right * Input.GetAxis("Horizontal");
            moveDirection = moveDirection.normalized;

            travelled += moveDirection.magnitude * Time.deltaTime;
            if (travelled > stepLength)
            {
                int n = Random.Range(1, clips.Length);
                audio.clip = clips[n];
                audio.Play();
                travelled = 0;
                clips[n] = clips[0];
                clips[0] = audio.clip;
            }
        }

    }
}
