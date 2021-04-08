using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isOn, damageGiven;
    public float nextDamage;
    void Start()
    {
        damageGiven = false;
        nextDamage = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        isOn = GetComponentInChildren<FireTrap>().isOn;

        if(damageGiven)
        {
            if(nextDamage > 0)
            {
                nextDamage -= Time.deltaTime;
            }
            else
            {
                nextDamage = 2.0f;
                damageGiven = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!damageGiven && isOn && !other.gameObject.GetComponent<Player>().getDead())
            {
                other.gameObject.GetComponent<Player>().playHurtSound();
                damageGiven = true;
                GameManager.Instance.playerHealth--;
            }
        }
    }
}
