using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void difficultySelect()
    {
        SceneManager.LoadScene("Difficulty");
    }
    public void instructionMenu()
    {
        SceneManager.LoadScene("Instruction");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void exitGame()
    {
        Application.Quit();
    }

}
