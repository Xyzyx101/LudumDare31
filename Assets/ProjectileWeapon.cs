using UnityEngine;
using System.Collections;

public class ProjectileWeapon : MonoBehaviour {
	public float activeTime = 0.5f;
	public GameObject projectile;
	private float shootTime;
	private bool canShoot;
	
	// Use this for initialization
	void Start () {
		gameObject.SetActive(false);
		canShoot = true;
	}
	
	void OnEnable () {
		shootTime = activeTime;
	}
	
	// Update is called once per frame
	void Update () {
		shootTime -= Time.deltaTime;
		if (canShoot) {
			Instantiate(projectile, transform.position, transform.rotation);
			canShoot = false;
		}
		if (shootTime < 0) {
			canShoot = true;
			gameObject.SetActive(false);
		}
	}	
}
