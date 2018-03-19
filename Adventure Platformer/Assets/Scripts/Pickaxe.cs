using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour {

    [SerializeField] private GameObject _debrisParticles;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            Instantiate(_debrisParticles, transform.position, collision.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
