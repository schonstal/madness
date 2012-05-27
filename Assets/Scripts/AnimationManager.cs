using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationManager : MonoBehaviour {
  public string spriteName = "Default";

  Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
  IRagePixel ragePixel;
  string currentAnimation = null;
  bool forceRestart = false;

	void Start () {
    ragePixel = GetComponent<RagePixelSprite>();
	}
	
	void Update () {
    if (currentAnimation != null) {
      animations[currentAnimation].Play(Time.deltaTime, forceRestart);
      if(forceRestart) forceRestart = false;
    }
	}

  public void AddAnimation(string name, int[] frames, float framerate, bool loop = true) {
     ragePixel = GetComponent<RagePixelSprite>();
     animations.Add(name, new Animation(ragePixel, spriteName, frames, framerate, loop));
  }

  public void Play(string name, bool fRestart = false) {
    currentAnimation = name;
    forceRestart = fRestart;
  }
}
