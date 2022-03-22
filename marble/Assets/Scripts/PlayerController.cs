using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    MenuManager ui; //The UI Object handled by the MenuManager

    public bool fallOut; //A Boolean to tell the game whether the player has fallen out of the stage or not

    Vector3 spawnPosition; //The position the player was at when starting the game

    static int totalLives = 3; //The total amount of lives the player has before a Game Over

    CameraFollow cam;

    void Awake()
    {
        ui = FindObjectOfType<MenuManager>();
        cam = FindObjectOfType<CameraFollow>();

        fallOut = false;

        spawnPosition = transform.position;
    }

    void OnTriggerEnter(Collider collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Win":
                Debug.Log("Stage Complete!");

                ui.WinOrLoad();

                break;
            case "Lose":
                Debug.Log("Fell out of stage! Resetting");

                fallOut = true;

                totalLives -= 1;

                if(totalLives > 0)
                {
                    StartCoroutine(PlayerReset());
                }
                else
                    ui.GameOverScreen();

                break;
        }
    }

    IEnumerator PlayerReset()
    {
        cam.followCam = false;
        
        yield return new WaitForSeconds(2);

        Debug.Log("Reset");

        transform.rotation = Quaternion.Euler(Vector3.zero);

        cam.followCam = true;

        cam.ResetRotation();

        Debug.Log(cam.followCam);

        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        fallOut = false;

        transform.position = spawnPosition;
    }

    public void ResetLives()
    {
        totalLives = 3;
    }
}
