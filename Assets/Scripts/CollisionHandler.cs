using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    // Runs On Collision
    void OnCollisionEnter(Collision other) 
    {
        // Switch on other.gameObject.tag
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            case "LandingPad":
                Debug.Log("LandingPad");
                break;
            default:
                ReloadSceneOnDeath();
                break;
        }
    }

    // Reload Level Method
    void ReloadSceneOnDeath()
    {
        // Loads Scene with value of currentSceneIndex
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
