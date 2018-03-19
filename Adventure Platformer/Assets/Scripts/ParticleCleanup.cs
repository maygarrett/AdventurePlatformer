using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCleanup : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(DestroySelf());
	}

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
