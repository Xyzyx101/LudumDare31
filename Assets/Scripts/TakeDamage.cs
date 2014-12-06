using UnityEngine;
using System.Collections;

public class TakeDamage : MonoBehaviour {

	public float hp = 100 ;

	void Update () {
		if( hp <= 0) {
			Destroy(this.gameObject);
		}
	}

	public void DoDamage(float damage) {
		hp -= damage;
	}
}
