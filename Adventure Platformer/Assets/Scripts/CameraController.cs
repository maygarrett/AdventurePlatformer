using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject _playerObject;
    private PlayerMovementController _playerController;
    private Transform _playerTransform;
    private Transform _camera;
    private SpriteRenderer _playerRenderer;

    [SerializeField] private float _yPosition; // camera's y value
    [SerializeField] private float _xMaxDistance; // max distance from player before it begins to follow
    [SerializeField] private float _xMinClampValue; // left most edge of the level
    [SerializeField] private float _xMaxClampValue; // right most edge of the level

	// Use this for initialization
	void Start () {
        // assign references
        _playerObject = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = _playerObject.transform;
        _playerController = _playerObject.GetComponent<PlayerMovementController>();
        _playerRenderer = _playerObject.GetComponent<SpriteRenderer>();
        _camera = gameObject.transform;

        // check values are assigned
        if(_yPosition == 0)
        {
            Debug.Log("Y offest not set, defaulting to 1");
            _yPosition = 1;
        }
        if(_xMaxDistance == 0)
        {
            Debug.Log("X max distance not set, defaulting to 1");
            _xMaxDistance = 1;
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



        // if camera is between the maximum edges of the level
        //if(_camera.position.x > _xMinClampValue && _camera.position.x < _xMaxClampValue)
        //{
            // camera follow behaviour
            if (_camera.position.x > _xMinClampValue)
            {

                if (_camera.position.x > _playerTransform.position.x + _xMaxDistance)
                {
                    // begin to move the camera back to the closest safe position
                    _camera.position = new Vector3(_playerTransform.position.x + _xMaxDistance, _yPosition, _camera.position.z);
                }
            }

            if (_camera.position.x < _xMaxClampValue)
            {
                if (_camera.position.x < _playerTransform.position.x - _xMaxDistance)
                {
                    // begin to move the camera back to the closest safe position
                    _camera.position = new Vector3(_playerTransform.position.x - _xMaxDistance, _yPosition, _camera.position.z);
                }
            }



        //}
        /*
        // otherwise set the cameras position to the borders x value
        else if(_camera.position.x >= _xMaxClampValue)
        {
            // set position to the largest x value
            _camera.position = new Vector3(_xMaxClampValue, _camera.position.y, _camera.position.z);
        }
        else if(_camera.position.x <= _xMinClampValue)
        {
            // set position to the smallest x value
            _camera.position = new Vector3(_xMinClampValue, _camera.position.y, _camera.position.z);
        }
        */
		
	}
}
