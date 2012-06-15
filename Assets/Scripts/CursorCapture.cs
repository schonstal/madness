using UnityEngine;
using System.Collections;

public class CursorCapture : MonoBehaviour {
	void Awake() {
      guiTexture.pixelInset = new Rect(
        -Screen.currentResolution.width/2,
        -Screen.currentResolution.height/2,
        Screen.currentResolution.width,
        Screen.currentResolution.height);
	}
	
    void DidLockCursor() {
        Debug.Log("Locking cursor");
        guiTexture.enabled = false;
    }
    void DidUnlockCursor() {
        Debug.Log("Unlocking cursor");
        guiTexture.enabled = true;
    }
    void OnMouseDown() {
        Screen.lockCursor = true;
		Debug.Log ("Ding Dong");
    }
    private bool wasLocked = false;
    void Update() {
        if (Input.GetKeyDown("escape"))
            Screen.lockCursor = false;
        
        if (!Screen.lockCursor && wasLocked) {
            wasLocked = false;
            DidUnlockCursor();
        } else
            if (Screen.lockCursor && !wasLocked) {
                wasLocked = true;
                DidLockCursor();
            }
    }
} 