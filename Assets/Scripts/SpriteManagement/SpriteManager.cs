using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour {
  public int rows = 2;
  public int columns = 2;

  Dictionary<string, SpriteAnimation> animations = new Dictionary<string, SpriteAnimation>();
  string currentAnimation = null;
  bool forceRestart = false;

	void Start () {
    renderer.material.mainTextureScale = new Vector2(1/(float)rows, 1/(float)columns);
	}
	
	void Update () {
    if (currentAnimation != null) {
      CurrentAnimation.Play(Time.deltaTime, forceRestart);
      if(forceRestart) forceRestart = false;

      selectFrame(CurrentAnimation.Frame);
    }
	}

  void selectFrame(int frame) {
    int row = rows - frame/columns - 1;
    int column = frame%columns;

    renderer.material.mainTextureOffset = new Vector2(
        1 - (columns - column)/(float)columns, 
        row/(float)rows);

//    Debug.Log("Row: " + row + ", Column: " + column + ", Frame: " +frame);

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
      return CurrentAnimation.finished;
    }
  }

  public SpriteAnimation CurrentAnimation {
    get {
      return animations[currentAnimation];
    }
  }
}
