using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    int collectValue;

    float orig_y;

    [SerializeField]
    bool objFloat;

    [SerializeField]
    float floatSpeed;
    [SerializeField]
    [Range(0, 1)]
    float floatHeight;

    void Start() {
        orig_y = transform.position.y;
    }

    void OnTriggerEnter(Collider col) //Starts when something enters the Collectables Trigger
    {
        switch(col.gameObject.tag) //Checks the Object's Tag that just collided with the trigger
        {
            case "Player": //Checks to see if the Player hit the Colletable
                Debug.Log("Collected '" + gameObject.name + "'! Adding " + collectValue + " points");

                Debug.Log("Destroying " + gameObject.name);

                FindObjectOfType<MenuManager>().UpdateScore(collectValue); //Updates the Total/Current Score values in the MenuManager Script (Passes the Collectables Value)
                FindObjectOfType<MenuManager>().UpdateCollectTotal(); //Updates the Currently Collected total in the MenuManager Script

                Destroy(gameObject); //Destroys the current Collectable Object

                break; //Breaks from the Switch
        }
    }

    void Update() 
    {
        if(objFloat == true)
        {
            transform.position = new Vector3(transform.position.x, orig_y + (Mathf.Sin(Time.time * floatSpeed) * floatHeight), transform.position.z);
        }
    }
}
