using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    
    private bool idle, isDead, isWin, isShoot;
    private CharacterController myCharacterController;
    private Animator animator;
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    [Header("Player Movement Stats")]
    private float walkMovement;
    public int playerLife;
    public float speed = 100;
    private bool groundedPlayer;
    private float jumpHeight = .4f;
    private float gravityValue = -9.81f, facingDirection;
    private Vector3 playerVelocity;
    public AudioSource gunShoot, itemHit, damageTaken;
    [Header("Text")]
    public TMPro.TextMeshProUGUI tMPro;
    public Button saveButton, exitButton;
    [Header("Spawn Point")]
    public Transform spawnPoint, spawnPointTwo, spawnPointThree;
    public Transform mainSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        switch(GameManager.Instance.gameLevel)
        {
            case GameLevel.LEVEL1:
                mainSpawnPoint = spawnPoint;
                break;
            case GameLevel.LEVEL2:
                mainSpawnPoint = spawnPointTwo;
                break;
            case GameLevel.LEVEL3:
                mainSpawnPoint = spawnPointThree;
                break;
        }
        transform.position = mainSpawnPoint.position;
        myCharacterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        idle = true;
        isDead = false;
        isWin = false;
        isShoot = false;
        facingDirection = 90;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead && !isWin && !isShoot)
        {
            if (walkMovement == 0 && !idle && !GameManager.Instance.gamePaused)
            {
                animator.SetBool("isRunning", false);
                idle = true;
                transform.rotation = Quaternion.Euler(new Vector3(0, facingDirection, 0));
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            myCharacterController.Move(new Vector3(walkMovement * Time.deltaTime * speed, playerVelocity.y * Time.deltaTime, 0));

            groundedPlayer = myCharacterController.isGrounded;

            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
            if (!groundedPlayer)
                animator.SetBool("isJumping", true);
            else
                animator.SetBool("isJumping", false);
        }

        if(GameManager.Instance.playerHealth <= 0)
        {
            isDead = true;
            animator.SetBool("isDead", isDead);
            GameManager.Instance.delayScene(5, "LoseScene");
            GameManager.Instance.playerHealth = 5;
        }
    }

    public void OnWalk(InputAction.CallbackContext context)
    {
        if(isShoot)
        {
            walkMovement = 0;
        }

        if (!isDead && !isWin && !isShoot)
        {
            walkMovement = context.ReadValue<float>();
            animator.SetBool("isRunning", true);
            idle = false;
            if (!GameManager.Instance.gamePaused)
            {
                if (walkMovement < 0)
                {
                    facingDirection = -90;
                }
                else if(walkMovement > 0)
                {
                    facingDirection = 90;
                }
                transform.rotation = Quaternion.Euler(new Vector3(0, facingDirection, 0));
            }
        }
    }
    public void OnJump()
    {
        if (groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
            
    }

    public void OnShoot()
    {
        if(groundedPlayer && !isShoot && !isWin && GameManager.Instance.bulletCount > 0)
        {
            StartCoroutine(AnimatorSetFire(.3f));
            StartCoroutine(bulletShoot(0.3f));
        }
    }

    public bool getDead()
    {
        return isDead;
    }
    public void PauseGame()
    {
        if (!GameManager.Instance.gamePaused)
        {
            GameManager.Instance.gamePaused = true;
            tMPro.gameObject.SetActive(true);
            saveButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);
        }
        else if (GameManager.Instance.gamePaused)
        {
            GameManager.Instance.gamePaused = false;
            tMPro.gameObject.SetActive(false);
            saveButton.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(false);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && !isDead && !isWin)
        {
            if (!other.GetComponent<WanderingAI>().isDead)
            {
                damageTaken.Play();
                isDead = true;
                animator.SetBool("isDead", isDead);
                GameManager.Instance.delayScene(5, "LoseScene");
            }
        }

        if (other.gameObject.tag == "Goal")
        {

            GameManager.Instance.gameLevel = GameLevel.LEVEL2;
            transform.position = spawnPointTwo.position;
        }

        if (other.gameObject.tag == "GoalTwo")
        {

            GameManager.Instance.gameLevel = GameLevel.LEVEL3;
            transform.position = spawnPointThree.position;
        }

        if (other.gameObject.tag == "GoalThree")
        {

            isWin = true;
            idle = true;
            animator.SetBool("isRunning", false);
            GameManager.Instance.delayScene(5, "WinScene");
        }

        if (other.gameObject.tag == "DeathPlane")
        {
            transform.position = mainSpawnPoint.position;
            GameManager.Instance.playerHealth--;
        }

    }

    private IEnumerator AnimatorSetFire(float animationLength)
    {
        isShoot = true;
        animator.SetBool("isShoot", true);
        gunShoot.Play();
        yield return new WaitForSeconds(animationLength);
        isShoot = false;
        animator.SetBool("isShoot", false);
    }

    private IEnumerator bulletShoot(float timer)
    {
        yield return new WaitForSeconds(timer);
        var bulletClone = Instantiate(bullet, bulletSpawnPoint.position, bullet.transform.rotation);
        GameManager.Instance.bulletCount--;
        switch(facingDirection)
        {
            case 90:
                bulletClone.GetComponent<Bullet>().shootRight = true;
                break;
            case -90:
                bulletClone.GetComponent<Bullet>().shootRight = false;
                break;
        }
    }
    public void playHurtSound()
    {
        damageTaken.Play();
    }
    public void playItemSound()
    {
        itemHit.Play();
    }
}
