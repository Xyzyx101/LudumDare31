using UnityEngine;
using System.Collections;

public class HallwayTrigger : MonoBehaviour {

	public int hallNum;
	public GameObject wall1;
	public GameObject wall2;
	public GameObject roomManager;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
	}

	void OnTriggerExit(Collider other) 
	{
		roomManager.GetComponent<RoomManager>().RoomTransition(hallNum);
	}
}
