using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float nextSceneDelay;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool isCollisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheatManager();
    }

    // Runs On Collision
    void OnCollisionEnter(Collision other) 
    {
        // If isTransitioning or isCollisionDisabled is true do nothing
        if (isTransitioning || isCollisionDisabled) 
        {
            return;
        }
        // Switch on other.gameObject.tag
        switch (other.gameObject.tag)
        {
            case "Friendly":
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
    
    void CheatManager()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCollision();
        }
    }

    // Called on Collision with LandingPad
    void CompleteLevel()
    {
        isTransitioning = true;

        playAudioClip(successSound);

        successParticles.Play();
        
        GetComponent<Movement>().enabled = false;
        // Invoke LoadNextLevel with NextSceneDelay
        Invoke("LoadNextLevel", nextSceneDelay);
    }

    // Called on Default Collision - untagged tag
    void StartCrashSequence()
    {
        isTransitioning = true;

        playAudioClip(crashSound);

        crashParticles.Play();

        GetComponent<Movement>().enabled = false;
        // Using Invoke to cause Delay of 1 second
        Invoke("ReloadSceneOnDeath", 1f);
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
    
    void ToggleCollision()
    {
        isCollisionDisabled = !isCollisionDisabled;
        Debug.Log("isCollisionDisabled = " + isCollisionDisabled);
    }

    // Reloads Level On Collision with Untagged objects
    void ReloadSceneOnDeath()
    {
        // Loads Scene with value of currentSceneIndex
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    void playAudioClip(AudioClip sound)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(sound);
    }
}
