using UnityEngine;
using System.Collections;

public class FireballExplosion : MonoBehaviour {
	public float damage = 25f;
	public float delay = 5f;
	public GameObject light;
	private bool damageActive = true;

	void Update () {
		Debug.Log (delay);
		light.light.intensity = delay;
		delay -= Time.deltaTime;
		if (delay < 0) {
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		DamageMessage (other.gameObject);
	}

	void OnCollisionEnter(Collision collision) {
		DamageMessage (collision.gameObject);
	}

	void DamageMessage (GameObject target) {
		if ( damageActive ) {
			target.SendMessageUpwards("DoDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
	}
}
