using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _gravity = 1f;
    [SerializeField]
    private float _jumpHeight = 15;
   
    [SerializeField]
    private float _speed;
   
    private float _yVelocity;
    private Vector3 _direction;
    private Vector3 _velocity;

    private CharacterController _controller;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       

        if (_controller.isGrounded == true)
        {
            float horizonatal = Input.GetAxis("Horizontal");
            _direction = new Vector3(0, 0, horizonatal);
            _velocity = _direction * _speed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
            }
        }
        else
        {
            _yVelocity -= _gravity;
        }
        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);
    }
}
