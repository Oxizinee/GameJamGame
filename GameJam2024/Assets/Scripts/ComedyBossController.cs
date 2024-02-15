using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComedyBossController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform positionA, positionB;
    public float Speed = 5;

    private Vector2 _targetPos;
    void Start()
    {
        _targetPos = positionB.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, positionA.position) < 0.1f)
        {
            _targetPos = positionB.position;
        }
        if (Vector2.Distance(transform.position, positionB.position) < 0.1f)
        {
            _targetPos = positionA.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, _targetPos, Speed * Time.deltaTime);
    }
}
