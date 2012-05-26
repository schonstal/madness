using UnityEngine;
using System.Collections;

public class HerpDerp : MonoBehaviour {
  public int channelExponent = 6; //number of channels = 2^channelExponent
  public AudioSource audioSource;
  public float gain = 10.0f;
  public float hueChangeAmount = 0.5f;
  public float startHue = 0.8f;
  public float volumeCoefficient = 1.0f;
  public bool wacky = false;
  public float wackiness = 0.2f;

  public GameObject target;

  private int channelCount = 64;
  private float[][] spectrum;
  private float[] buffer = new float[1024];

	// Use this for initialization
	void Start () {
    channelExponent = (int)Mathf.Clamp(channelExponent, 6, 13);
    channelCount = (int)Mathf.Pow(2, channelExponent);
    spectrum = new float[2][];
    spectrum[0] = new float[channelCount];
    spectrum[1] = new float[channelCount];
	}
	
	// Update is called once per frame
	void Update () {
    audioSource.GetSpectrumData(spectrum[0], 0, FFTWindow.Blackman);
    audioSource.GetSpectrumData(spectrum[1], 1, FFTWindow.Blackman);

    float volume = (RMSForChannel(0) + RMSForChannel(1))/2;
    //Camera.main.backgroundColor = new HSBColor(volume, 0.4f, volume * volumeCoefficient).ToColor();
    if(target) {
      target.renderer.material.color = new HSBColor(volume, 0.4f, volume * volumeCoefficient).ToColor();
      if(wacky)
        target.transform.localScale = new Vector3(1 - volume * wackiness, 1 + volume * wackiness, 0);
    } else {
      Camera.main.backgroundColor = new HSBColor(volume, 0.4f, volume * volumeCoefficient).ToColor();
    }
	}

  float RMSForChannel(int channel) {
    audioSource.GetOutputData(buffer, channel);
    float sum = 0;
    foreach(float f in buffer){
        sum += f*f;
    }
    return Mathf.Sqrt(sum/buffer.Length);
  }
}
