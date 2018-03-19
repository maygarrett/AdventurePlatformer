using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowItem : MonoBehaviour {

    // throwing variables
    [SerializeField] private Transform _throwPositionRight;
    [SerializeField] private Transform _throwPositionLeft;
    [SerializeField] private GameObject _pickaxePrefab;
    [SerializeField] private float _throwPower;
    [SerializeField] private float _throwHeight;
    [SerializeField] private float _spinSpeed;

    //animation variables
    private Animator _animator;

    // face direction variables
    private bool _isFacingRight;
    private SpriteRenderer _spriteRenderer;

    // Use this for initialization
    void Start () {
        //assign references
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        // reference checks
        if(!_throwPositionLeft)
        { _throwPositionLeft = GameObject.Find("ThrowPositionLeft").transform; }
        if (!_throwPositionRight)
        { _throwPositionRight = GameObject.Find("ThrowPositionRight").transform; }
        if(!_pickaxePrefab)
        { Debug.LogError("Pickaxe prefab reference not assigned in throw item script"); }

    }
	
	// Update is called once per frame
	void Update () {
        // get user input to throw pickaxe
		if(Input.GetKeyDown(KeyCode.C))
        {
            // if x is flipped
            if (_spriteRenderer.flipX)
            {   ThrowPickaxe(_throwPositionLeft, -1); /* throw pickaxe left */ }
            else ThrowPickaxe(_throwPositionRight, 1); // throw right
        }
	}

    private void ThrowPickaxe(Transform throwPosition, float leftOrRight)
    {
        // start animation
        _animator.SetTrigger("Throw");

        // create the pickaxe object
        GameObject thrownPickaxe = Instantiate(_pickaxePrefab, throwPosition.position, throwPosition.rotation);

        // add speed and rotation to it
        Vector2 throwVector = new Vector2();
        throwVector.x = _throwPower * leftOrRight;
        throwVector.y = _throwHeight;
        Rigidbody2D thrownPickaxeRB = thrownPickaxe.GetComponent<Rigidbody2D>();
        thrownPickaxeRB.velocity = throwVector;
        thrownPickaxeRB.AddTorque(_spinSpeed);

        // play sound effect
        SoundManager.instance.PlaySoundEffect(SoundManager.instance.g_throwWhoosh);
    }
}
