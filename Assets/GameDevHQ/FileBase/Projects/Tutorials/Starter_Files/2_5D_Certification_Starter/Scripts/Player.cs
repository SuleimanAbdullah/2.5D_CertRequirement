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

    private bool _isLedgeGrabed;

    private CharacterController _controller;
   
    private Transform _animationBonesTransform;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        _animationBonesTransform = _anim.GetBoneTransform(HumanBodyBones.Hips);
        Debug.Log(_animationBonesTransform);
    }

    private void Update()
    {
        if (_isLedgeGrabed ==true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _anim.SetTrigger("Climbup");
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_controller.isGrounded == true)
        {
            float horizonatal = Input.GetAxis("Horizontal");
            _direction = new Vector3(0, 0, horizonatal);
            _velocity = _direction * _speed;
            _anim.SetFloat("Speed", Mathf.Abs(horizonatal));

            if (_isJumping == true)
            {
                _isJumping = false;
                _anim.SetBool("Jumping", _isJumping);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isJumping = true;
                _anim.SetBool("Jumping", _isJumping);
                _yVelocity = _jumpHeight;
            }
            if (horizonatal > 0.1f)
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
            if (_isLedgeGrabed == true)
            {
                _anim.SetBool("Jumping", false);
                return;
            }
                
            else
            {
                _yVelocity -= _gravity;
            }
        }
        if (_isLedgeGrabed == false)
            _velocity.y = _yVelocity;
        Physics.SyncTransforms();
        _controller.Move(_velocity * Time.deltaTime);
    }

    public void ActivateLedgeGrab()
    {
        _isLedgeGrabed = true;
        _controller.enabled = false;
        transform.position = new Vector3(0.05f, 67.95f, 123.83f);
        _anim.SetBool("Jumping", false);
        _anim.SetFloat("Speed", 0.0f);
        _anim.SetBool("LedgeGrab", true);
    }

    public void AlignFinalPosition()
    {
        _controller.enabled = true;
        _isLedgeGrabed = false;
        _anim.SetBool("LedgeGrab", false);
        Vector3 originPos = _animationBonesTransform.position;
        transform.position = _animationBonesTransform.position;
        _animationBonesTransform.position = originPos *Time.deltaTime;
        Debug.Log("Standup");
    }
}
