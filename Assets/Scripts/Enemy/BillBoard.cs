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
        Vector3 temp = new Vector3(entity.transform.position.x, entity.transform.position.y + 1, entity.transform.position.z + 2);
        transform.LookAt(temp, Vector3.up);
	}
}
