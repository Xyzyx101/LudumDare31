using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour {
	public GameObject[] Spawnable;
	public int chanceToSpawn = 5; 

	public void DoDamage(float damage)
	{
		if(Random.Range(0, 100) < chanceToSpawn)
		{
			int objectToSpawn = Random.Range(0, Spawnable.Length); 
			GameObject spawnedObject = (GameObject)Instantiate(Spawnable[objectToSpawn], this.transform.position, Spawnable[objectToSpawn].transform.rotation);
			spawnedObject.transform.parent = this.transform.parent;
		}

		Destroy (this.gameObject);
	}
}
