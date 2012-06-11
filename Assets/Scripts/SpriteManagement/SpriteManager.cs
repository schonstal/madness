using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour {
  public float rows = 2f;
  public float columns = 2f;

  Dictionary<string, SpriteAnimation> animations = new Dictionary<string, SpriteAnimation>();
  string currentAnimation = null;
  bool forceRestart = false;

	void Start () {
    renderer.material.mainTextureScale = new Vector2(1/rows, 1/columns);
	}
	
	void Update () {
    if (currentAnimation != null) {
      animations[currentAnimation].Play(Time.deltaTime, forceRestart);
      if(forceRestart) forceRestart = false;

      selectFrame(animations[currentAnimation].Frame);
    }
	}

  void selectFrame(int frame) {
    float row = Mathf.Floor(frame/columns) - 1;
    float column = Mathf.Floor(frame%rows);

    renderer.material.mainTextureOffset = new Vector2(
        row/rows, (columns - column)/columns);

  }

  public void AddAnimation(string name, int[] frames, float framerate, bool loop = true) {
     animations.Add(name, new SpriteAnimation(frames, framerate, loop));
  }

  public void Play(string name, bool fRestart = false) {
    currentAnimation = name;
    forceRestart = fRestart;
  }

  public bool Finished {
    get {
      return animations[currentAnimation].finished;
    }
  }
}
