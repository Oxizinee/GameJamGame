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
    public float MovementSpeed = 0.1f;
    public Vector3 MoveDirection = Vector3.down;
    public LayerMask floorMask;

    private float timer = 0;
    private GameObject _player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (bulletType == BulletType.FollowPlayer)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            MoveDirection = _player.transform.position - transform.position;
        }


    }

    // Update is called once per frame
    void Update()
    {
         transform.localPosition += MoveDirection.normalized * MovementSpeed;

        if (Physics2D.OverlapCircle(transform.position, transform.localScale.x - 0.2f, floorMask,0,1))
        {
           // Debug.Log("hit");
            Destroy(gameObject);
        }

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
