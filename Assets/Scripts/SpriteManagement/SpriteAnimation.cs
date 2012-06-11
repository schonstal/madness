using System.Collections.Generic;

[System.Serializable]
public class SpriteAnimation {
  public int[] frames;
  public List<int> listFrames;
  public bool finished;
  public float FramesPerSecond = 30;

  IRagePixel ragePixel;
  float timer = 0f;
  int frameIndex = 0;
  bool loops;
  float frameTime;

  public float FPS {
    get { return 1/frameTime; }
    set { frameTime = 1/value; }
  }

  public int Frame {
    get { return frames[frameIndex]; }
  }

	public SpriteAnimation(int[] frameArray, float framerate, bool loop = true) {
    frames = frameArray; 
    frameTime = 1/framerate;
    loops = loop;
	}
	
	public void Play(float dt, bool forceRestart = false) {
    if(forceRestart) {
      timer = 0f;
      frameIndex = 0;
      finished = false;
    }
    timer += dt;
    if(timer >= frameTime && !(!loops && finished)) {
      frameIndex++;
      if(frameIndex > frames.Length - 1 || forceRestart) {
        frameIndex = 0;
        finished = true;
      }
      timer = 0;
    }
	}
}
