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
		transform.Translate(hSpeed, vSpeed, 0);
		UpdateDirection();
	}

	public Transform target;
	void UpdateDirection() {
		if (Mathf.Abs(vSpeed) > Mathf.Abs(hSpeed)) {

			if (vSpeed > 0) {
				Debug.Log ("up");
				//transform.Rotate(Vector3.up, Time.deltaTime, Space.World);
			} else {
				Debug.Log ("down");
				//transform.Rotate(Vector3.down, Time.deltaTime, Space.World);
			}
		} else {
			if (hSpeed > 0) {
				Debug.Log ("right");
				//transform.Rotate(Vector3.right, Time.deltaTime, Space.World);
			} else {
				Debug.Log ("left");
				//transform.Rotate(Vector3.left, Time.deltaTime, Space.World);
			}
		}
		Vector3 targetDir = target.position - transform.position;
		float step = turnSpeed * Time.deltaTime;

		//Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
		//Debug.DrawRay(transform.position, newDir, Color.red);
		//transform.rotation = Quaternion.LookRotation(newDir);
		//transform.Rotate (Vector3.forward * -90);
		transform.Rotate(Vector3.right, Time.deltaTime);
	}
}
