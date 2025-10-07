using UnityEngine;

using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // 移動速度
    public float walkSpeed = 2.0f;
    public float runSpeed = 5.0f;
    public float rotationSpeed = 10.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.2f;

    public int maxlife = 3;
    public Image[] life;
    private int currentlife;
    private GameManager gameManager;

    public float damdelaysetting= 1f; // 無敵幀秒數
    private bool damdelay = false;       // 是否無敵
    private float damdelayTimer = 0f;

    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    private Transform cameraMainTransform;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cameraMainTransform = Camera.main.transform;
        gameManager = FindFirstObjectByType<GameManager>();

        currentlife = maxlife;
        UpdatelifehUI();
    }

    void Update()
    {
        bool isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; 
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = cameraMainTransform.forward;
        Vector3 right = cameraMainTransform.right;
        
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize(); 

        Vector3 moveDirection = forward * vertical + right * horizontal;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        if (moveDirection.magnitude >= 0.1f)
        {
            controller.Move(moveDirection.normalized * currentSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        float animationSpeedPercent = (isRunning ? 1.0f : 0.5f) * moveDirection.magnitude;
        animator.SetFloat("MoveSpeed", animationSpeedPercent);
        
        animator.SetBool("Grounded", isGrounded);

        if (damdelay)
        {
            damdelayTimer -= Time.deltaTime;
            if (damdelayTimer <= 0)
            {
                damdelay = false;
            }
        }
        
    }


    void UpdatelifehUI()
    {
        for (int i = 0; i < life.Length; i++)
        {
            if (i < currentlife)
            {
                life[i].enabled = true; 
            }
            else
            {
                life[i].enabled = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (damdelay)
        {
            return;
        }

        currentlife -= damage;
        UpdatelifehUI();

        if (currentlife <= 0)
        {
            Die();
        }
        else
        {
            damdelay = true;
            damdelayTimer = damdelaysetting;
        }
    }

    void Die()
    {
        Debug.Log("GameOVER");
        this.enabled = false;
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
    }

}
