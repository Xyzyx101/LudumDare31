using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {
	public float speed = 1f;
	public GameObject explosion;

	private bool exploding = false;
	
	void Start () {	
	}

	void Update () {
		transform.Translate(Vector3.forward * speed);
	}

	void OnTriggerEnter (Collider other) {
		Debug.Log(other.gameObject);
		if (!exploding) {
			Explode ();
			exploding = true;
		}
	}

	void Explode () {
		Instantiate(explosion, transform.localPosition, transform.localRotation);
		Destroy(this.gameObject);
	}


}
