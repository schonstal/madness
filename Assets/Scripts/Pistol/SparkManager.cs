using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SparkManager : MonoBehaviour {
  public Spark sparkPrefab;

  Stack<Spark> inactiveSparks;

	void Start() {
    inactiveSparks = new Stack<Spark>();
	}
	
	void Update() {
	}

  public void SpawnSpark(Vector3 position) {
    Spark spark;
    if (inactiveSparks.Count > 0) {
      spark = inactiveSparks.Pop();
      spark.gameObject.SetActiveRecursively(true);
      spark.transform.position = position;
      spark.transform.rotation = transform.rotation;
    } else {
      spark = Instantiate(sparkPrefab, position, transform.rotation) as Spark;
    }  
    spark.Initialize();
    spark.GetComponent<Spark>().manager = this;
  }

  public void push(Spark spark) { 
    inactiveSparks.Push(spark);
  }
}
