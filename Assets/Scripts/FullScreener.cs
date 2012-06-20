using UnityEngine;
using System.Collections;

public class FullScreener : MonoBehaviour {
  void Update() {
    if(Input.GetKeyDown(KeyCode.F)) {
      Screen.fullScreen = !Screen.fullScreen;
    }
  }
}
