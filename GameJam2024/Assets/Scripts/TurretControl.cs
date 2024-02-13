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
                Instantiate(BulletPrefab, t.transform.position, t.rotation);
                BulletPrefab.GetComponent<BulletStraight>().MovementSpeed = BulletSpeed;
            }
            
            Timer = 0;
        }
    }
}
