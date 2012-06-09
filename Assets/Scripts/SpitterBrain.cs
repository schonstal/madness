using UnityEngine;
using System.Collections;

public class SpitterBrain : MonoBehaviour {
  public GameObject soundTarget;
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
	  soundManager = soundTarget.GetComponent<SoundManager>() as SoundManager;
    shooter = shooterTarget.GetComponent<SpitBallShooter>() as SpitBallShooter; 

    animationManager = GetComponent<AnimationManager>() as AnimationManager;
    animationManager.AddAnimation("FlyDown", new int[] {1,2,3}, 9f);
    animationManager.AddAnimation("FlyUp", new int[] {4,5,0}, 9f);

    animationManager.AddAnimation("FireDown", new int[] {7,8,9}, 9f);
    fireIterationMax += 1;
    iterations = Random.Range(fireIterationMin, fireIterationMax);
	}
	
	void Update () {
    soundTimer += Time.deltaTime;
    if(soundManager.OutputVolume > soundThreshold && !inPeak && soundTimer >= minimumTime) {
      soundTimer = 0;
      inPeak = true;
      flyCycle = !flyCycle;
      if(flyCycle) {
        animationManager.Play("FlyUp", true);
      } else {
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
    } else {
      inPeak = false;
    }
	}
}
