using UnityEngine;

public class playerMove : MonoBehaviour
{
    public GameObject mapObject;
    private InputSystem_Actions controls;
    Rigidbody2D rb;

    public float speed = 1f;

    Vector2 moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Awake()
    {
        controls = new InputSystem_Actions();

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
        Vector2 move = new Vector2(moveInput.x, moveInput.y);
        rb.MovePosition(rb.position + move * speed);
    }
}
