using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    int currentScore;

    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Collectable":
                Debug.Log("Collected " + collision.gameObject.name + "! Adding 100 points");
                
                currentScore += 100;

                Debug.Log("Destroying " + collision.gameObject.name);

                Destroy(collision.gameObject);

                Debug.Log("Current Score is: " + currentScore);

                break;
        }
    }
}
