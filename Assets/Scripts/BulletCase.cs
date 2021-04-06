using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameManager.Instance.bulletCount += 5;
        }
    }
}
