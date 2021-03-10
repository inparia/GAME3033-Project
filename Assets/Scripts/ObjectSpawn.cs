using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    public GameObject objToSpawn;

    private float timeRemaining;
    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = GameManager.Instance.setSpawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            if (!GameManager.Instance.gamePaused)
            {
                var instantiatedObj = Instantiate(objToSpawn, new Vector3(transform.position.x + Random.Range(-8, 9), 11, 0), Random.rotation);
                Destroy(instantiatedObj, 2);
                timeRemaining = GameManager.Instance.setSpawnTimer;
            }
        }

        
    }
}
