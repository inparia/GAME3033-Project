using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayData
{
    public GameLevel gameLevel;
    public int health, bulletCount;

    public PlayData(GameManager gameManager)
    {
        gameLevel = gameManager.gameLevel;
        health = gameManager.playerHealth;
        bulletCount = gameManager.bulletCount;
    }
}
