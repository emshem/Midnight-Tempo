using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour {
	private RectTransform reticle;

	[Range(0f, 250)]
	public float size = 50;

	// Use this for initialization
	private void Start () {
		// Cursor.visible = false;
		reticle = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		// this.transform.position = Input.mousePosition;
		reticle.sizeDelta = new Vector2(size, size);
	}
}
