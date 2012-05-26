using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {
  public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
  	transform.LookAt(target);
    transform.rotation = Camera.main.transform.rotation;//Quaternion.Euler(0, transform.eulerAngles.y, 0);
	}
}
