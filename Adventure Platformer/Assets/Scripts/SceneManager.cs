using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {

    private static SceneManager _instance = null;



    private void Awake()
    {
        // instance singleton stuff
        if (instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static SceneManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public void LoadLevel(int pSceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(pSceneIndex);
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
