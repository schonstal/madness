using UnityEngine;
using System.Collections;

public class SecretDoor : MonoBehaviour {
  public float probability = 0.2f;

  void OnDrawGizmos() {
    Gizmos.DrawIcon(transform.position + new Vector3(0f,2.5f,0f), "EditorIcons/Secret.png", true);
  }

	void Start () {
    if(Random.value < probability) {
      transform.Find("WallTile").gameObject.SetActiveRecursively(false);
      transform.Find("SecretDoor").gameObject.SetActiveRecursively(true);
    } else {
      transform.Find("WallTile").gameObject.SetActiveRecursively(true);
      transform.Find("SecretDoor").gameObject.SetActiveRecursively(false);
    }
	}
}
