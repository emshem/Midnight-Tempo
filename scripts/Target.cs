using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public GameObject explosionEffect;

	// Use this for initialization
	void Start () {
		StartCoroutine("DelayedDestroy");
	}

	// OnMouseDown is called on mouse click
	private void OnMouseDown () {
		// GameControl.score += 10;
		Instantiate(explosionEffect, transform.position, transform.rotation);
		Destroy(gameObject);
	}

	public IEnumerator DelayedDestroy(){
		// destroy target after 1 second
		yield return new WaitForSeconds(1f);
		if (gameObject != null) {
			GameControl.score -= 5;
			GameControl.healthValue -= 0.1f;
			Destroy(gameObject);
		}
	}
}
