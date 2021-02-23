using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelDelay = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    AudioSource audioSource;

    bool isTransitioning = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning){return;}
        
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }    
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        // todo add SFX upon crash
        audioSource.PlayOneShot(crash);
        // todo add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelDelay);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        // todo add SFX upon crash
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        // todo add particle effect upon victory
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelDelay);
    }
    private void LoadNextLevel()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
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
