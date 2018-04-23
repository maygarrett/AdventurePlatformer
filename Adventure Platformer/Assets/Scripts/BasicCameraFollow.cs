using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraFollow : MonoBehaviour {

    [SerializeField] private float _yValue;

    private Vector3 _locationStorage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _locationStorage = transform.position;
        transform.position = new Vector3(_locationStorage.x, _yValue, _locationStorage.z);
	}
}
