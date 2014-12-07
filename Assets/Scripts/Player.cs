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

	public class Inventory
	{
		public GameObject primaryWeapon;
		public GameObject secondaryWeapon;
		public GameObject armour;
		public GameObject ring;
	}

	public Inventory inventory = new Inventory();

	private float vSpeed;
	private float hSpeed;

	private int maxHP;
	private int currHP;
    private bool isAlive = true;

	private int primaryDmg;
	private int secondaryDmg;

	private int[] basePlayerStats = new int[]{10, 10, 10, 10, 10};
	private int[] calPlayerStats= new int[5];

	private float desiredAngle;

    public HealthMeter healthScript;

	private void CalculatePlayerStats() 
	{
		//get the current hp so that we can proporly increase your hp
		int prevHP = maxHP;

		//get all the stuff from the equiped items
		for(int i = 0; i < 5; i++)
		{
			calPlayerStats[i] += basePlayerStats[i];
		}

		int pDmg = 0;
		int sDmg = 0;

		if(inventory.primaryWeapon)
		{
			string itemType = inventory.primaryWeapon.tag;
			int[] tempArray;

			WeaponItem script = inventory.primaryWeapon.GetComponent<WeaponItem>();
			tempArray = script.getItemStats();
			pDmg = script.GetDamage();
			for(int i = 0; i < 5; i++)
			{
				calPlayerStats[i] += tempArray[i];
			}
		}

		if(inventory.secondaryWeapon)
		{
			string itemType = inventory.secondaryWeapon.tag;
			int[] tempArray;

			WeaponItem script = inventory.secondaryWeapon.GetComponent<WeaponItem>();
			tempArray = script.getItemStats();
			pDmg = script.GetDamage();
			for(int i = 0; i < 5; i++)
			{
				calPlayerStats[i] += tempArray[i];
			}
		}

		//and then for armour and ring
		if(inventory.armour)
		{
			int[] tempArray;
			Armour script = inventory.armour.GetComponent<Armour>();
			tempArray = script.getItemStats();
			for(int i = 0; i < 5; i++)
			{
				calPlayerStats[i] += tempArray[i];
			}
		}


		if(inventory.ring)
		{
			int[] tempArray;
			Ring script = inventory.ring.GetComponent<Ring>();
			tempArray = script.getItemStats();
			for(int i = 0; i < 5; i++)
			{
				calPlayerStats[i] += tempArray[i];
			}			
		}

		//calc max hp and wepon damage
		maxHP = hpPerVitality * calPlayerStats [(int)stats.Vitality];
		currHP += maxHP - prevHP;

		primaryDmg = pDmg + calPlayerStats [(int)stats.Strength];
		secondaryDmg = sDmg + calPlayerStats [(int)stats.Strength];
	}

    void Awake()
    {
        CalculatePlayerStats();
        healthScript.SetMaxHitPoints(maxHP);
    }

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update() 
    {
        if (isAlive)
        {
            bool primaryAttack = Input.GetMouseButton(0);

            if (primaryWeapon != null && primaryAttack)
            {
                primaryWeapon.SetActive(true);
            }
            bool secondaryAttack = Input.GetMouseButton(1);
			if (secondaryWeapon != null && secondaryAttack)
			{
				secondaryWeapon.SetActive(true);
			}

            if (!primaryAttack && !secondaryAttack)
            {
                float angle = Mathf.LerpAngle(transform.localEulerAngles.y, desiredAngle, 0.1f);
                //Debug.Log("before:"+transform.localEulerAngles.y + "  desired:" + desiredAngle + "  after:" + angle);
                transform.localEulerAngles = new Vector3(0, angle, 0);
            }

            //Quaternion desiredQuat = Quaternion.Euler(new Vector3(0, desiredAngle, 0));
            //transform.localRotation = Quaternion.Lerp(tranform.localRotation, desiredRotation, turnSpeed * Time.deltaTime);

            vSpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            hSpeed = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            vSpeed *= Time.deltaTime;
            hSpeed *= Time.deltaTime;
            transform.Translate(hSpeed, 0, vSpeed, null);
        }
        else
        {
            Debug.Log("FixMe Im Dead");
        }
	}
	
	public void UpdateDirection(Vector3 target) 
    {
		Vector3 targetDir = target - transform.position;
		desiredAngle = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;
	}

	public void PickupPrimaryItem(Vector3  pos, GameObject item)
	{
		Vector2 posThis = new Vector2 (transform.position.x, transform.position.z);
		Vector2 posThat = new Vector2 (pos.x, pos.z);

		if((posThat - posThis).magnitude < 1.5f) 
		{
			switch(item.tag)
			{
			case "Armour":
				if(inventory.armour)
				{
					inventory.armour.transform.parent = null;
					inventory.armour.GetComponent<SpriteRenderer>().enabled = true;
					inventory.armour.GetComponent<SphereCollider>().enabled = true;
				}
				inventory.armour = item;
				break;
			case "Ring":
				if(inventory.ring)
				{
					inventory.ring.transform.parent = null;
					inventory.ring.GetComponent<SpriteRenderer>().enabled = true;
					inventory.ring.GetComponent<SphereCollider>().enabled = true;
				}
				inventory.ring = item;
				break;
			default:
				if(inventory.primaryWeapon)
				{
					inventory.primaryWeapon.transform.parent = null;
					inventory.primaryWeapon.GetComponent<SpriteRenderer>().enabled = true;
					inventory.primaryWeapon.GetComponent<SphereCollider>().enabled = true;
				}
				inventory.primaryWeapon = item;
				break;
			}
			item.transform.parent = transform;
			item.GetComponent<SpriteRenderer>().enabled = false;
			item.GetComponent<SphereCollider>().enabled = false;
			CalculatePlayerStats();
			//init weapon?
		}
	}

	public void PickupSecondaryItem(Vector3  pos, GameObject item)
	{
		Vector2 posThis = new Vector2 (transform.position.x, transform.position.z);
		Vector2 posThat = new Vector2 (pos.x, pos.z);
		
		if((posThat - posThis).magnitude < 1.5f) 
		{
			switch(item.tag)
			{
			case "Armour":
				if(inventory.armour)
				{
					inventory.armour.transform.parent = null;
					inventory.armour.GetComponent<SpriteRenderer>().enabled = true;
					inventory.armour.GetComponent<SphereCollider>().enabled = true;
				}
				inventory.armour = item;
				break;
			case "Ring":
				if(inventory.ring)
				{
					inventory.ring.transform.parent = null;
					inventory.ring.GetComponent<SpriteRenderer>().enabled = true;
					inventory.ring.GetComponent<SphereCollider>().enabled = true;
				}
				inventory.ring = item;
				break;
			default:
				if(inventory.primaryWeapon)
				{
					inventory.secondaryWeapon.transform.parent = null;
					inventory.secondaryWeapon.GetComponent<SpriteRenderer>().enabled = true;
					inventory.secondaryWeapon.GetComponent<SphereCollider>().enabled = true;
				}
				inventory.primaryWeapon = item;
				break;
			}
			item.transform.parent = transform;
			item.GetComponent<SpriteRenderer>().enabled = false;
			item.GetComponent<SphereCollider>().enabled = false;
			CalculatePlayerStats();
			//init weapon?
		}
	}

    public void DoDamage(float damage)
    {
        currHP -= (int)damage;
        healthScript.AlterHealth(-(int)damage);
        if(currHP <= 0)
        {
            isAlive = false;
        }
    }
}
