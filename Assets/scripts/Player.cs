using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed = 150.0F;
	public float turnSpeed = 0.1f;
	public Transform target;

	public GameObject primaryWeapon;
	public GameObject secondaryWeapon;

	private float vSpeed;
	private float hSpeed;

	private int[] playerStats = new int[5];

	private float desiredAngle;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update() {
		bool primaryAttack = Input.GetMouseButton(0);

		if (primaryWeapon != null && primaryAttack) 
        {
			primaryWeapon.SetActive(true);
		}
		bool secondaryAttack = Input.GetMouseButton(1);

		if ( !primaryAttack && !secondaryAttack) 
        {
			float angle = Mathf.LerpAngle(transform.localEulerAngles.y, desiredAngle, 0.1f);
			//Debug.Log("before:"+transform.localEulerAngles.y + "  desired:" + desiredAngle + "  after:" + angle);
			transform.localEulerAngles = new Vector3(0, angle, 0);
		}




		//Quaternion desiredQuat = Quaternion.Euler(new Vector3(0, desiredAngle, 0));
		//transform.localRotation = Quaternion.Lerp(tranform.localRotation, desiredRotation, turnSpeed * Time.deltaTime);

		vSpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime;
		hSpeed = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		vSpeed *= Time.deltaTime;
		hSpeed *= Time.deltaTime;
		transform.Translate(hSpeed, 0, vSpeed, null);
	}
	
	public void UpdateDirection(Vector3 target) 
    {
		Vector3 targetDir = target - transform.position;
		desiredAngle = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;
	}
}

;
