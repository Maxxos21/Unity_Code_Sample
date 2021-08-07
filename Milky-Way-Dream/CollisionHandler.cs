using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    //config parameters
    [SerializeField] float timeDelay = 1f;
    [SerializeField] AudioClip winSFX;
    [SerializeField] AudioClip explosionSFX;

    [SerializeField] ParticleSystem crash;
    [SerializeField] ParticleSystem smoke;



    //cached reff
    AudioSource myAudioSource;

    //state

    bool isTransitioning = false;


    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }



    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartNewLevel();
                break;
            case "Fuel":
                Debug.Log("Fuel Up");
                break;
            default:
                StartCrash();
                break;
        }
    }

    private void StartCrash()
    {
        isTransitioning = true;
        myAudioSource.PlayOneShot(explosionSFX);
        crash.Play();
        smoke.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", timeDelay);
    }

    private void StartNewLevel()
    {
        isTransitioning = true;
        myAudioSource.PlayOneShot(winSFX);
        //todo add particle 
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", timeDelay);
    }




    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
