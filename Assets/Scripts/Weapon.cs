using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float activeTime;
	private float killTime;

	// Use this for initialization
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
