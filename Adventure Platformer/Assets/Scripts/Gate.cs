using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    [SerializeField] private GameObject[] _states;
    private int _currentState = 0;

    private int _counter = 0;

    [SerializeField] private float _timeBetweenStates;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(_counter == 1)
        {
            StartCoroutine(ChangeState());
            _counter = 0;
        }
        else if(_counter == 2)
        {
            StartCoroutine(ChangeState());
            _counter = 0;
        }
        else if (_counter == 3)
        {
            StartCoroutine(ChangeState());
            _counter = 0;
        }
        else if (_counter == 4)
        {
            StartCoroutine(ChangeState());
            _counter = 0;
        }


    }

    IEnumerator ChangeState()
    {
        yield return new WaitForSecondsRealtime(_timeBetweenStates);

        if (_currentState + 1 <= _states.Length)
        {
            _states[_currentState + 1].SetActive(true);
            _states[_currentState].SetActive(false);
            _currentState++;
            _counter = _currentState;
        }
        Debug.Log("Current State Increase");
    }

    public void OpenGate()
    {
        if (_currentState == 0)
        {
            StartCoroutine(ChangeState());
        }
    }
}
