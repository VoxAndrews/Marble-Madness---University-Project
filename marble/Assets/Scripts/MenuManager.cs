using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //Adds the Text Mesh Pro Library
using UnityEngine.SceneManagement; //Adds functionality so we can Load Scenes

public class MenuManager : MonoBehaviour
{
    [Header("UI Game Objects")]
    [SerializeField] //Allows me to set the object through the Inspector
    GameObject resultScreenObj; //The Result Screen canvas object
    [SerializeField] //Allows me to set the object through the Inspector
    GameObject gameOverScreenObj; //The Game Over Screen canvas object
    [SerializeField] //Allows me to set the object through the Inspector
    GameObject inGameScreenObj; //The In-Game Screen canvas object 
    [SerializeField] //Allows me to set the object through the Inspector
    GameObject mainMenuScreenObj; //The Main Menu Screen canvas object

    [Header("Text Variables")] //Adds a header to the Inspector
    [SerializeField] //Allows me to set the object through the Inspector
    TextMeshProUGUI levelNameMesh; //The Level Name Text Mesh Object
    [SerializeField] //Allows me to set the object through the Inspector
    TextMeshProUGUI currentScore; //The //The Current Score Text Mesh Object
    [SerializeField] //Allows me to set the object through the Inspector
    TextMeshProUGUI totalCollectablesMesh; //The Total Amount of Collectables Text Mesh Object
    [SerializeField] //Allows me to set the object through the Inspector
    TextMeshProUGUI collectedCollectablesMesh; //The Collected Amount of Collectables Text Mesh Object
    [SerializeField] //Allows me to set the object through the Inspector
    TextMeshProUGUI totalScore; //The Total Score Text Mesh Object

    [SerializeField] //Allows me to set the object through the Inspector
    string levelName = "Error: No Name Set"; //The name of the current level

    [Header("Main Menu Toggle")] //Adds a header to the Inspector
    [SerializeField] //Allows me to set the object through the Inspector
    bool startMenu = false; //This varibale tells the system whether we are in the Main Menu or not

    int totalCollectables; //The Total amount of Collectables in a scene
    int collectedCollectables; //The currently collected amount of Collectables in a scene
    int currentPoints; //The current amount of points in the scene

    static int totalPoints = 0; //The Total amount of points across all scenes
    static int levelCount = 1; //The Current Level Count (Set to 1 by Default)

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(levelCount);
        
        if(startMenu == false) //If we are not in the Main Menu, initialise these variables
        {
            totalCollectables = 0;
            collectedCollectables = 0;
            currentPoints = 0;

            totalCollectables = GameObject.FindGameObjectsWithTag("Collectable").Length; //Finds the total amount of objects with the tag 'Collectable' and sets totalCollectables

            totalCollectablesMesh.SetText(totalCollectables.ToString()); //Sets the totalCollectablesMesh object to be equal to totalCollectables

            if (levelName == "") //Checks to see if the levelname is empty, if it is, it will set it
            {
                levelName = "Error: No Name Set"; //Sets a default String 
            }

            levelNameMesh.SetText(levelName); //Sets the levelNameMesh to be equal to levelName

            if (resultScreenObj == null) //Checks to see if the resultScreenObj is empty, if it is, it will set it
            {
                resultScreenObj = GameObject.Find("Results Screen"); //Finds an Object with the name 'Results Screen'

                resultScreenObj.SetActive(false); //Sets the Object to Inactive
            }

            if (gameOverScreenObj == null) //Checks to see if the gameOverScreenObj is empty, if it is, it will set it
            {
                gameOverScreenObj = GameObject.Find("Game Over Screen"); //Finds an Object with the name 'Game Over Screen'

                inGameScreenObj.SetActive(false); //Sets the Object to Inactive
            }

            if (inGameScreenObj == null) //Checks to see if the inGameScreenObj is empty, if it is, it will set it
            {
                inGameScreenObj = GameObject.Find("In Game"); //Finds an Object with the name 'In Game'

                inGameScreenObj.SetActive(true); //Sets the Object to Active
            }
        }
        else //If we are in the Main Menu, initialise these variables
        {
            if (mainMenuScreenObj == null) //Checks to see if the mainMenuScreenObj is empty, if it is, it will set it
            {
                mainMenuScreenObj = GameObject.Find("Main Menu Screen"); //Finds an Object with the name 'Main Menu Screen'

                mainMenuScreenObj.SetActive(true); //Sets the Object to Active
            }
        }
    }

    public void UpdateCollectTotal() //Updates the current amount of Objects Collected
    {
        collectedCollectables++; //Adds +1 to the amount of objects colleted

        collectedCollectablesMesh.SetText(collectedCollectables.ToString()); //Stes the collectedCollectablesMesh object to be the value of collectedCollectables
    }

    public void UpdateScore(int score) //Updates the Current Score and the Total Score (Takes a 'score' value from 'Collectable.cs')
    {
        currentPoints += score; //Adds the amount from a Collectable Object to the currentPoints value

        totalPoints += score; //Adds the currentPoints value to the totalPoints value

        currentScore.SetText(currentPoints.ToString()); //Sets the currentScore object to be the currentPoints value
    }

    public void WinOrLoad() //Tells the game to either display the results screen or load a new level
    {
        if (levelCount == (SceneManager.sceneCountInBuildSettings - 1)) //Checks to see if the current LevelCount value is equal to the number of scenes in the build minus 1
        {
            Debug.Log("Displaying Results Screen");

            ResultsScreen(); //Runs the 'ResultsScreen' function the number of scenes in the build minus 1, Load a new Level
        }
        else //If the current levelCount value is not equal to 
        {
            Debug.Log("Loading Level");

            LoadLevel(); //Runs the 'LoadLevel' function
        }
    }
    
    public void ResultsScreen() //Displays the Results Screen
    {
        DeactivateScene(); //Deactivates Player functions

        resultScreenObj.SetActive(true); //Sets the resultScreenObj Object to Active

        totalScore.SetText(totalPoints.ToString()); //Sets the totalScore to be equal to the totalPoints variable
    }

    public void GameOverScreen()
    {
        DeactivateScene(); //Deactivates Player functions

        gameOverScreenObj.SetActive(true); //Sets the gameOverScreenObj Object to Active

        totalScore.SetText(totalPoints.ToString());

        FindObjectOfType<PlayerController>().ResetLives(); //Resets the Players Total amount of lives
    }

    public void DeactivateScene() //Deactivates Player Movement, Physics and Animation
    {
        Debug.Log("Deactivating Scene");

        Time.timeScale = 0; //Freezes time so game stops

        inGameScreenObj.SetActive(false);
    }

    public void RestartGame() //Restarts the game to the First Level
    {
        Debug.Log("Restarting Game");

        totalCollectables = 0;
        collectedCollectables = 0;
        currentPoints = 0;
        totalPoints = 0;

        levelCount = 1; //Sets the Level Count back to the Beggining

        SceneManager.LoadScene("marbleLevel1"); //Restarts the entire game

        Time.timeScale = 1; //Freezes time so game stops
    }

    public void LoadLevel() //Loads a new level
    {
        currentPoints = 0; //Sets the current amount of Points to 0

        Time.timeScale = 1; //Freezes time so game stops

        if (SceneManager.GetActiveScene().name == "menu") //If the current Scene is the menu, load the first level
        {
            SceneManager.LoadScene("marbleLevel1"); //Loads the First Level, named 'marbleLevel1'
        }
        else //If the current Scene is not the menu, load the next level
        {
            levelCount++; //Adds +1 to the levelCount variable

            Debug.Log("Loading marbleLevel" + levelCount);

            if (SceneManager.GetSceneByName("marbleLevel" + levelCount) != null) //If the next Scene is not equal to Null, load it
            {
                SceneManager.LoadScene("marbleLevel" + levelCount); //Loads the next scene equal to 'marbleLevel' plus the current levelCount value
            }
            else //If the next Scene is equal to Null, load the Menu
                SceneManager.LoadScene("menu"); //Quits game to Menu
        }
    }

    public void QuitToMenu() //Quits to the Main Menu
    {
        Debug.Log("Quitting to Menu");

        SceneManager.LoadScene("menu"); //Quits game to Menu

        levelCount = 1; //Sets the levelCount variable to 1;
    }

    public void QuitGame() //Quits the Game Entierly
    {
        Debug.Log("Quitting Game");

        Application.Quit(); //Quits the game
    }
}