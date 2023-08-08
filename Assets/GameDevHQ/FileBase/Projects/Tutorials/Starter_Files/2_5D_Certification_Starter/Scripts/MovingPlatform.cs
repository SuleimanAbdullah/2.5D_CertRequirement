using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _pointA, _pointB;
    private bool _hasReachedTargetA;
    [SerializeField]
    private float _speed = 3f;

    void Update()
    {
        PlatformMovement();

    }

    private void PlatformMovement()
    {
        if (_hasReachedTargetA == true)
        {
            if (transform.position == _pointB.position)
            {
                _hasReachedTargetA = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, _pointB.position, _speed * Time.deltaTime);
        }
        else if (_hasReachedTargetA == false)
        {
            if (transform.position == _pointA.position)
            {
                _hasReachedTargetA = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, _pointA.position, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
