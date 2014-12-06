using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed = 10.0F;
	//enum Direction { Left, Right, Up, Down};
	//private myDirection;

	private float vSpeed;
	private float hSpeed;
	private float turnSpeed = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update() {
		vSpeed = Input.GetAxis("Vertical") * speed;
		hSpeed = Input.GetAxis("Horizontal") * speed;
		vSpeed *= Time.deltaTime;
		hSpeed *= Time.deltaTime;
		transform.Translate(hSpeed, vSpeed, 0, null);

		UpdateDirection();
	}

	public Transform target;
	void UpdateDirection() {

		Vector3 targetDir = target.position - transform.position;
		float step = turnSpeed * Time.deltaTime;
		float targetAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

		Debug.Log(targetAngle);
		transform.localRotation = Quaternion.Euler(new Vector3(0, 0, targetAngle));

	}
}
