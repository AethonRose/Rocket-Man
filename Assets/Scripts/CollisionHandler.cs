using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float NextSceneDelay;
    [SerializeField] AudioClip CrashSound;
    [SerializeField] AudioClip SuccessSound;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Runs On Collision
    void OnCollisionEnter(Collision other) 
    {
        // If isTransitioning is true do nothing
        if (isTransitioning) 
        {
            return;
        }
        // Switch on other.gameObject.tag
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "LandingPad":
                // Call CompleteLevel on collision with LandingPad
                CompleteLevel();
                break;
            default:
                // Calling StartCrashSequence on default collision with Untagged tag
                StartCrashSequence();
                break;
        }
    }

    // Called on Default Collision - untagged tag
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(CrashSound);
        GetComponent<Movement>().enabled = false;

        // Using Invoke to cause Delay of 1 second
        Invoke("ReloadSceneOnDeath", 1f);
    }

    // Called on Collision with LandingPad
    void CompleteLevel()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(SuccessSound);
        GetComponent<Movement>().enabled = false;

        // Invoke LoadNextLevel with NextSceneDelay
        Invoke("LoadNextLevel", NextSceneDelay);
    }

    // Reloads Level On Collision with Untagged objects
    void ReloadSceneOnDeath()
    {
        // Loads Scene with value of currentSceneIndex
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    // Loads Next Level on Collision with LandingPad
    void LoadNextLevel()
    {
        // Set nextSceneIndex to currentScenebuildIndex + 1
        int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1);

        // If nextSceneIndex equals total sceneCountInBuildSetting set nextSceneIndex to 0
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        // LoadScene of nextSceneIndex
        SceneManager.LoadScene(nextSceneIndex);
    }
}
