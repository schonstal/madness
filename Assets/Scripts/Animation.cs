public class Animation {
  public float frameTime;
  public int[] frames;
  public string spriteName;

  IRagePixel ragePixel;
  float timer = 0f;
  int frameIndex = 0;

	public Animation(IRagePixel rage, string name, int[] frameArray, float framerate) {
    ragePixel = rage;
    spriteName = name;
    frames = frameArray; 
    frameTime = 1/framerate;
	}
	
	public void Play(float dt, bool forceRestart = false) {
    timer += dt;
    if(timer >= frameTime) {
      ragePixel.SetSprite("Gun", frames[frameIndex]);
      frameIndex++;
      if(frameIndex > frames.Length - 1 || forceRestart) {
        frameIndex = 0;
      }
      timer = 0;
    }
	}
}
