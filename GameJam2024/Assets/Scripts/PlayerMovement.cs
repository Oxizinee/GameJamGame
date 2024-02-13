using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEditor;

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
    public GameObject[] Hearts;
    private List<GameObject> _hearts;

    public float MovementSpeed = 6;
    public float GravityScale = 9.81f;
    public float JumpHeight = 7;
    public float TeleportCooldown = 3f;

    private Material _deafultMaterial;
    public Material FlashMaterial;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            LoseALife();
        }
    }
    void Start()
    {
        _deafultMaterial = GetComponent<SpriteRenderer>().sharedMaterial;
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        _hearts = new List<GameObject>();

        for (int i = 0; i < Hearts.Length; i++)
        {
            _hearts.Add(Hearts[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _keyboardInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _velocity += Physics2D.gravity.y * GravityScale * Time.deltaTime;

        CheckBoarders();
        Movement();

        if (Lives == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    private void CheckBoarders()
    {
        float boarderScale = BoarderLeft.transform.localScale.x;

        if (Camera.main.WorldToScreenPoint(transform.position).x > Screen.width - BoarderRight.transform.localScale.x)
        {
            transform.position = (new Vector3(transform.position.x - boarderScale, transform.position.y, 0));
            LoseALife();
        }
        else if (Camera.main.WorldToScreenPoint(transform.position).x < 0 + BoarderLeft.transform.localScale.x)
        {
            transform.position = (new Vector3(transform.position.x + boarderScale, transform.position.y, 0));
            LoseALife();
        }
    }

    private void LoseALife()
    {
        _impulseSource.GenerateImpulse();
        _hearts[_hearts.Count - 1].gameObject.SetActive(false);
        _hearts.Remove(_hearts[_hearts.Count - 1]);
        Lives--;
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
                StartCoroutine(FlashPlayer());
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

    private IEnumerator FlashPlayer()
    {
        for (int i = 0; i < 2; i++)
        {
            GetComponent<SpriteRenderer>().sharedMaterial = FlashMaterial;
            yield return new WaitForSeconds(0.125f);
            GetComponent<SpriteRenderer>().sharedMaterial = _deafultMaterial;
            yield return new WaitForSeconds(0.125f);
        }

        yield return null;
    }
}
