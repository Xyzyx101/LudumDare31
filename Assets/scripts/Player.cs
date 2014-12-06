using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed = 10.0F;
	public Transform target;

	public GameObject primaryWeapon;
	public GameObject secondaryWeapon;

	private float vSpeed;
	private float hSpeed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update() {
		bool primaryAttack = Input.GetMouseButton(0);
		if (primaryWeapon != null && primaryAttack) {
			primaryWeapon.SetActive(true);
		}

		bool secondaryAttack = Input.GetMouseButton(1);


		vSpeed = Input.GetAxis("Vertical") * speed;
		hSpeed = Input.GetAxis("Horizontal") * speed;
		vSpeed *= Time.deltaTime;
		hSpeed *= Time.deltaTime;
		transform.Translate(hSpeed, vSpeed, 0, null);
	}
	
	public void UpdateDirection(Vector3 targetX) {
		//Vector3 targetDir = target.position - transform.position;
		Vector3 targetDir = targetX - transform.position;
		float targetAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
		transform.localRotation = Quaternion.Euler(new Vector3(0, 0, targetAngle));
	}
}
