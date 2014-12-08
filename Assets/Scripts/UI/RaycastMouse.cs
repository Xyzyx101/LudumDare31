using UnityEngine;
using System.Collections;

public class RaycastMouse : MonoBehaviour 
{
	public Texture2D iconArrow;
	public Vector2 arrowRegPoint;
	public Texture2D iconInteract;
	public Vector2 interactRegPoint;
	private Vector2 mouseReg;
	private Vector2 mouseCoord;
	private Texture mouseTex;
	public Player playerScript;

	// Use this for initialization
	void Start () {
	}

	void OnDisable()
	{
		Screen.showCursor = true;	
	}
	
    void Update () 
	{
		Screen.showCursor = false;
	}
	
	void OnGUI()
	{
		Vector3 worldPos = Vector3.zero;
		//determine what we hit.
    	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		int mask = 1 << 19;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask)) 
		{
			mouseTex = iconInteract;
			mouseReg = interactRegPoint;
			worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseCoord.x, mouseCoord.y, 20));
			if(Input.GetMouseButtonUp(0))
			{
				playerScript.PickupPrimaryItem(worldPos, hit.collider.gameObject);
			}
			else if (Input.GetMouseButtonUp(1))
			{
				playerScript.PickupSecondaryItem(worldPos, hit.collider.gameObject);
			}
		}
		else
		{
			mouseTex = iconArrow;
			mouseReg = arrowRegPoint;
			if(Input.GetMouseButtonUp(0))
			{
				playerScript.PrimaryAttack();
			}
			else if (Input.GetMouseButtonUp(1))
			{
				playerScript.SecondaryAttack();
			}
		}
		//update texture object.
        GUI.depth = 0;
		GUI.DrawTexture( new Rect(mouseCoord.x-mouseReg.x, Screen.height-mouseCoord.y - mouseReg.y, mouseTex.width, mouseTex.height), mouseTex, ScaleMode.StretchToFill, true, 10.0f);
	
		mouseCoord = Input.mousePosition;
		worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseCoord.x, mouseCoord.y, 20));
		playerScript.UpdateDirection(worldPos);
	}
} 
