using UnityEngine;
using System.Collections;

public class Upgrade : MonoBehaviour {
  void OnDrawGizmos() {
    Gizmos.DrawIcon(transform.position + new Vector3(0f,2.5f,0f), "EditorIcons/Upgrade.png", false);
  }

	void Start () {
    //Perhaps spawn an upgrade
	}
}
