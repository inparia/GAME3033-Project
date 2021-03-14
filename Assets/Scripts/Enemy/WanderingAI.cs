using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WanderingAI : MonoBehaviour
{
    // Start is called before the first frame update
    public int wanderRadius;
    public float wanderTimer;
 
    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    private Animator animator;
    public NavMeshPath navMeshPath;
    // Use this for initialization
    void OnEnable () {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent> ();
        timer = wanderTimer;
        navMeshPath = new NavMeshPath();
    }
 
    // Update is called once per frame
    void Update () {

        var player = GameObject.FindGameObjectWithTag("Player");

        if (Mathf.Abs(player.transform.position.x - agent.transform.position.x) < 10)
        {
            agent.destination = player.transform.position;
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                if (newPos.x > transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                }
                else if (newPos.x < transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                }
                
                agent.SetDestination(new Vector3 (newPos.x, newPos.y, 0));
                timer = 0;
            }
        }
        if(agent.velocity != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public Vector3 RandomNavSphere(Vector3 origin, int dist, int layermask) {

        Vector3 randDirection = new Vector3(Random.Range(-dist, dist), transform.position.y, 0);

        randDirection += new Vector3(origin.x + 1, 0, 0);

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        
        agent.CalculatePath(randDirection, navMeshPath);

        if (navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            return navHit.position;
        }
        else
        {
            return -navHit.position;
        }

    }

 
}
