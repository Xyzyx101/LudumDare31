using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour {
	public int chanceOfStat = 70;
	public float statMultiplier = 0.25f;
	
	public int itemLevel;
	
	public bool enchanted = true;
	
	public int[] itemStats = new int[5];
	
	// Use this for initialization
	void Start () {
		int levelVar = RoomManager.Instance.roomLevelVariation;
		itemLevel = Mathf.FloorToInt(RoomManager.Instance.GetRoomLevel() + (Random.Range(0, levelVar) - (levelVar * 0.5f)));
		
		for (int i = 0; i < itemStats.Length; i++)
		{
			if (Random.Range(0, 100) < chanceOfStat)
			{
				itemStats[i] = Mathf.FloorToInt(Random.Range(itemLevel * 0.4f, itemLevel) * statMultiplier); //does not have negatives
			}
			else
			{
				itemStats[i] = 0;
			}
		}
	}
	
	public int[] getItemStats()
	{
		return itemStats;
	}
	
}