using UnityEngine;
using System.Collections;

public class TakeDamage : MonoBehaviour {

	public float hp = 100f;
	public float immunityTime = 0.3f;
	private bool damaged = false;
	private float immunityTimer;

	void Update () {
		if( hp <= 0) {
			Destroy(this.gameObject);
		}
		if (damaged) {
			immunityTime -= Time.deltaTime;
			if (immunityTime < 0) {
				damaged = false;
			}
		}
	}

	public void DoDamage(float damage) {
		if(!damaged) {
			damaged = true;
			hp -= damage;
			immunityTimer = immunityTime;
		}
	}
}
