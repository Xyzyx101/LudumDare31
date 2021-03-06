﻿using UnityEngine;
using System.Collections;

//globals
public enum stats {
	Vitality,
	Speed,
	Dextarity,
	Defense,
	Strength
};

public class GameManager : MonoBehaviour 
{
	public static GameManager Instance { get { return instance; } }
	private static GameManager instance = null;
	
	public GameState currentState;
	public StateGamePlaying stateGamePlaying{get;set;}
	public StateGameLost stateGameLost{get;set;}
	public StateGameIntro stateGameIntro{get;set;}
	public StateGameMenu stateGameMenu{get;set;}

    public Texture2D gameoverIMG;
    public Player playerScript;

	private void Awake () 
	{
		stateGamePlaying = new StateGamePlaying(this);
		stateGameLost = new StateGameLost(this);
		stateGameIntro = new StateGameIntro(this);
		stateGameMenu = new StateGameMenu(this);
	}	
	
	private void Start () 
	{
		NewGameState( stateGameMenu );
	}
	
	private void Update () 
	{
		if (currentState != null)
		{
			currentState.StateUpdate();
		}
	}
	
	private void OnGUI () 
	{
		if (currentState != null)
		{
			currentState.StateGUI();
		}
	}
	
	public void NewGameState(GameState newState)
	{
		if( null != currentState)
		{
			currentState.OnStateExit();
		}

		playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		currentState = newState;
		currentState.OnStateEntered();
	}
	
    public void SetPLaying()
    {
        NewGameState(stateGamePlaying);
    }
}