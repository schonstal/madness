using UnityEngine;
using System.Collections;

public class SpitterHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnCollisionEnter(Collision other) {
    Bullet bullet = other.gameObject.GetComponent<Bullet>() as Bullet;
    if(bullet != null) {
      gameObject.SetActiveRecursively(false);
    }
  }
}
