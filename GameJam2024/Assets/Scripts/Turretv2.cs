using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public enum FirePattern
{
    HallfCircle,
    Spiral
}

public class Turretv2 : MonoBehaviour
{
    // Start is called before the first frame update
    public FirePattern firePattern = FirePattern.HallfCircle;
    public float statrtAngle = 90f, endAngle = 270f;
    private float _spiralAngle = 0f;

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
        if (Timer > CoolDown)
        {
            float angleStep = (endAngle - statrtAngle) / BulletAmount;
            float angle = statrtAngle;

            for (int i = 0; i < BulletAmount; i++)
            {
                float bulDirectionX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                float bulDirectionY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

                Vector3 bulMoveVector = new Vector3(bulDirectionX, bulDirectionY, 0);
                Vector2 bulDir = (bulMoveVector - transform.position).normalized;

                GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
                bullet.GetComponent<BulletStraight>().SetMoveDirection(bulDir);
                bullet.GetComponent<BulletStraight>().SetMovementSpeed(BulletSpeed);
                angle += angleStep;
            }

            Timer = 0;
        }
    }

    private void FireSpiral()
    {
        float bulDirectionX = transform.position.x + Mathf.Sin((_spiralAngle * Mathf.PI) / 180f);
        float bulDirectionY = transform.position.y + Mathf.Cos((_spiralAngle * Mathf.PI) / 180f);

        Vector3 bulMoveVector = new Vector3(bulDirectionX, bulDirectionY, 0);
        Vector2 bulDir = (bulMoveVector - transform.position).normalized;

        if (Timer > CoolDown)
        {
            GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<BulletStraight>().SetMoveDirection(bulDir);
            bullet.GetComponent<BulletStraight>().SetMovementSpeed(BulletSpeed);
            _spiralAngle += 10F;
            Timer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if (_player != null)
        {
            if(firePattern == FirePattern.HallfCircle)
            FireBullets();
            else if (firePattern == FirePattern.Spiral)
            {
                FireSpiral();
            }
        }
    }
}
