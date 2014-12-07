using UnityEngine;
using System.Collections;

public class StateGameMenu : GameState 
{
	public StateGameMenu(GameManager manager):base(manager){ }
	
	public override void OnStateEntered()
	{
		Application.LoadLevel(0);
	}
	public override void OnStateExit(){}
	public override void StateUpdate() {}
	
	public override void StateGUI() 
	{
		GUILayout.Label("state: MENU");

        if (gameManager.currentState != gameManager.stateGamePlaying)
        {
            if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height - Screen.height * 0.2f, Screen.width * 0.1f, Screen.height * 0.1f), "Play Game"))
            {
                gameManager.NewGameState(gameManager.stateGamePlaying);
            }
        }
	}
}
