using UnityEngine;
using System.Collections;

public class RenderToTexture : MonoBehaviour {
  public GUITexture guiTexture;

	// Use this for initialization
	void Start () {
    guiTexture.pixelInset = new Rect(
        -Screen.currentResolution.width/2,
        -Screen.currentResolution.height/2,
        Screen.currentResolution.width,
        Screen.currentResolution.height);

    guiTexture.texture = camera.targetTexture = new RenderTexture(
        Screen.currentResolution.width,
        Screen.currentResolution.height,
        24);

    guiTexture.texture.filterMode = FilterMode.Point;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
