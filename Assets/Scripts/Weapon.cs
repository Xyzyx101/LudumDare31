using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float activeTime;
	public DamageEnemy damageEnemy;
	private float killTime;
	
	public void InitWithDamage(float damage) {
		damageEnemy.SetDamage(damage);
	}

	void Start () {
		gameObject.SetActive(false);
	}

	void OnEnable () {
		killTime = activeTime;
        PlayAttackSound();
	}

	// Update is called once per frame
	void Update () {
		killTime -= Time.deltaTime;
		if (killTime < 0) {
			gameObject.SetActive(false);
		}
	}

    private void PlayAttackSound()
    {
        if (gameObject.name == "punch")
        {
            AudioManager.Instance.PlaySound("punch");
        }
        else if (gameObject.name == "sword")
        {
            AudioManager.Instance.PlaySound("sword");
        }
        else if (gameObject.name == "axe")
        {
            AudioManager.Instance.PlaySound("axe");
        }
        else if (gameObject.name == "spear")
        {
            AudioManager.Instance.PlaySound("spear");
        }
        else if (gameObject.name == "dagger-weapon")
        {
            AudioManager.Instance.PlaySound("dagger");
        }
        else if (gameObject.name == "crossbow-weapon")
        {
            AudioManager.Instance.PlaySound("bow");
        }
        else if (gameObject.name == "fireball-weapon")
        {
            AudioManager.Instance.PlaySound("fireballShot");
        }
    }
}
