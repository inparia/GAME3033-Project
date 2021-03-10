using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Player : MonoBehaviour
{

    public bool idle;
    private CharacterController myCharacterController;
    private Animator animator;
    public float walkMovement;
    public float speed = 100;
    public Text resumeText;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float jumpHeight = .35f;
    private float gravityValue = -9.81f;
    // Start is called before the first frame update
    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        idle = true;
    }

    // Update is called once per frame
    void FixedUpdate()
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
    }

    public void OnWalk(InputAction.CallbackContext context)
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
    public void OnJump()
    {
        if(groundedPlayer)
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }
    public void PauseGame()
    {
        if (!GameManager.Instance.gamePaused && GameManager.Instance.gamePlay)
        {
            Time.timeScale = 0;
            GameManager.Instance.gamePaused = true;
            resumeText.gameObject.SetActive(true);
        }
        else if (GameManager.Instance.gamePaused && GameManager.Instance.gamePlay)
        {
            Time.timeScale = 1;
            GameManager.Instance.gamePaused = false;
            resumeText.gameObject.SetActive(false);
        }

    }

}
