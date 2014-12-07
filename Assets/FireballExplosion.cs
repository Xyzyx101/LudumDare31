using UnityEngine;
using System.Collections;

public class FireballExplosion : MonoBehaviour {
	public float damage = 25f;
	public float delay = 100f;
	private bool damageActive = true;

	void Update () {
		delay -= Time.time;
		if (delay < 0) {
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log(other);
		if ( damageActive ) {
			other.gameObject.SendMessageUpwards("DoDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
	}
}
