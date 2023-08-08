using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private Transform _pointA, _pointB;

    [SerializeField]
    private bool _isGoingDown =false;
    [SerializeField]
    private float _speed = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CallElevator();
        }
        ElevatorMovement();
    }

    private void ElevatorMovement()
    {
        if (_isGoingDown == true)
        {
            if (transform.position == _pointB.position)
            {
                StartCoroutine(DelayLiftMovementRoutine());
            }
            transform.position = Vector3.MoveTowards(transform.position, _pointB.position, _speed * Time.deltaTime);
        }
        else if (_isGoingDown == false)
        {
            if (transform.position == _pointA.position)
            {
                StartCoroutine(DelayLiftMovementRoutine());
            }
            transform.position = Vector3.MoveTowards(transform.position, _pointA.position, _speed * Time.deltaTime);
        }
    }

    IEnumerator DelayLiftMovementRoutine()
    {
        yield return new WaitForSeconds(5f);
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

     void CallElevator()
    {
        _isGoingDown = !_isGoingDown;
    }
}
