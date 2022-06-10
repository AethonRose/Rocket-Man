using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    // Runs On Collision
    void OnCollisionEnter(Collision other) 
    {
        // Switch on other.gameObject.tag
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            case "LandingPad":
                LoadNextLevel();
                break;
            default:
                ReloadSceneOnDeath();
                break;
        }
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
