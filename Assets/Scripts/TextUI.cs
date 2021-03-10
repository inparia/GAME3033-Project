using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    public Text healthText, timeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "X " + GameManager.Instance.playerLife;
        timeText.text = "Time : " + ((int)GameManager.Instance.survivedTimer).ToString();
    }
}
