using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    [SerializeField] private GameObject[] _states;
    private int _currentState = 0;

    [SerializeField] private float _timeBetweenStates;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (_currentState == 0)
            {
                for (_currentState = 0; _currentState <= _states.Length; _currentState++)
                {
                    StartCoroutine(ChangeState());
                    Debug.Log("End Loop");
                }
            }
        }
		
	}

    IEnumerator ChangeState()
    {
        

        _states[_currentState].SetActive(true);

        if (_currentState - 1 >= 0)
        {
            _states[_currentState - 1].SetActive(false);
        }
        Debug.Log("Current State Increase");

        yield return new WaitForSeconds(_timeBetweenStates);
    }
}
