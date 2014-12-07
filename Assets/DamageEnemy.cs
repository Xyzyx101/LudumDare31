using UnityEngine;
using System.Collections;

public class DamageEnemy : MonoBehaviour {
	public float damage = 10f;
	void OnTriggerEnter(Collider other) 
	{
		Debug.Log ("stabby stabby");
		other.gameObject.SendMessageUpwards("DoDamage", damage, SendMessageOptions.DontRequireReceiver);
	}

}
