using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float activeTime;
	private float killTime;
	
	public void InitWithDamage(float damage) {
		DamageEnemy damageEnemy = GetComponentInChildren<DamageEnemy>();
		damageEnemy.SetDamage(damage);
	}

	void Start () {
		gameObject.SetActive(false);
	}

	void OnEnable () {
		killTime = activeTime;
	}

	// Update is called once per frame
	void Update () {
		killTime -= Time.deltaTime;
		if (killTime < 0) {
			gameObject.SetActive(false);
		}
	}
}
