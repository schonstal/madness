using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {
  public Transform target;

  SpitBallHandler ballHandler;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
  	transform.LookAt(target);
    transform.rotation = Camera.main.transform.rotation;//Quaternion.Euler(0, transform.eulerAngles.y, 0);
	}
}
