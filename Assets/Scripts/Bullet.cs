using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody rigidbody;
    public bool shootRight;
    public float bulletSpeed = 25.0f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shootRight)
            rigidbody.velocity = Vector3.right * bulletSpeed;
        else
            rigidbody.velocity = Vector3.left * bulletSpeed;

        Destroy(gameObject, 3);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            other.GetComponent<WanderingAI>().enemyHealth--;
        }
    }
}
