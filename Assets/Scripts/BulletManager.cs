using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {
  public GameObject soundTarget;
  public GameObject bulletPrefab;
  public float bulletSpeed = 100.0f;

  SoundManager manager;
  Stack<GameObject> inactiveBullets;

	void Start() {
    inactiveBullets = new Stack<GameObject>();
	  manager = soundTarget.GetComponent<SoundManager>() as SoundManager;  
	}
	
	void Update() {
    if(Input.GetButton("Fire1")) {
      manager.Volume = 1.0f;
      SpawnBullet();
    } else {
      manager.Volume = 0f;
    }
	}

  void SpawnBullet() {
    GameObject bullet;
    if (inactiveBullets.Count > 0) {
      bullet = inactiveBullets.Pop();
      bullet.SetActiveRecursively(true);
      bullet.transform.position = transform.position;
      bullet.transform.rotation = transform.rotation;
    } else {
      bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
    }  
    bullet.rigidbody.velocity = transform.forward * bulletSpeed; 
    bullet.GetComponent<Bullet>().manager = this;
  }

  public void pushBullet(GameObject bullet) { 
    inactiveBullets.Push(bullet);
  }
}
