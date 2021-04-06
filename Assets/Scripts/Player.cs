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
    private float jumpHeight = .35f;
    private float gravityValue = -9.81f, facingDirection;
    private Vector3 playerVelocity;

    [Header("Text")]
    public Text resumeText;
    public Button saveButton, exitButton;
    [Header("Spawn Point")]
    public Transform spawnPoint, spawnPointTwo;
    public Transform mainSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        switch(GameManager.Instance.gameLevel)
        {
            case GameLevel.LEVEL1:
                mainSpawnPoint = spawnPoint;
                break;
            case GameLevel.LEVEL3:
                mainSpawnPoint = spawnPointTwo;
                break;
            default:
                mainSpawnPoint = spawnPoint;
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
        if(gameObject.transform.position.x > spawnPointTwo.position.x)
        {
            GameManager.Instance.gameLevel = GameLevel.LEVEL3;
        }
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


    public void PauseGame()
    {
        if (!GameManager.Instance.gamePaused)
        {
            GameManager.Instance.gamePaused = true;
            resumeText.gameObject.SetActive(true);
            saveButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);
        }
        else if (GameManager.Instance.gamePaused)
        {
            GameManager.Instance.gamePaused = false;
            resumeText.gameObject.SetActive(false);
            saveButton.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(false);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!other.GetComponent<WanderingAI>().isDead)
            {
                isDead = true;
                animator.SetBool("isDead", isDead);
                GameManager.Instance.delayScene(5, "LoseScene");
            }
        }

        if (other.gameObject.tag == "Goal")
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
}
