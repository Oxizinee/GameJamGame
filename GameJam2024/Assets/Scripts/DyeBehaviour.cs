using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DyeBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public int amountOfSec;
    public LayerMask floorMask;

    private float _timer;

    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > amountOfSec)
        {
            Destroy(gameObject);
        }

        if (_rb.IsTouchingLayers(floorMask))
        {
            //_rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
