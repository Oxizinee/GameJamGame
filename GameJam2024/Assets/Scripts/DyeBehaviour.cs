using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyeBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public int amountOfSec;
    private float _timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > amountOfSec)
        {
            Destroy(gameObject);
        }
    }
}
