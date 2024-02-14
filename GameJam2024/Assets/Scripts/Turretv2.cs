using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Turretv2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float statrtAngle = 90f, endAngle = 270f;

    public GameObject BulletPrefab;
    public float BulletSpeed;
    public float BulletAmount = 10;

    public float Timer = 0;
    public float CoolDown = 1;

    private GameObject _player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player = collision.gameObject;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player = collision.gameObject;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _player = null;
    }
    void Start()
    {
    }

    private void FireBullets()
    {
        float angleStep = (endAngle - statrtAngle) / BulletAmount;
        float angle = statrtAngle;

        for (int i = 0; i < BulletAmount; i++)
        {
            float bulDirectionX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirectionY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirectionX, bulDirectionY, 0);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            Instantiate(BulletPrefab, transform.position, transform.rotation);
            BulletPrefab.GetComponent<BulletStraight>().MoveDirection = bulDir;
            BulletPrefab.GetComponent<BulletStraight>().MovementSpeed = BulletSpeed;

            angle += angleStep;
        }

        Timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if (_player != null && Timer > CoolDown)
        {
            FireBullets();
        }
    }
}
