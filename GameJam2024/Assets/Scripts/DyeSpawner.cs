using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyeSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Dyes;
    public float Cooldown = 10;
    private float _timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > Cooldown)
        {
            int num = Random.Range(0, 6);
            GameObject dyeInstance = Instantiate(Dyes[num], transform.position, Quaternion.identity);
            _timer = 0;
        }
    }
}
