using UnityEngine;
using System.Collections;

public class PointAt : MonoBehaviour {
  public GameObject target;
  
  void Awake() {
    target = GameObject.Find("Player");
  }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
  	transform.LookAt(target.transform);
	}
}
