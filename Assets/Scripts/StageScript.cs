using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScript : MonoBehaviour
{

    private TMPro.TextMeshProUGUI tMPro;
    public float timeRemaining;
    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = 3.0f;
        tMPro = GetComponent<TMPro.TextMeshProUGUI>();
        
        switch(GameManager.Instance.gameLevel)
        {
            case GameLevel.LEVEL1:
                tMPro.text = "Stage One";
                break;
            case GameLevel.LEVEL2:
                tMPro.text = "Stage Two";
                break;
            case GameLevel.LEVEL3:
                tMPro.text = "Stage Three";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
            timeRemaining = 3.0f;
        }
        
    }
}
