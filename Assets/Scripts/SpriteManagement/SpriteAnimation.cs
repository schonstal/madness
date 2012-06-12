using System.Collections.Generic;

[System.Serializable]
public class SpriteAnimation {
  public int[] frames;
  public List<int> listFrames;
  public bool finished;
  public float FramesPerSecond = 30;
  public int frameIndex = 0;

  IRagePixel ragePixel;
  float timer = 0f;
  bool loops;
  float frameTime;

  public float FPS {
    get { return 1/frameTime; }
    set { frameTime = 1/value; }
  }

  public int Frame {
    get { return frames[frameIndex]; }
  }

  public int FrameIndex {
    get { return frameIndex; }
  }

	public SpriteAnimation(int[] frameArray, float framerate, bool loop = true) {
    frames = frameArray; 
    frameTime = 1/framerate;
    loops = loop;
	}
	
	public void Play(float dt, bool forceRestart = false, int startFrameIndex = 0) {
    if(forceRestart) {
      timer = 0f;
      frameIndex = startFrameIndex;
      finished = false;
    }
    timer += dt;
    if(timer >= frameTime && !(!loops && finished)) {
      if(frameIndex + 1 > frames.Length - 1 || forceRestart) {
        if(loops) frameIndex = startFrameIndex;
        finished = true;
      } else {
        frameIndex++;
      }
      timer = 0;
    }
	}
}
