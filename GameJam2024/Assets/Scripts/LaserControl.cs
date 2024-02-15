using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class LaserControl : MonoBehaviour
{
    public GameObject LaserPrefab;
    public float Cooldown;
    public float LaserOut = 2;

    private GameObject _laserInstance;
    private float _timer = 0;
    private bool _laserSpawned = false;
    private float _laserTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > Cooldown && !_laserSpawned)
        {
            _laserInstance = Instantiate(LaserPrefab, new Vector3(transform.position.x, transform.position.y,0), 
                Quaternion.identity);
            _laserInstance.transform.parent = transform;   
            _laserInstance.transform.localScale = new Vector3(0.02f, transform.localScale.y, transform.localScale.z);
            _laserTimer = 0;
            _laserSpawned=true;
        }

        if (_laserSpawned)
        {
            _laserTimer += Time.deltaTime;
            _laserInstance.transform.localScale = new Vector3(((_laserInstance.transform.localScale.x )+Time.deltaTime), transform.localScale.y, transform.localScale.z);
            if (_laserTimer > LaserOut)
            {
                Destroy(_laserInstance);
                _timer = 0;
                _laserSpawned = false;
            }
        }
    }
}
