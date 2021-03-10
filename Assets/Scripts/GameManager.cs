using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public enum GameLevel
{
    EASY,
    NORMAL,
    HARD,
    EXTREME
}
public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    #endregion


    public GameLevel gameLevel;
    public int playerLife;
    public float setSpawnTimer;
    public float survivedTimer;
    public bool gamePlay;
    public bool gamePaused;

    // Start is called before the first frame update
    void Start()
    {
        gamePlay = false;
        gamePaused = false;
    }


    public void gameSetup()
    {
        gamePlay = true;
        playerLife = 5;
        survivedTimer = 0;
        if (gameLevel == GameLevel.EASY)
            setSpawnTimer = 2;
        else if (gameLevel == GameLevel.NORMAL)
            setSpawnTimer = 1;
        else if (gameLevel == GameLevel.HARD)
            setSpawnTimer = 0.4f;
        else if (gameLevel == GameLevel.EXTREME)
            setSpawnTimer = 0.1f;

    }
    // Update is called once per frame
    void Update()
    {
        if (gamePlay)
        {
            survivedTimer += Time.deltaTime;
        }

        if(playerLife <= 0 && gamePlay)
        {
            gamePlay = false;
            SceneManager.LoadScene("LoseScene");
        }
    }


    
}
