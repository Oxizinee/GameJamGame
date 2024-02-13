using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 _keyboardInput;
    private Vector2 _mousePosition;
    private float _velocity;

    public GameObject BoarderLeft;
    public GameObject BoarderRight;

    private bool _isGrounded = false;
    [SerializeField] private LayerMask _floorMask;
    [SerializeField] private Transform _feet;
    [SerializeField] private float _floorHeight;

    [SerializeField]private bool _teleportAvaiable = true;

    private CinemachineImpulseSource _impulseSource;

    public float Lives = 3;

    public float MovementSpeed = 6;
    public float GravityScale = 9.81f;
    public float JumpHeight = 7;
    public float TeleportCooldown = 3f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Lives--;
            _impulseSource.GenerateImpulse();
        }
    }
    void Start()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _keyboardInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _velocity += Physics2D.gravity.y * GravityScale * Time.deltaTime;

        CheckBoarders();
        Movement();

    }

    private void CheckBoarders()
    {
        float boarderScale = BoarderLeft.transform.localScale.x;

        if (Camera.main.WorldToScreenPoint(transform.position).x > Screen.width - BoarderRight.transform.localScale.x)
        {
            transform.position = (new Vector3(transform.position.x - boarderScale, transform.position.y, 0));
            _impulseSource.GenerateImpulse();
            Lives--;
        }
        else if (Camera.main.WorldToScreenPoint(transform.position).x < 0 + BoarderLeft.transform.localScale.x)
        {
            transform.position = (new Vector3(transform.position.x + boarderScale, transform.position.y, 0));
            _impulseSource.GenerateImpulse();
            Lives--;
        }
    }

    private void Movement()
    {
        if (Physics2D.OverlapBox(_feet.position, _feet.localScale, 0, _floorMask, 0, 1) != null && _velocity < 0)
        {
            _velocity = 0;
            Vector2 surface = Physics2D.ClosestPoint(transform.position, Physics2D.OverlapBox(_feet.position, _feet.localScale, 0, _floorMask, 0, 1)) + Vector2.up * _floorHeight;
            transform.position = new Vector3(transform.position.x, surface.y, transform.position.z);
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && _isGrounded)
        {
            _velocity = Mathf.Sqrt(JumpHeight * -2 * (Physics2D.gravity.y * GravityScale));
        }
        Teleport();

        transform.Translate(new Vector3(Vector3.right.x * _keyboardInput.x * MovementSpeed, _velocity, 0) * Time.deltaTime);
    }

    private void Teleport()
    {
        if (Input.GetMouseButtonDown(0) && _teleportAvaiable)
        {
            transform.position = _mousePosition;
            _teleportAvaiable = false;
        }

        if (_teleportAvaiable == false)
        {
            TeleportCooldown = TeleportCooldown - Time.deltaTime;

            if (TeleportCooldown <= 0)
            {
                TeleportCooldown = 3;
                _teleportAvaiable = true;
            }
        }
    }
}
