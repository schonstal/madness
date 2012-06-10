using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour {
  void OnDrawGizmos() {
    Gizmos.DrawIcon(transform.position + new Vector3(0f,2.5f,0f), "EditorIcons/Player.png", false);
  }

	void Start() {
	}
}
