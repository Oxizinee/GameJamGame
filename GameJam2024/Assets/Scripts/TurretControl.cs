using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class TurretControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] BulletSpawn;
    public GameObject BulletPrefab;

    public float Timer = 0;
    public float Cooldown = 3;

    public float BulletSpeed = 5;

    public Vector2 BulletMoveDir;

    [SerializeField]private GameObject _player;
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

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if (_player != null && Timer > Cooldown)
        {
            foreach (Transform t in BulletSpawn)
            {
                GameObject bulletInstance = Instantiate(BulletPrefab, t.transform.position, t.rotation);
                if (bulletInstance.GetComponent<BulletStraight>().bulletType == BulletType.Normal)
                {
                    bulletInstance.GetComponent<BulletStraight>().SetMoveDirection(BulletMoveDir.normalized);
                }
                else if (bulletInstance.GetComponent<BulletStraight>().bulletType == BulletType.FollowPlayer)
                {
                    bulletInstance.GetComponent<BulletStraight>().SetMoveDirection((_player.transform.position - bulletInstance.transform.position).normalized);
                }
                bulletInstance.GetComponent<BulletStraight>().SetMovementSpeed(BulletSpeed);
            }
            
            Timer = 0;
        }
    }
}
