using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Bumped into a Friendly object!");
                break;
            case "Finish":
                Debug.Log("Got to the Finish!");
                break;
            case "Fuel":
                Debug.Log("Picked up fuel!");
                break;
            default:
                Debug.Log("Bumped into an obstacle!");
                break;
        }
    }
}
