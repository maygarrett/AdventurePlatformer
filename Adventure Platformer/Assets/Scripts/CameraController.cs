using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject _playerObject;
    private PlayerMovementController _playerController;
    private Transform _playerTransform;
    private Transform _camera;
    private SpriteRenderer _playerRenderer;
    private Rigidbody2D _playerRigidbody;

    [SerializeField] private float _yPosition; // camera's y value
    [SerializeField] private float _xMaxDistance; // max distance from player before it begins to follow
    [SerializeField] private float _xMinClampValue; // left most edge of the level
    [SerializeField] private float _xMaxClampValue; // right most edge of the level

    private float _lerpTime = 0;

	// Use this for initialization
	void Start () {
        // assign references
        _playerObject = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = _playerObject.transform;
        _playerController = _playerObject.GetComponent<PlayerMovementController>();
        _playerRenderer = _playerObject.GetComponent<SpriteRenderer>();
        _playerRigidbody = _playerObject.GetComponent<Rigidbody2D>();
        _camera = gameObject.transform;

        // check values are assigned
        if(_yPosition == 0)
        {
            Debug.Log("Y offest not set, defaulting to 1");
            _yPosition = 1;
        }
        if(_xMaxDistance == 0)
        {
            Debug.Log("X max distance not set, defaulting to 2");
            _xMaxDistance = 2;
        }
        if(_xMaxClampValue == 0)
        {
            Debug.Log("Maximum x value not set, defaulting to 1000");
            _xMaxClampValue = 1000;
        }
        if(_xMinClampValue == 0)
        {
            Debug.Log("Minimum x value not set, defaulting to -1000");
            _xMinClampValue = -1000;
        }
	}
	
	// Update is called once per frame
	void Update () {

        // check to see if player is moving
        bool isMoving = false;
        if (_playerRigidbody.velocity.x > 0.1 || _playerRigidbody.velocity.x < -0.1)
        { isMoving = true; }



            // if camera is between the maximum edges of the level
            // camera follow behaviour
        if (_camera.position.x > _xMinClampValue)
        {
            // if camera is farther right than the left limit
            if (_camera.position.x > _playerTransform.position.x - _xMaxDistance && _playerRenderer.flipX)
            {
                // begin to move the camera back to the closest safe position
                _camera.position = new Vector3(_playerTransform.position.x - _xMaxDistance, _yPosition, _camera.position.z);
            }
            else if( isMoving && !_playerRenderer.flipX)
            {
                // smoothly bring camera back to the other side
                Vector3 rightLimit = new Vector3(_playerTransform.position.x + _xMaxDistance, _yPosition, _camera.position.z);
                // _camera.position = Vector3.Lerp(_camera.position, rightLimit, _lerpTime);
            }
        }

        if (_camera.position.x < _xMaxClampValue)
        {
            // if camera is farther left than the right limit
            if (_camera.position.x < _playerTransform.position.x + _xMaxDistance && !_playerRenderer.flipX)
            {
                // begin to move the camera back to the closest safe position
                _camera.position = new Vector3(_playerTransform.position.x + _xMaxDistance, _yPosition, _camera.position.z);
            }
            else if (isMoving && _playerRenderer.flipX)
            {
                // smoothly bring camera back to the other side
                Vector3 leftLimit = new Vector3(_playerTransform.position.x - _xMaxDistance, _yPosition, _camera.position.z);
                _camera.position = Vector3.Lerp(_camera.position, leftLimit, _lerpTime);
            }
        }
		
	}
}
