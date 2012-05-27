using UnityEngine;
using System.Collections;

public class CharacterDamage : MonoBehaviour {
  public float effectDuration = 1f;
  public float noiseIntensity = 2.5f;
  public float vignetteIntensity = 4f;
  public float motionBlurIntensity = 0.8f;
  public Color hurtColor;
  public float hurtAlpha = 0.3f;
  public GameObject hurtPlane;

  public float lowpassMinimumFrequency = 500f;
  public float lowpassMaximumFrequency = 6000f;

  float timer = 0f;
  NoiseAndGrain noise;
  Vignetting vignetting;
  AudioLowPassFilter lowPass;
  MotionBlur motionBlur;

	void Start () {
	}
	
	void Update () {
	  if (timer > 0) {
      timer -= Time.deltaTime;
      if(lowPass != null) lowPass.cutoffFrequency = Mathf.Lerp(lowpassMaximumFrequency, lowpassMinimumFrequency, timer/effectDuration);
    } else {
      if(lowPass != null) lowPass.cutoffFrequency = 22000;
      timer = 0;
    }
    if(noise != null) noise.strength = noiseIntensity * timer/effectDuration;
    if(vignetting != null) vignetting.intensity = vignetteIntensity * timer/effectDuration;
    if(motionBlur != null) motionBlur.blurAmount = motionBlurIntensity * timer/effectDuration;
    hurtColor.a = timer/effectDuration * hurtAlpha;
    hurtPlane.renderer.material.SetColor("_TintColor", hurtColor); 
	}

  void OnCollisionEnter(Collision other) {
    SpitterHandler enemy = other.gameObject.GetComponent<SpitterHandler>() as SpitterHandler;
    if (enemy != null) {
      noise = Camera.main.GetComponent<NoiseAndGrain>() as NoiseAndGrain;
      vignetting = Camera.main.GetComponent<Vignetting>() as Vignetting;
      lowPass = Camera.main.GetComponent<AudioLowPassFilter>() as AudioLowPassFilter;
      motionBlur = Camera.main.GetComponent<MotionBlur>() as MotionBlur;
      timer = effectDuration;
    }
   }
}
