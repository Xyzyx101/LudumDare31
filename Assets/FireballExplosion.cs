using UnityEngine;
using System.Collections;

public class FireballExplosion : MonoBehaviour {
	public float damage = 25f;
	public float delay = 5f;
	public GameObject light;
	private bool damageActive = true;

	void Update () {
		light.light.intensity = delay;
		delay -= Time.deltaTime;
		if (delay < 0) {
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		if ( damageActive ) {
			other.gameObject.SendMessageUpwards("DoDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
	}
}
