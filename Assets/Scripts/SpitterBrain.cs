using UnityEngine;
using System.Collections;

public class SpitterBrain : MonoBehaviour {
  public GameObject shooterTarget;
  public float soundThreshold = 0.6f;
  public float minimumTime = 0.39f;

  public int fireIterationMin = 1;
  public int fireIterationMax = 3;

  SpitBallShooter shooter;
  AnimationManager animationManager;
  SoundManager soundManager;
  bool flyCycle = true;
  bool inPeak = false;
  float soundTimer = 0f;

  int iterations = 1;
  int currentIteration = 0;

	void Start () {
    renderer.material.color = new Color(1,0,1);
    shooter = shooterTarget.GetComponent<SpitBallShooter>() as SpitBallShooter; 

    animationManager = GetComponent<AnimationManager>() as AnimationManager;
    animationManager.AddAnimation("FlyDown", new int[] {1,2,3}, 9f);
    animationManager.AddAnimation("FlyUp", new int[] {4,5,0}, 9f);

    animationManager.AddAnimation("FireDown", new int[] {7,8,9}, 9f);
    fireIterationMax += 1;
    iterations = Random.Range(fireIterationMin, fireIterationMax);

    animationManager.Play("FireDown");
	}
	
	void Update () {
    if(animationManager.Finished) {
      if(currentIteration >= iterations) {
        iterations = Random.Range(fireIterationMin, fireIterationMax);
        currentIteration = 0;
        animationManager.Play("FireDown", true);
        shooter.Fire();
      } else {
        animationManager.Play("FlyDown", true);
      }
      currentIteration++;
    }
	}
}
