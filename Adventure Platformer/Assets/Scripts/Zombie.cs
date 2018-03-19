using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {

    //components
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    //movement booleans
    private bool _isMovingRight;
    private bool _isChasing= false;
    private bool _isWalking = true;
    // movement variables
    [SerializeField] private float _speed;

    // detection layermask
    [SerializeField] private LayerMask _playerDetection;

    // player
    private GameObject _player;

    public bool _isDead = false;

    [SerializeField] private GameObject _puffOfSmoke;

    // Use this for initialization
    void Start () {
        // assigning references
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        // patrol movement
        if (_isWalking && !_isChasing)
        {
            if (_isMovingRight)
            { WalkRight(); }
            else if (!_isMovingRight)
            { WalkLeft(); }
        }

        // chasing movement
        if(!_isWalking && _isChasing)
        {
            if(_player.transform.position.x > gameObject.transform.position.x)
            { WalkRight(); }
            else if (_player.transform.position.x < gameObject.transform.position.x)
            { WalkLeft(); }
        }

        // detection raycast
        if (_isWalking)
        {
            if (_isMovingRight) // if zombie is facing right
            {  // throw a raycast and see if it hits the player
                if (Physics2D.Raycast(transform.position, Vector2.right, 7, _playerDetection)) // if it does, enter chase mode
                {
                    StartChasing();
                }
            }
            else if (!_isMovingRight) // if the zombie is facing left
            {
                if (Physics2D.Raycast(transform.position, Vector2.left, 7, _playerDetection)) // throw the same raycast the other way, do the same thing as above
                {
                    StartChasing();
                }
            }
        }

        // animation checks and triggers
        // flip sprite check
        if (_isChasing || _isWalking)
        {
            bool flipSprite = (_spriteRenderer.flipX ? (_rb.velocity.x > 0.01f) : (_rb.velocity.x < -0.01f));
            if (flipSprite)
            {
                _spriteRenderer.flipX = !_spriteRenderer.flipX;
            }
        }
        // walking animation check
        if (_isWalking)
        { _animator.SetBool("IsWalking", true); }
        else { _animator.SetBool("IsWalking", false); }
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
    private void StartChasing()
    {
        _isWalking = false;
        _isChasing = true;
        _speed *= 1.5f;
        _animator.speed *= 1.5f;
        SoundManager.instance.PlaySoundEffect(_audioSource, SoundManager.instance.g_zombieChase);
    }

    // collision functions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PathNode")
        {
            if(_isWalking)
            { _isMovingRight = !_isMovingRight; }
        }
    }

    // got hit by a projectile or hit box functionality
    public void GotHit(Vector3 pHitLocation)
    {
        _isDead = true;
        _animator.SetTrigger("Death");
        SoundManager.instance.PlaySoundEffect(_audioSource, SoundManager.instance.g_zombieDeath);
        _isWalking = false;
        _isChasing = false;
        _rb.freezeRotation = false;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
        if (pHitLocation.x < transform.position.x)
            _rb.AddTorque(-30);
        else _rb.AddTorque(30);
        StartCoroutine(DestroySelf());
    }
    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(2);
        Instantiate(_puffOfSmoke, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
