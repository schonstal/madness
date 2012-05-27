using UnityEngine;
using System.Collections;

public class SpitterHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnCollisionEnter() {
    gameObject.SetActiveRecursively(false);
  }
}
