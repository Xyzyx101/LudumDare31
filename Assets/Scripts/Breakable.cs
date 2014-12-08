using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour {
	public GameObject[] Spawnable;
	public int chanceToSpawn = 5; 

	public void DoDamage(float damage)
	{
		Debug.Log ("smashy Smashy");
		if(Random.Range(0, 100) < chanceToSpawn)
		{
			int objectToSpawn = Random.Range(0, Spawnable.Length); 
			GameObject spawnedObject = (GameObject)Instantiate(Spawnable[objectToSpawn], this.transform.position, Spawnable[objectToSpawn].transform.rotation);
		}

		Destroy (this.gameObject);
	}
}
