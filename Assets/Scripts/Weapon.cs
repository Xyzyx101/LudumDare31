using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float activeTime;
	public float damage;
	private float killTime;

	// Use this for initialization
	void Start () {
		Debug.Log ("Init Disabled");
		gameObject.SetActive(false);
	}

	void OnEnable () {
		Debug.Log ("Enabled");
		killTime = activeTime;
	}

	// Update is called once per frame
	void Update () {
		killTime -= Time.deltaTime;
		if (killTime < 0) {
			Debug.Log ("Disabled");
			gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider other) {
		other.gameObject.SendMessageUpwards("DoDamage", damage, SendMessageOptions.DontRequireReceiver);
	}
}
