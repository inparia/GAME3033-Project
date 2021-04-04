using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveObject : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void exitGame()
    {
        SceneManager.LoadScene("MainScene");
        GameManager.Instance.gamePaused = false;
    }
    public void SaveGame()
    {
        SaveSystem.SaveGameLevel(gameManager);
        SceneManager.LoadScene("MainScene");
        GameManager.Instance.gamePaused = false;
    }

    public void LoadGame()
    {
        PlayData data = SaveSystem.LoadData();
        gameManager.gameLevel = data.gameLevel;
        gameManager.playerHealth = data.health;
        SceneManager.LoadScene("Game");
    }

    public void NewGame()
    {
        gameManager.gameLevel = GameLevel.LEVEL1;
        gameManager.playerHealth = 5;
        SceneManager.LoadScene("Game");
    }
}
