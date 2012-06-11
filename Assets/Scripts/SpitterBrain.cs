using UnityEngine;
using System.Collections;

public class SpitterBrain : MonoBehaviour {
  public GameObject shooterTarget;
  public float soundThreshold = 0.6f;
  public float minimumTime = 0.39f;

  public float cringeTime = 0.2f;
  public float health = 15;

  public int fireIterationMin = 1;
  public int fireIterationMax = 3;

  SpitBallShooter shooter;
  AnimationManager animationManager;

  bool hurt = false;

  int iterations = 1;
  int currentIteration = 0;

  float hurtTimer = 0;

	void Start () {
    renderer.material.color = new Color(1,0,1);
    shooter = shooterTarget.GetComponent<SpitBallShooter>() as SpitBallShooter; 

    animationManager = GetComponent<AnimationManager>() as AnimationManager;
    animationManager.AddAnimation("FlyDown", new int[] {1,2,3}, 9f);
    animationManager.AddAnimation("FlyUp", new int[] {4,5,0}, 9f);

    animationManager.AddAnimation("FireDown", new int[] {7,8,9}, 9f);
    fireIterationMax += 1;
    iterations = Random.Range(fireIterationMin, fireIterationMax);

    animationManager.AddAnimation("Hurt", new int[] {12}, 9f);

    animationManager.Play("FlyDown");
	}
	
	void Update () {
    if(hurt) {
      hurtTimer += Time.deltaTime;
      if(hurtTimer >= cringeTime && health > 0) {
        hurt = false;
        hurtTimer = 0;
      }
      animationManager.Play("Hurt");
    } else if(animationManager.Finished) {
      if(currentIteration >= iterations) {
        iterations = Random.Range(fireIterationMin, fireIterationMax);
        currentIteration = 0;
        animationManager.Play("FireDown", true);
//        shooter.Fire();
      } else {
        animationManager.Play("FlyDown", true);
      }
      currentIteration++;
    }

	}

  public void ApplyDamage(float damage) {
    Debug.Log(health);
    hurtTimer = 0;
    health -= damage;
    hurt = true;
  }
}
