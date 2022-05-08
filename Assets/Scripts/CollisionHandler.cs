using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Bumped into a Friendly object!");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        audio.PlayOneShot(success);
        DisableMovement();
        Invoke("LoadNextLevel", loadDelay);
    }

    void StartCrashSequence()
    {
        audio.PlayOneShot(crash);
        DisableMovement();
        Invoke("ReloadLevel", loadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void DisableMovement()
    {
        GetComponent<Movement>().enabled = false;
    }
}
