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
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().PlayMusic();
    }
    public void SaveGame()
    {
        SaveSystem.SaveGameLevel(gameManager);
        SceneManager.LoadScene("MainScene");
        GameManager.Instance.gamePaused = false;
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().PlayMusic();
    }

    public void LoadGame()
    {
        PlayData data = SaveSystem.LoadData();
        gameManager.gameLevel = data.gameLevel;
        gameManager.playerHealth = data.health;
        gameManager.bulletCount = data.bulletCount;
        SceneManager.LoadScene("Game");
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().StopMusic();
    }

    public void NewGame()
    {
        gameManager.gameLevel = GameLevel.LEVEL1;
        gameManager.playerHealth = 5;
        gameManager.bulletCount = 10;
        SceneManager.LoadScene("Game");
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().StopMusic();
    }
}
