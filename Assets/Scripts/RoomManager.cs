using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour {

	public GameObject[] rooms;

	public GameObject[] halls;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RoomTransition(int hallNum)
	{
		switch(hallNum)
		{
		case 1:
			if(rooms[0].activeSelf)
			{
				ActivateRoom(1);
				break;
			}
			else
			{
				ActivateRoom(0);
				break;
			}
		case 2:
			if(rooms[1].activeSelf)
			{
				ActivateRoom(2);
				break;
			}
			else
			{
				ActivateRoom(1);
				break;
			}
		case 3:
			if(rooms[2].activeSelf)
			{
				ActivateRoom(3);
				break;
			}
			else
			{
				ActivateRoom(2);
				break;
			}
		case 4:
			if(rooms[3].activeSelf)
			{
				ActivateRoom(0);
				break;
			}
			else
			{
				ActivateRoom(3);
				break;
			}
		default:
			Debug.Log("Invalid hallway trigger");
			break;
		}
	}

	void ActivateRoom(int room)
	{
		int behind = room - 1;
		if(behind < 0)
		{
			behind += 4;
		}
		else if(behind > 3)
		{
			behind -= 4;
		}

		for(int i = 0; i < 4; i++)
		{
			if(room == i)
			{
				rooms[i].SetActive(true);
			}
			else
			{
				rooms[i].SetActive(false);
			}

			if(i == behind)
			{
				halls[i].SetActive(true);
				halls[i].GetComponentInChildren<HallwayTrigger>().wall1.SetActive(true);
				halls[i].GetComponentInChildren<HallwayTrigger>().wall2.SetActive(false);
			}
			else if(i == room)
			{
				halls[i].SetActive(true);
				halls[i].GetComponentInChildren<HallwayTrigger>().wall1.SetActive(false);
				halls[i].GetComponentInChildren<HallwayTrigger>().wall2.SetActive(true);
			}
			else
			{
				halls[i].SetActive(true);
				halls[i].GetComponentInChildren<HallwayTrigger>().wall1.SetActive(false);
				halls[i].GetComponentInChildren<HallwayTrigger>().wall2.SetActive(false);
				halls[i].SetActive(false);
			}
		}
	}
}
