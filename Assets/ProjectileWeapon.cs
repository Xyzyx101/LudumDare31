using UnityEngine;
using System.Collections;

public class ProjectileWeapon : MonoBehaviour {
	public float activeTime = 0.5f;
	public GameObject projectile;
	public float damage = 5f;
	private float shootTime;
	private bool canShoot;


	public void InitWithDamage(float newDamage) {
		damage = newDamage;
	}

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
			GameObject gameObject = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
			Projectile newProjectile = gameObject.GetComponentInChildren<Projectile>();
			newProjectile.SetDamage(damage);
			canShoot = false;
		}
		if (shootTime < 0) {
			canShoot = true;
			gameObject.SetActive(false);
		}
	}	
}
