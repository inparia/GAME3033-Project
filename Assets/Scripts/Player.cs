using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{

    private bool idle, isDead, isWin;
    private CharacterController myCharacterController;
    private Animator animator;

    [Header("Player Movement Stats")]
    private float walkMovement;
    public int playerLife = 5;
    public float speed = 100;
    private bool groundedPlayer;
    private float jumpHeight = .35f;
    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;

    [Header("Text")]
    public Text resumeText;

    [Header("Spawn Point")]
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        idle = true;
        isDead = false;
        isWin = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!isDead && !isWin)
        {
            if (walkMovement == 0 && !idle && !GameManager.Instance.gamePaused)
            {
                animator.SetBool("isRunning", false);
                idle = true;
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
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
        if (!isDead && !isWin)
        {
            walkMovement = context.ReadValue<float>();
            animator.SetBool("isRunning", true);
            idle = false;
            if (!GameManager.Instance.gamePaused)
            {
                if (walkMovement < 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                }
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                }
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
    public void PauseGame()
    {
        if (!GameManager.Instance.gamePaused)
        {
            GameManager.Instance.gamePaused = true;
            resumeText.gameObject.SetActive(true);
        }
        else if (GameManager.Instance.gamePaused)
        {
            GameManager.Instance.gamePaused = false;
            resumeText.gameObject.SetActive(false);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            isDead = true;
            animator.SetBool("isDead", isDead);
            GameManager.Instance.delayScene(5, "LoseScene");
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
            transform.position = spawnPoint.position;
            playerLife--;
        }
    }

}
