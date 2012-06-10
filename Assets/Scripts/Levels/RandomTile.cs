using UnityEngine;
using System.Collections;

public class RandomTile : MonoBehaviour {

	// Use this for initialization
	void Start () {
    renderer.material.mainTextureOffset = new Vector2(Random.value < 0.5 ? 0f : 0.5f, Random.value < 0.5 ? 0f : 0.5f);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
