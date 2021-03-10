using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public bool hit;
    public GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            hit = true;
            GameObject particleEffect = Instantiate(effect, gameObject.transform.position, Quaternion.identity);
            particleEffect.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (hit)
        {
            GameManager.Instance.playerLife--;
        }
    }
}
