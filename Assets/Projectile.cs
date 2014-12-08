using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float speed = 5f;
	public float rotationSpeed = 0f;
	public float damage = 2f;
	public GameObject sprite;
	public float maxTimer = 2.0f;

	public void SetDamage(float newDamage) {
		damage = newDamage;
	}

	void Update () {
		maxTimer -= Time.deltaTime;
		if (maxTimer < 0) {
			Destroy (this.gameObject);
		}
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		if(sprite != null)
		{
			sprite.transform.Rotate (Vector3.forward, rotationSpeed * Time.deltaTime);
		}
	}
	
	void OnCollisionEnter (Collision collision) {
		collision.other.gameObject.SendMessageUpwards("DoDamage", damage, SendMessageOptions.DontRequireReceiver);
		Destroy (this.gameObject);
	}	
}
