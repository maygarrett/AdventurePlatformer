using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    private GameObject _pauseCanvas;

    [SerializeField] private GameObject[] _objectsToDisable;

	// Use this for initialization
	void Start () {

        _pauseCanvas = gameObject.transform.GetChild(0).gameObject;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        }
	}

    public void PauseToggle()
    {
        _pauseCanvas.SetActive(!_pauseCanvas.activeSelf);
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else Time.timeScale = 0;

        // flip the actvity of nessisary scripts
        foreach(GameObject objectToDisable in _objectsToDisable)
        {
            MonoBehaviour[] scriptsToDisable = objectToDisable.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scriptsToDisable)
            {
                script.enabled = !script.enabled;
            }
        }
    }

    public void ReturnToMenu()
    {
        _pauseCanvas.SetActive(false);
        Time.timeScale = 1;
        SceneManager.instance.MainMenu();
    }
}
