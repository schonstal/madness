using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {
  public GameObject soundTarget;
  public GameObject bulletPrefab;
  public GameObject gun;
  public float bulletSpeed = 100.0f;
  public float soundThreshold = 0.6f;
  public float soundVolume = 1.0f;

  bool inPeak = false;
  SoundManager manager;
  Stack<GameObject> inactiveBullets;

  float timer = 0f;


	void Start() {
    inactiveBullets = new Stack<GameObject>();
	  manager = soundTarget.GetComponent<SoundManager>() as SoundManager;  
	}
	
	void Update() {
    if(Input.GetButton("Fire1")) {
      if(manager.OutputVolume > soundThreshold) {
        if(!inPeak) {
          inPeak = true;
          SpawnBullet();
        }
      } else {
        inPeak = false;
      }
      manager.Volume = soundVolume;
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
    gun.GetComponent<GunAnim>().Fire();
  }

  public void pushBullet(GameObject bullet) { 
    inactiveBullets.Push(bullet);
  }
}
