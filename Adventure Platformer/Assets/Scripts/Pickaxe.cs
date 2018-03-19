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
        if(collision.gameObject.layer == 10 || collision.gameObject.layer == 11)
        {
            Instantiate(_debrisParticles, transform.position, collision.transform.rotation);
            SoundManager.instance.PlayBreakSound();

            // if collision is an enemy, trigger its get hit function
            if (collision.gameObject.tag == "Zombie")
            {
                if (!collision.gameObject.GetComponent<Zombie>()._isDead)
                {
                    collision.gameObject.GetComponent<Zombie>().GotHit(transform.position);
                }
            }




            Destroy(this.gameObject);
        }
    }
}
