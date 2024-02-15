using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BulletType
{
    Normal,
    FollowPlayer
}

public class BulletStraight : MonoBehaviour
{
    // Start is called before the first frame update
    public BulletType bulletType = BulletType.Normal;
    private float _movementSpeed;
    private Vector3 _moveDirection;
    public LayerMask floorMask;

    private float _angle = 0;
    private float timer = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    public void SetMoveDirection(Vector2 dir)
    {
        _moveDirection = dir;
    }

    public void SetMovementSpeed(float speed)
    {
        _movementSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += _moveDirection * _movementSpeed;

        transform.Rotate(Vector3.forward, (_angle + 5f));

        if (Physics2D.OverlapCircle(transform.position, transform.localScale.x - 0.2f, floorMask, 0, 1))
        {
            Destroy(gameObject);
        }

        DeleteOutOfScreen();

    }

    private void DeleteOutOfScreen()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);


        if (!Screen.safeArea.Contains(pos))
        {
            //Debug.Log("hit");
            timer += Time.deltaTime;

            if (timer > 5)
            {
                Destroy(gameObject);
                timer = 0;
            }
        }
    }
}
