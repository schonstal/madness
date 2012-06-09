using UnityEngine;
using System.Collections;

public class SpitBallShooter : MonoBehaviour {
  public float speed;
  public float fireTime = 0.11f;

  SpitBallHandler ballHandler;
  bool fire = false;
  float fireTimer = 0f;

	void Start () {
    ballHandler = FindObjectOfType(typeof(SpitBallHandler)) as SpitBallHandler;
	}

  void Update() {
    if(fire) {
      fireTimer += Time.deltaTime;
      if(fireTimer >= fireTime) {
        fire = false;
        GameObject ball = ballHandler.SpawnSpitBall();
        ball.transform.position = transform.position;
        ball.rigidbody.velocity = transform.forward * speed;
      }
    }
  }

  public void Fire() {
    fireTimer = 0;
    fire = true;
  }
}
