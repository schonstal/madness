using UnityEngine;
using System.Collections;

public class SpitBallShooter : MonoBehaviour {
  public float speed;
  public float minimumFireTime = 2.0f;
  public float maximumFireTime = 5.0f;

  float fireTime = 0.5f;
  float fireTimer = 0f;

  SpitBallHandler ballHandler;

	void Start () {
    ballHandler = FindObjectOfType(typeof(SpitBallHandler)) as SpitBallHandler;
    fireTime = Random.Range(minimumFireTime, maximumFireTime);
	}
	
	void Update () {
    fireTimer += Time.deltaTime;
    if(fireTimer >= fireTime) {
      fireTime = Random.Range(minimumFireTime, maximumFireTime);
      fireTimer = 0;
      Fire();
    }
	}

  void Fire() {
    GameObject ball = ballHandler.SpawnSpitBall();
    ball.transform.position = transform.position;
    ball.rigidbody.velocity = transform.forward * speed;
  }
}
