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

    public void easyLevel()
    {
        GameManager.Instance.gameLevel = GameLevel.EASY;
        SceneManager.LoadScene("Game");
        GameManager.Instance.gameSetup();
    }
    public void normalLevel()
    {
        GameManager.Instance.gameLevel = GameLevel.NORMAL;
        SceneManager.LoadScene("Game");
        GameManager.Instance.gameSetup();
    }
    public void hardLevel()
    {
        GameManager.Instance.gameLevel = GameLevel.HARD;
        SceneManager.LoadScene("Game");
        GameManager.Instance.gameSetup();
    }
    public void extremeLevel()
    {
        GameManager.Instance.gameLevel = GameLevel.EXTREME;
        SceneManager.LoadScene("Game");
        GameManager.Instance.gameSetup();
    }
}
