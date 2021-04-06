using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public enum GameLevel
{
    LEVEL1,
    LEVEL2,
    LEVEL3
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

    public bool gamePaused;
    public GameLevel gameLevel;
    public int playerHealth, bulletCount;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 5;
        bulletCount = 10;
        gamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void delayScene(float delay, string SceneName)
    {
        StartCoroutine(LoadLevelAfterDelay(delay, SceneName));
    }
    IEnumerator LoadLevelAfterDelay(float delay, string SceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneName);
    }

}
