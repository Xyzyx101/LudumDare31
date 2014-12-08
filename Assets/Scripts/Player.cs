using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float speedMultiplier = 15;
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
    public StatGUI statGui;

    public static bool go { get; set; }

    void Awake()
    {
        go = false;
        CalculatePlayerStats();
        healthScript.SetMaxHitPoints(maxHP);
    }

	private void CalculatePlayerStats() 
	{
		//get the current hp so that we can proporly increase your hp
		int prevHP = maxHP;

		//get all the stuff from the equiped items
		for(int i = 0; i < 5; i++)
		{
			calPlayerStats[i] = basePlayerStats[i];
		}

		if(inventory.primaryWeapon)
		{
			string itemType = inventory.primaryWeapon.tag;
			int[] tempArray;

			WeaponItem script = inventory.primaryWeapon.GetComponent<WeaponItem>();
			tempArray = script.getItemStats();
			if(script.enchanted)
			{
				for(int i = 0; i < 5; i++)
				{
					calPlayerStats[i] += tempArray[i];
				}
			}
		}

		if(inventory.secondaryWeapon)
		{
			string itemType = inventory.secondaryWeapon.tag;
			int[] tempArray;

			WeaponItem script = inventory.secondaryWeapon.GetComponent<WeaponItem>();
			tempArray = script.getItemStats();
			if(script.enchanted)
			{
				for(int i = 0; i < 5; i++)
				{
					calPlayerStats[i] += tempArray[i];
				}
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
				if(script.enchanted || i == (int)stats.Defense)
				{
					calPlayerStats[i] += tempArray[i];
				}
			}
		}


		if(inventory.ring)
		{
			int[] tempArray;
			Ring script = inventory.ring.GetComponent<Ring>();
			tempArray = script.getItemStats();
			if(script.enchanted)
			{
				for(int i = 0; i < 5; i++)
				{
					calPlayerStats[i] += tempArray[i];
				}
			}		
		}

		//calc max hp
		maxHP = hpPerVitality * calPlayerStats[(int)stats.Vitality];
		currHP += maxHP - prevHP;
		healthScript.SetMaxHitPoints(maxHP);
		//calc speed
		speed = speedMultiplier * calPlayerStats[(int)stats.Speed];
	}
	
	// Update is called once per frame
	void Update() 
    {
        if(go)
        {
            if (isAlive)
            {
                bool primaryAttack = Input.GetMouseButtonDown(0);

                if (primaryWeapon != null && primaryAttack)
                {
					primaryWeapon.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
					secondaryWeapon.SetActive(false);
					primaryWeapon.SetActive(true);
                }
                bool secondaryAttack = Input.GetMouseButtonDown(1);
			    if (secondaryWeapon != null && secondaryAttack)
			    {
					secondaryWeapon.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
					primaryWeapon.SetActive(false);
					secondaryWeapon.SetActive(true);
			    }

                if (!primaryAttack && !secondaryAttack)
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
            else
            {
                Debug.Log("FixMe Im Dead");
            }
        }
	}

	public int GetStats(stats stat) {
		return calPlayerStats[(int)stat];
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
					inventory.armour.transform.parent = item.transform.parent;
					inventory.armour.GetComponent<SpriteRenderer>().enabled = true;
					inventory.armour.GetComponent<SphereCollider>().enabled = true;
				}
				inventory.armour = item;
				break;
			case "Ring":
				if(inventory.ring)
				{
					inventory.ring.transform.parent = item.transform.parent;
					inventory.ring.GetComponent<SpriteRenderer>().enabled = true;
					inventory.ring.GetComponent<SphereCollider>().enabled = true;
				}
				inventory.ring = item;
				break;
			default:
				if(inventory.primaryWeapon)
				{
					if( inventory.primaryWeapon.name == "StaffItem" ) {
						ProjectileWeapon weaponScript = primaryWeapon.GetComponent<ProjectileWeapon>();
						WeaponItem itemScript = inventory.primaryWeapon.GetComponent<WeaponItem>();
						itemScript.charges = weaponScript.charges;
					}
					inventory.primaryWeapon.transform.parent = item.transform.parent;
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
		}
		Transform newWeapon;
		if(newWeapon = transform.Find("WeaponBindPoint/" + inventory.primaryWeapon.tag)) {
			primaryWeapon = newWeapon.gameObject;
			WeaponItem script = inventory.primaryWeapon.GetComponent<WeaponItem>();
			float pDmg = script.GetDamage();
			Weapon weapon = primaryWeapon.GetComponent<Weapon>();
			if ( weapon ) {
				weapon.InitWithDamage(pDmg);
			}
			ProjectileWeapon projWeapon = primaryWeapon.GetComponent<ProjectileWeapon>();
			if ( projWeapon ) {
				projWeapon.InitWithDamageAndCharges(pDmg, script.charges);
			}
		}
        statGui.UpdateWeapons();
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
					inventory.armour.transform.parent = item.transform.parent;
					inventory.armour.GetComponent<SpriteRenderer>().enabled = true;
					inventory.armour.GetComponent<SphereCollider>().enabled = true;
				}
				inventory.armour = item;
				break;
			case "Ring":
				if(inventory.ring)
				{
					inventory.ring.transform.parent = item.transform.parent;
					inventory.ring.GetComponent<SpriteRenderer>().enabled = true;
					inventory.ring.GetComponent<SphereCollider>().enabled = true;
				}
				inventory.ring = item;
				break;
			default:
				if(inventory.secondaryWeapon)
				{
					if( inventory.secondaryWeapon.name == "StaffItem" ) {
						ProjectileWeapon weaponScript = secondaryWeapon.GetComponent<ProjectileWeapon>();
						WeaponItem itemScript = inventory.secondaryWeapon.GetComponent<WeaponItem>();
						itemScript.charges = weaponScript.charges;
					}
					inventory.secondaryWeapon.transform.parent = item.transform.parent;
					inventory.secondaryWeapon.GetComponent<SpriteRenderer>().enabled = true;
					inventory.secondaryWeapon.GetComponent<SphereCollider>().enabled = true;
				}
				inventory.secondaryWeapon = item;
				break;
			}
			item.transform.parent = transform;
			item.GetComponent<SpriteRenderer>().enabled = false;
			item.GetComponent<SphereCollider>().enabled = false;
			CalculatePlayerStats();
			//init weapon?
		}
		Transform newWeapon = transform.Find("WeaponBindPoint/" + inventory.secondaryWeapon.tag);
		if( newWeapon ) {
			secondaryWeapon = newWeapon.gameObject;
			WeaponItem script = inventory.secondaryWeapon.GetComponent<WeaponItem>();
			float pDmg = script.GetDamage();
			Weapon weapon = secondaryWeapon.GetComponent<Weapon>();
			if ( weapon ) {
				weapon.InitWithDamage(pDmg);
			}
			ProjectileWeapon projWeapon = secondaryWeapon.GetComponent<ProjectileWeapon>();
			if ( projWeapon ) {
				projWeapon.InitWithDamageAndCharges(pDmg, script.charges);
			}
		}
        statGui.UpdateWeapons();
	}

    public void DoDamage(float damage)
    {
		//apply defence to damage
		damage = damage * (100/(100 + calPlayerStats[(int)stats.Defense]));

        currHP -= Mathf.CeilToInt(damage);//so you always take at least 1 damage.
        healthScript.AlterHealth(-(int)damage);
        if(currHP <= 0)
        {
            isAlive = false;
        }
    }
}
