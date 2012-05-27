using UnityEngine;
using System.Collections;

public class GunManager : MonoBehaviour {
  public GameObject activeGun;

  enum GunState {In=0, Out, Active};
  GunState state = GunState.Active;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
