using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float speed = 150.0F;
	public float turnSpeed = 0.1f;
	public Transform target;

	public int hpPerVitality;

	public GameObject primaryWeapon;
	public GameObject secondaryWeapon;
	public GameObject Armour;
	public GameObject ring;

	private float vSpeed;
	private float hSpeed;

	private int maxHP;
	private int currHP;

	private int primaryDmg;
	private int secondaryDmg;

	private int[] basePlayerStats = new int[]{10, 10, 10, 10, 10};
	private int[] calPlayerStats= new int[5];

	private float desiredAngle;

    public HealthMeter healthScript;

	private void CalculatePlayerStats() 
	{
		//get all the stuff from the equiped items
		for(int i = 0; i < 5; i++)
		{
			calPlayerStats[i] += basePlayerStats[i];
		}

		int pDmg = 0;
		int sDmg = 0;

		if(primaryWeapon)
		{
			string itemType = primaryWeapon.tag;
			int[] tempArray;
			if(itemType == "Sword")
			{
				Sword script = primaryWeapon.GetComponent<Sword>();
				tempArray = script.getItemStats();
				pDmg = script.GetDamage();
				for(int i = 0; i < 5; i++)
				{
					calPlayerStats[i] += tempArray[i];
				}
			}
			else if(itemType == "Bow")
			{
				Bow script = primaryWeapon.GetComponent<Bow>();
				tempArray = script.getItemStats();
				pDmg = script.GetDamage();
				for(int i = 0; i < 5; i++)
				{
					calPlayerStats[i] += tempArray[i];
				}
			}
			else if(itemType == "Staff")
			{
				Staff script = primaryWeapon.GetComponent<Staff>();
				tempArray = script.getItemStats();
				pDmg = script.GetDamage();
				for(int i = 0; i < 5; i++)
				{
					calPlayerStats[i] += tempArray[i];
				}
			}
		}


		if(secondaryWeapon)
		{
			string itemType = secondaryWeapon.tag;
			int[] tempArray;
			if(itemType == "Sword")
			{
				Sword script = secondaryWeapon.GetComponent<Sword>();
				tempArray = script.getItemStats();
				pDmg = script.GetDamage();
				
				for(int i = 0; i < 5; i++)
				{
					calPlayerStats[i] += tempArray[i];
				}
			}
			else if(itemType == "Bow")
			{
				Bow script = secondaryWeapon.GetComponent<Bow>();
				tempArray = script.getItemStats();
				pDmg = script.GetDamage();
				for(int i = 0; i < 5; i++)
				{
					calPlayerStats[i] += tempArray[i];
				}
			}
			else if(itemType == "Staff")
			{
				Staff script = secondaryWeapon.GetComponent<Staff>();
				tempArray = script.getItemStats();
				pDmg = script.GetDamage();
				for(int i = 0; i < 5; i++)
				{
					calPlayerStats[i] += tempArray[i];
				}
			}
		}

		//and then for armour and ring


		//calc max hp and wepon damage
		maxHP = hpPerVitality * calPlayerStats [(int)stats.Vitality];
		primaryDmg = pDmg + calPlayerStats [(int)stats.Strength];
		secondaryDmg = sDmg + calPlayerStats [(int)stats.Strength];
	}

	// Use this for initialization
	void Start () 
    {
        //healthScript.SetMaxHitPoints();
	}
	
	// Update is called once per frame
	void Update() 
    {
		bool primaryAttack = Input.GetMouseButton(0);

		if (primaryWeapon != null && primaryAttack) 
        {
			primaryWeapon.SetActive(true);
		}
		bool secondaryAttack = Input.GetMouseButton(1);

		if ( !primaryAttack && !secondaryAttack) 
        {
			float angle = Mathf.LerpAngle(transform.localEulerAngles.y, desiredAngle, 0.1f);
			transform.localEulerAngles = new Vector3(0, angle, 0);
		}

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

    public void DoDamage(float damage)
    {
        currHP -= (int)damage;
        healthScript.AlterHealth((int)damage);
    }
}
