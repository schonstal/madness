using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
  public float survivalTime = 10f;
  public GameObject spriteToDeactivate;
  public BulletManager manager = null;

  float timer = 0f;

	void Start() {
	
	}
	
	void Update() {
    timer += Time.deltaTime;
    if(timer >= survivalTime) {
      killSelf();
    }
	}

  void OnCollisionEnter(Collision collision) {
    killSelf();
  }

  void killSelf() {
    if (manager != null)
       manager.pushBullet(gameObject);
    gameObject.SetActiveRecursively(false);
    timer = 0;
  }
}
