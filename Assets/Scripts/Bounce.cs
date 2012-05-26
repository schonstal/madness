using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {
  public GameObject soundTarget;
  public Vector3 amplitude = new Vector3(0.2f, 0.2f, 0f);

  IRagePixel ragePixel;
  SoundManager manager;

  Vector3 originalScale = new Vector3(1, 1, 1);
  Vector3 originalLocalPosition = new Vector3(1, 1, 1);

	void Start () {
	  manager = soundTarget.GetComponent<SoundManager>() as SoundManager;  
    originalScale = transform.localScale;
    originalLocalPosition = transform.localPosition;
    ragePixel = GetComponent<RagePixelSprite>();
	}

	// Update is called once per frame
	void Update () {
    float xOffset = manager.OutputVolume * amplitude.x;

    transform.localScale = new Vector3(
        originalScale.x + xOffset,
        originalScale.y + manager.OutputVolume * amplitude.y,
        originalScale.z + manager.OutputVolume * amplitude.z);

    transform.localPosition = new Vector3(
        originalLocalPosition.x - xOffset * ragePixel.GetRect().x,
        originalLocalPosition.y,
        originalLocalPosition.z);
	}
}
