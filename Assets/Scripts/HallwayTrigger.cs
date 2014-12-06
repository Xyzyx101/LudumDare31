using UnityEngine;
using System.Collections;

public class HallwayTrigger : MonoBehaviour {

	public GameObject room1;
	public GameObject wall1;

	public GameObject room2;
	public GameObject wall2;

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
		if(room1.activeSelf)
		{
			wall2.SetActive(false);
			room1.SetActive(false);
			room2.SetActive(true);
			wall1.SetActive(true);
		}
		else
		{
			wall1.SetActive(false);
			room2.SetActive(false);
			room1.SetActive(true);
			wall2.SetActive(true);
		}
	}
}
