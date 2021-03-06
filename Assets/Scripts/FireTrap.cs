using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public bool isOn;
    private ParticleSystem particleSystem;
    public float fireTimer;
    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isOn)
        {
            StartCoroutine(turnOff(fireTimer));
        }
        else
        {
            StartCoroutine(turnOn(fireTimer));
        }
    }

    private IEnumerator turnOn(float timer)
    {
        yield return new WaitForSeconds(timer);
        particleSystem.Play();
        isOn = true;
    }
    private IEnumerator turnOff(float animationLength)
    {
        
        yield return new WaitForSeconds(animationLength);
        particleSystem.Stop();
        isOn = false;
    }

}
