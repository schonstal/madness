using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {
  public GameObject soundTarget;
  public GameObject bulletPrefab;
  public float bulletSpeed = 100.0f;

  SoundManager manager;
  Stack<Transform> inactiveBullets;

	void Start() {
    inactiveBullets = new Stack<Transform>();
	  manager = soundTarget.GetComponent<SoundManager>() as SoundManager;  
	}
	
	void Update() {
    if(Input.GetButton("Fire1")) {
      GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
      bullet.rigidbody.velocity = transform.forward * bulletSpeed; 
      manager.Volume = 1.0f;
    } else {
      manager.Volume = 0f;
    }
	}
}
