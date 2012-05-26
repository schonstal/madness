using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
  public const string CONTAINER_NAME = "SoundContainer";

  public int channelExponent = 6; //number of channels = 2^channelExponent
  public bool getSpectrum = false;
  public bool getVolume = false;

  AudioSource audioSource;
  int channelCount = 64;
  float[][] spectrum;
  float[] buffer = new float[1024];

  float outputVolume = 0f;

  public float OutputVolume {
    get {
      return outputVolume;
    }
  }

  public float Volume {
    get {
      return audioSource.volume;
    }
    set {
      audioSource.volume = value;
    }
  }

  public float[][] Spectrum {
    get {
      return spectrum;
    }
  }

	// Use this for initialization
	void Start () {
    audioSource = GetComponent<AudioSource>() as AudioSource;

    channelExponent = (int)Mathf.Clamp(channelExponent, 6, 13);
    channelCount = (int)Mathf.Pow(2, channelExponent);
    spectrum = new float[2][];
    spectrum[0] = new float[channelCount];
    spectrum[1] = new float[channelCount];
	}
	
	// Update is called once per frame
	void Update () {
    if(getSpectrum) {
      audioSource.GetSpectrumData(spectrum[0], 0, FFTWindow.Blackman);
      audioSource.GetSpectrumData(spectrum[1], 1, FFTWindow.Blackman);
    }
    if(getVolume) {	
      outputVolume = (RMSForChannel(0) + RMSForChannel(1))/2;
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
