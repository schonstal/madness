using UnityEngine;
using System.Collections;

public class SoundManagerTest : MonoBehaviour {
  public GameObject soundTarget;

  SoundManager manager;

	// Use this for initialization
	void Start () {
	  manager = soundTarget.GetComponent<SoundManager>() as SoundManager;  
	}
	
	// Update is called once per frame
	void Update () {
    manager.Volume = Mathf.Abs(rigidbody.velocity.magnitude);
	}
}
