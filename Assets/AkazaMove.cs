using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkazaMove : MonoBehaviour
{
    private float _speed = 7f;

    public void StartMove()
    {
        Vector3 position = transform.position;
        position.x = 0;
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
        if (transform.position.x > 30f || transform.position.x < -30f) _speed *= -1f;
    }
}
