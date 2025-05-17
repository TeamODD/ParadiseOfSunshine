using UnityEngine;

public class playerMove : MonoBehaviour
{
    public static playerMove Instance;
    private InputSystem_Actions controls;
    Rigidbody2D rb;
    Animator animator;
    AudioSource audioSource;

    public float speed = 1f;

    Vector2 moveInput;

    public bool isTalking;
    public bool isTuto = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isTalking = false;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Awake()
    {
        Instance = this;
        controls = new InputSystem_Actions();
        audioSource = GetComponent<AudioSource>();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        Debug.Log(isTalking);
        if (isTalking)
        {
            animator.SetInteger("direction", 0);
            audioSource.Stop();
            return;
        }
        else
        {
            //방향 상하좌우 2143
            if (moveInput.y > 0)
                animator.SetInteger("direction", 2);
            else if (moveInput.y < 0)
                animator.SetInteger("direction", 1);
            else if (moveInput.x > 0)
                animator.SetInteger("direction", 3);
            else if (moveInput.x < 0)
                animator.SetInteger("direction", 4);
            else
                animator.SetInteger("direction", 0);

            animator.speed = speed * 0.04f;
            Vector2 move = new Vector2(moveInput.x, moveInput.y);
            rb.MovePosition(rb.position + move * speed);

            if(moveInput.x != 0 || moveInput.y != 0)
            {
                if(!audioSource.isPlaying)
                    audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        }
    }

}
