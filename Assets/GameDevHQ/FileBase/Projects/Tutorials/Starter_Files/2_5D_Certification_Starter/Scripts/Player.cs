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
    private Animator _anim;
    private bool _isJumping;

    private CharacterController _controller;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       

        if (_controller.isGrounded == true)
        {
            float horizonatal = Input.GetAxis("Horizontal");
            _direction = new Vector3(0, 0, horizonatal);
            _velocity = _direction * _speed;
            _anim.SetFloat("Speed", Mathf.Abs(horizonatal));

            if (_isJumping ==true)
            {
                _isJumping = false;
                _anim.SetBool("Jumping", false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isJumping = true;
                _anim.SetBool("Jumping", true);
                _yVelocity = _jumpHeight;
            }
            if (horizonatal >0.1f)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (horizonatal < -0.1f)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
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
