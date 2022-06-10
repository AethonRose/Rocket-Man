using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) 
    {
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
                Debug.Log("You Blew Up");
                break;
        }
    }
}
