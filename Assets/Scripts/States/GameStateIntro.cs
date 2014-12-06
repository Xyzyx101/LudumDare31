using UnityEngine;
using System.Collections;

public class StateGameIntro : GameState 
{
	private float timer;
	
	public StateGameIntro(GameManager manager):base(manager){ }
	
	public override void OnStateEntered()
	{
		timer = 1.5f; 
		Application.LoadLevel(0);
	}
	public override void OnStateExit(){}
	public override void StateUpdate() 
	{
		timer -= Time.deltaTime;
		if ( timer <= 0)
		{
			gameManager.NewGameState( gameManager.stateGameMenu );	
		}
	}
	
	public override void StateGUI() 
	{
		GUILayout.Label("state: INTRO " + timer);
	}
}
