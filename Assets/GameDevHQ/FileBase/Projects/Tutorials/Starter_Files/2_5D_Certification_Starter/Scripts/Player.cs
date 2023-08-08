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
    private bool _isRunningJumping;
    private bool _isIdleJumping;
    private bool _canRoll;
    private bool _isLedgeGrabed;

    [SerializeField]
    private bool _isClimbingLadder;

    private CharacterController _controller;

    private Transform _animationBonesTransform;



    private int _coins;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        _animationBonesTransform = _anim.GetBoneTransform(HumanBodyBones.Hips);
    }

    private void Update()
    {
        if (_isLedgeGrabed == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _anim.SetTrigger("Climbup");
            }
        }
    }

    void FixedUpdate()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        if (_controller.isGrounded == true)
        {
            float horizonatal = Input.GetAxis("Horizontal");
            _direction = new Vector3(0, 0, horizonatal);
            _velocity = _direction * _speed;
            _anim.SetFloat("Speed", Mathf.Abs(horizonatal));
            if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Rolling") == false)
            {
                _canRoll = false;
            }
            if (_isRunningJumping == true)
            {
                _isRunningJumping = false;
                _anim.SetBool("Jumping", _isRunningJumping);
            }
            else if (_isIdleJumping == true)
            {
                _isIdleJumping = false;
                _anim.SetBool("IdleJumping", _isIdleJumping);
            }

            if (Input.GetKeyDown(KeyCode.Space) && _canRoll == false)
            {
                _isRunningJumping = true;
                _isIdleJumping = true;
                _anim.SetBool("Jumping", _isRunningJumping);
                _anim.SetBool("IdleJumping", _isIdleJumping);
                _yVelocity = _jumpHeight;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _canRoll = true;
                _anim.SetTrigger("Rolling");
            }

            if (horizonatal > 0.1f && _controller.enabled == true && _canRoll == false)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (horizonatal < -0.1f && _controller.enabled == true && _canRoll == false)
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

    public void ActivateLedgeGrab(GameObject handPos)
    {
        _isLedgeGrabed = true;
        _controller.enabled = false;
        transform.position = handPos.transform.position;
        _anim.SetBool("Jumping", false);
        _anim.SetFloat("Speed", 0.0f);
        _anim.SetBool("ClimbingLadder", false);
        _anim.SetBool("LedgeGrab", true);
    }

    public void AlignFinalPosition()
    {
        _controller.enabled = true;
        _isLedgeGrabed = false;
        _isClimbingLadder = false;
        _anim.SetBool("ClimbingLadder", false);
        _anim.SetBool("LedgeGrab", false);
        Vector3 originPos = _animationBonesTransform.position;
        transform.position = _animationBonesTransform.position;
        _animationBonesTransform.position = originPos;
    }

    public void AddCoin()
    {
        _coins++;
        UIManager.Instance.UpdateCoin(_coins);
    }

    public void ClimbLader()
    {
        _controller.enabled = false;
        _anim.SetBool("Jumping", false);
        _anim.SetFloat("Speed", 0.0f);
        if (_isClimbingLadder == true)
        {
            _anim.enabled = true;
            _anim.SetBool("ClimbingLadder", true);
            transform.Translate(Vector3.up * Time.deltaTime);
        }
    }

    public void ExitLader()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("ClimbingLadder") == true)
        {
            _anim.enabled = false;
            _isClimbingLadder = false;
        }
        else
        {
            _anim.enabled = true;
            _isClimbingLadder = false;
            _anim.SetBool("ClimbingLadder", false);
        }
    }

    public void AllowToClimbLadder()
    {
        _isClimbingLadder = true;
    }
}
