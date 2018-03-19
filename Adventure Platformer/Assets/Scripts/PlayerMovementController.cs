using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    private Rigidbody2D _rb;

    // movement variables
    [SerializeField] private float _speed;
    [SerializeField] private float _airSpeed;

    // grounded variables
    private bool _isGrounded;
    [SerializeField] private Transform _feet;
    [SerializeField] private Transform _feetRight;
    [SerializeField] private Transform _feetLeft;
    [SerializeField] private LayerMask _environmentLayer;

    // jumping variables
    [SerializeField] private float _jumpPower;

    // animation stuff
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    // sound stuff
    private int _footstepCounter = 0;
    private SoundManager _soundManager;

    // Use this for initialization
    void Start () {
        // setting references
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
        _soundManager = SoundManager.instance;

        // reference checks
        if(!_feet)
        { Debug.LogError("Player feet transform not set in inspector");
          _feet = GameObject.Find("PlayerGroundedPoint").transform; }
        if (!_feetRight)
        {   Debug.LogError("Player feet transform not set in inspector");
            _feet = GameObject.Find("PlayerGroundedPointRight").transform; }
        if (!_feetLeft)
        {   Debug.LogError("Player feet transform not set in inspector");
            _feet = GameObject.Find("PlayerGroundedPointLeft").transform; }

    }
	
	// Update is called once per frame
	void Update () {

        // check if the player is grounded and set grounded status accordingly
        if (Physics2D.Raycast(_feet.position, Vector2.down, 0.05f, _environmentLayer))
        { _isGrounded = true; }
        else { _isGrounded = false; }


        // check to see if player is grounded
        // if they are grounded use movement controls
        if (_isGrounded)
        {
            // manage movement inputs
            if (Input.GetKey(KeyCode.LeftArrow))
            { WalkLeft(); }
            if (Input.GetKey(KeyCode.RightArrow))
            { WalkRight(); }
            // jump input
            if (Input.GetKeyDown(KeyCode.Space))
            { Jump(); }

        }
        // player is not grounded use limited movement controls
        else if(!_isGrounded)
        {
            // moving in the air
            AirMovement();
        }




        // animation checks and triggers

        // checking if player is moving left or right
        if(_rb.velocity.x > 0.1 || _rb.velocity.x < -0.1)
        {
            _animator.SetBool("IsWalking", true);
        }
        else _animator.SetBool("IsWalking", false);
        // flip sprite check
        bool flipSprite = (_spriteRenderer.flipX ? (_rb.velocity.x > 0.01f) : (_rb.velocity.x < -0.01f));
        if (flipSprite)
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
        // check if the player is jumping
        if(!_isGrounded)
        { _animator.SetBool("IsJumping", true); }
        else { _animator.SetBool("IsJumping", false); }


        // sound checks and triggers

        // checking and counting while player is moving to play footsteps
        if (_isGrounded)
        {
            if (_rb.velocity.x > 0.01f || _rb.velocity.x < -0.01f) // check if player is moving
            {
                if (_footstepCounter >= 15)
                { _soundManager.PlayFootstepSound();
                  _footstepCounter = 0; }
                _footstepCounter++;
            }
            else { _footstepCounter = 14; }
        }


    }






    // movement functions

    private void WalkLeft()
    {
        Vector2 newVelocity = new Vector2();
        newVelocity.x = _speed * -1.0f;
        newVelocity.y = _rb.velocity.y;
        _rb.velocity = newVelocity;
    }
    private void WalkRight()
    {
        Vector2 newVelocity = new Vector2();
        newVelocity.x = _speed;
        newVelocity.y = _rb.velocity.y;
        _rb.velocity = newVelocity;
    }
    private void StopMoving()
    {
        Vector2 stopLinearVelocity = _rb.velocity;
        stopLinearVelocity.x = 0.0f;
        _rb.velocity = stopLinearVelocity;
    }
    private void AirMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector2 newVelocity = new Vector2();
            if (_rb.velocity.x > -_speed)
            {
                newVelocity.x = _rb.velocity.x - (_airSpeed / 30);
            }
            else { newVelocity.x = _rb.velocity.x; }
            newVelocity.y = _rb.velocity.y;
            _rb.velocity = newVelocity;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector2 newVelocity = new Vector2();
            if (_rb.velocity.x < _speed)
            {
                newVelocity.x = _rb.velocity.x + (_airSpeed / 30);
            }
            else { newVelocity.x = _rb.velocity.x; }
            newVelocity.y = _rb.velocity.y;
            _rb.velocity = newVelocity;
        }
    }


    // jump functions
    private void Jump()
    {
        Vector2 newVelocity = new Vector2();
        newVelocity.y = _jumpPower;
        newVelocity.x = _rb.velocity.x;
        _rb.velocity = newVelocity;

        // play sound
        _soundManager.PlayPlayerSoundEffect(_soundManager.g_jumpGrunt);
    }



    // collision functions
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }


    // getters and setters
    public bool GetIsGrounded()
    {
        return _isGrounded;
    }
}
