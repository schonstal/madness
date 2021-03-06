public class Animation {
  public float frameTime;
  public int[] frames;
  public string spriteName;
  public bool finished;

  IRagePixel ragePixel;
  float timer = 0f;
  int frameIndex = 0;
  bool loops;

	public Animation(IRagePixel rage, string name, int[] frameArray, float framerate, bool loop = true) {
    ragePixel = rage;
    spriteName = name;
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
      ragePixel.SetSprite(spriteName, frames[frameIndex]);
      frameIndex++;
      if(frameIndex > frames.Length - 1 || forceRestart) {
        frameIndex = 0;
        finished = true;
      }
      timer = 0;
    }
	}
}
