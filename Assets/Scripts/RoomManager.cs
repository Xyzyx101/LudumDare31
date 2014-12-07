using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour {
	public static RoomManager Instance { get { return instance; } }
	private static RoomManager instance = null;

	public int roomLevelIncrement = 5;
	public int roomLevelStart = 10;
	public int roomLevelVariation = 8;
	protected int roomLevel;

	public GameObject[] objectsToSpawn;
	public int minObjectsToSpawn = 0;
	public int maxObjectsToSpawn = 3;


	public GameObject[] rooms;

	public GameObject[] halls;

	public GameObject currRoom;

	private void Awake () 
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;        
		} 
		else 
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
		
		//stuff for game(shoudl this be in game playing?    // Eventually
		roomLevel = roomLevelStart;
	}	

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	public int GetRoomLevel()
	{
		return roomLevel;
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
				currRoom = rooms[i];
				roomLevel += roomLevelIncrement;
				//delete everything in room and restart
				rooms[i].SetActive(true);
				MakeRoom();
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

	private void MakeRoom()
	{

		//destroy everything incurr room
		foreach(Transform toDelete in currRoom.transform) 
		{
			Destroy(toDelete.gameObject);
		}

		//then spawn stuff in the current room
		for( int i = 0; i < Random.Range(minObjectsToSpawn, maxObjectsToSpawn + 1); i++)
		{
			int objectToSpawn = Random.Range(0, objectsToSpawn.Length); 
			Vector3 thisSize = currRoom.transform.GetComponent<Renderer>().bounds.size;
			Vector3 spawnPlace = currRoom.transform.position;
			spawnPlace.y += 0.5f;
			spawnPlace.x += Random.Range(-thisSize.x * 0.45f, thisSize.x * 0.45f);
			spawnPlace.z += Random.Range(-thisSize.z * 0.45f, thisSize.z * 0.45f);
			GameObject spawnedObject = (GameObject)Instantiate(objectsToSpawn[objectToSpawn], spawnPlace, objectsToSpawn[objectToSpawn].transform.rotation);
			spawnedObject.transform.parent = currRoom.transform;
		}
	}
}
