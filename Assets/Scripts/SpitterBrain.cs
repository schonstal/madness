using UnityEngine;
using System.Collections;

public class SpitterBrain : MonoBehaviour {
  public GameObject soundTarget;
  public float soundThreshold = 0.6f;
  public float minimumTime = 0.39f;

  AnimationManager animationManager;
  SoundManager soundManager;
  bool flyCycle = true;
  bool inPeak = false;
  float soundTimer = 0f;

	void Start () {
	  soundManager = soundTarget.GetComponent<SoundManager>() as SoundManager;  

    animationManager = GetComponent<AnimationManager>() as AnimationManager;
    animationManager.AddAnimation("FlyDown", new int[] {0,1,2}, 9f);
    animationManager.AddAnimation("FlyUp", new int[] {3,4,5}, 9f);
	}
	
	void Update () {
    soundTimer += Time.deltaTime;
    if(soundManager.OutputVolume > soundThreshold && !inPeak && soundTimer >= minimumTime) {
      soundTimer = 0;
      inPeak = true;
      flyCycle = !flyCycle;
      if(flyCycle) animationManager.Play("FlyUp", true);
      else animationManager.Play("FlyDown", true);
    } else {
      inPeak = false;
    }
	}
}
