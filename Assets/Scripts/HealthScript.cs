using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI tMPro;
    // Start is called before the first frame update
    void Start()
    {
        tMPro = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        tMPro.text = "X " + GameManager.Instance.playerHealth;
    }
}
