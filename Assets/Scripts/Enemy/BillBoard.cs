using UnityEngine;
using System.Collections;

public class BillBoard : MonoBehaviour 
{
    public GameObject entity;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 temp = new Vector3(entity.transform.position.x, entity.transform.position.y, entity.transform.position.z - 0.2f);
		this.transform.position = temp;
		this.transform.rotation = Camera.main.transform.rotation;
		//this.gameObject.transform.rotation.Set(90, 0, 0, 0);
	}
}
