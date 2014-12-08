using UnityEngine;
using System.Collections;

public class DamageEnemy : MonoBehaviour {
	public float damage = 10f;
	public bool StrBonus = false;
	public bool DexBonus = false;
	private Player player;

	public void SetDamage(float newDamage) {
		damage = newDamage;
	}

	void Awake() {
		GameObject playerObject = GameObject.Find("Player");
		player = playerObject.GetComponent<Player>();
	}

	void OnTriggerEnter(Collider other) {
		float totalDamage = damage;
		if (StrBonus && DexBonus) {
			totalDamage += ( player.GetStats(stats.Strength) + player.GetStats(stats.Dextarity) ) / 2;
		} else if (StrBonus) {
			totalDamage += player.GetStats(stats.Strength);
		} else if (DexBonus) {
			totalDamage += player.GetStats(stats.Dextarity);
		}
		other.gameObject.SendMessageUpwards("DoDamage", totalDamage, SendMessageOptions.DontRequireReceiver);
	}
}
