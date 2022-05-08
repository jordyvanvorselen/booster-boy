using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    AudioSource audio;

    bool isTransitioning = false;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Bumped into a Friendly object!");
                break;
            case "Finish":
                StartSequence(true);
                break;
            default:
                StartSequence(false);
                break;
        }
    }

    void StartSequence(bool succeeded)
    {
        isTransitioning = true;
        audio.Stop();
        audio.PlayOneShot(succeeded ? success : crash);
        DisableMovement();
        Invoke(succeeded ? "LoadNextLevel" : "ReloadLevel", loadDelay);
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
