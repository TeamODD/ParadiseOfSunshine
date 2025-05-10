using System;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using static UnityEditor.PlayerSettings;
using UnityEngine.UIElements;

public class playerMove : MonoBehaviour
{
    public GameObject mapObject;
    private InputSystem_Actions controls;
    BoxCollider2D MapCollider;
    BoxCollider2D BoxCollider2D;

    public float speed = 1f;
    public float rangeRest = 2f;

    Vector2 moveInput;
    Vector3 size;
    private Vector2 minBound;
    private Vector2 maxBound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MapCollider = mapObject.GetComponent<BoxCollider2D>();
        BoxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        Vector3 mapPos = mapObject.transform.position;
        Vector3 mapScale = MapCollider.size * 100;
        Vector3 size = BoxCollider2D.size * 100;

        minBound = new Vector2(mapPos.x - (mapScale.x - size.x) * 0.5f, mapPos.y - (mapScale.y - size.y ) * 0.5f);
        maxBound = new Vector2(mapPos.x + (mapScale.x - size.x) * 0.5f, mapPos.y + (mapScale.y - size.y ) * 0.5f);
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
        Vector3 move = new Vector3(moveInput.x, moveInput.y, 0);
        transform.Translate(move * Time.deltaTime * speed);
        //InCamera();
    }
    private void LateUpdate()
    {
       Vector3 pos = transform.position;
       pos.x = Mathf.Clamp(pos.x, minBound.x, maxBound.x);
       pos.y = Mathf.Clamp(pos.y, minBound.y, maxBound.y);
       transform.position = pos;
    }
    void InCamera()
    {
        size = transform.localScale;
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        pos.x = Mathf.Clamp(pos.x, 0f, 1f);
        pos.y = Mathf.Clamp(pos.y, 0f, 1f);

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
