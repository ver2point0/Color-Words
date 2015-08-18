using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class RandomColorFill : MonoBehaviour {

	public Color[] colorPool;
	// Use this for initialization
	void Start () {
		int randomColorIndex = Random.Range (0, colorPool.Length);
		SpriteRenderer r = GetComponent<SpriteRenderer> ();
		r.color = colorPool [randomColorIndex];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
