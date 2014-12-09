using UnityEngine;
using System.Collections;

public class ProjectileWeapon : MonoBehaviour {
	public float activeTime = 0.5f;
	public GameObject projectile;
	public float damage = 5f;
	public int charges;
	private float shootTime;
	private bool canShoot;
	
	public void InitWithDamageAndCharges(float newDamage, int newCharges) {
		damage = newDamage;
		charges = newCharges;
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
			PlaySound();
			charges--;
			GameObject gameObject = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
			Projectile newProjectile = gameObject.GetComponentInChildren<Projectile>();
			newProjectile.SetDamage(damage);
			canShoot = false;
		}
		if (shootTime < 0) {
			if (this.name == "fireball-weapon" && charges <= 0) {
				canShoot = false;	
			} else {
				canShoot = true;
			}
			gameObject.SetActive(false);
		}
	}
	void PlaySound() {
		switch (this.name) {
		case "dagger-weapon" :
			AudioManager.Instance.PlaySound("dagger");
			break;
		case "crossbow-weapon" :
			AudioManager.Instance.PlaySound("bow");
			break;
		case "fireball-weapon" :
			AudioManager.Instance.PlaySound("fireballShot");
			break;
		}
	}
}
