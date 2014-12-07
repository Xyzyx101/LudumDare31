﻿using UnityEngine;
using System.Collections;

public class RaycastMouse : MonoBehaviour 
{
	public Texture2D iconArrow;
	public Vector2 arrowRegPoint;
	public Texture2D iconAttack;
	public Vector2 attackRegPoint;
	public Texture2D iconTalk;
	public Vector2 talkRegPoint;
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


		if (Physics.Raycast(ray, out hit)) 
		{
			switch(hit.collider.tag)
			{
				case "Enemy":
					mouseTex = iconAttack;
					mouseReg = attackRegPoint;
				break;
				case "NPC":
					mouseTex = iconTalk;
					mouseReg = talkRegPoint;
				break;
				case "Interact":
					mouseTex = iconInteract;
					mouseReg = interactRegPoint;
				break;
				default:
					mouseTex = iconArrow;
					mouseReg = arrowRegPoint;
				break;
			}
			if(hit.collider.gameObject.layer == 19)
			{
				worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseCoord.x, mouseCoord.y, 20));
				if(Input.GetMouseButton(0))
				{
					playerScript.PickupPrimaryItem(worldPos);
				}
				else if (Input.GetMouseButton(1))
				{
					playerScript.PickupSecondaryItem(worldPos);
				}
			}
		}
		else
		{
			mouseTex = iconArrow;
			mouseReg = arrowRegPoint;
		}
		//update texture object.
        GUI.depth = 0;
		GUI.DrawTexture( new Rect(mouseCoord.x-mouseReg.x, Screen.height-mouseCoord.y - mouseReg.y, mouseTex.width, mouseTex.height), mouseTex, ScaleMode.StretchToFill, true, 10.0f);
	
		mouseCoord = Input.mousePosition;
		worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseCoord.x, mouseCoord.y, 20));
		playerScript.UpdateDirection(worldPos);
	}
} 
