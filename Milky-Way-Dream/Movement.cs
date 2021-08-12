using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //cached reference
    Rigidbody myRigidBody;
    AudioSource myAudioSource;

    //config 
    [SerializeField] float mainThrust = 5f;
    [SerializeField] float sideThrust = 20f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            myRigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);

            if(!myAudioSource.isPlaying)
            {
                PlaySFX();
            }

            if (!mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Play();
            }
            
        }

        else

        {
            myAudioSource.Stop();
            mainEngineParticles.Stop();
        }


    }
    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApllyThrust(-sideThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApllyThrust(sideThrust);
        }
    }

    private void ApllyThrust(float rotationThisFrame)
    {
        myRigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        myRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }

    private void PlaySFX()
    {
        myAudioSource.PlayOneShot(mainEngine);
    }
}

    
