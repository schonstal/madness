using UnityEngine;
using System.Collections;

public class BounceFOV : MonoBehaviour {
  public float changeAmount = 40.0f;
  public GameObject soundTarget;

  SoundManager manager;

	void Start () {
	  manager = soundTarget.GetComponent<SoundManager>() as SoundManager;  
	}

	// Update is called once per frame
	void Update () {
    transform.camera.fieldOfView = 60 + (manager.OutputVolume * changeAmount);
	}
}
