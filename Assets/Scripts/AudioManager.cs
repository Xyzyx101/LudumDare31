using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour 
{
	private static AudioManager instance = null;
	public static AudioManager Instance { get { return instance; } }
	
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		if(instance != null && instance != this)
		{
			Destroy (this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}

        musicOn = true;
        leaveState = false;
	}

    public int musicAmount = 4;
    public GameObject MenuMusicPlayer;
    public GameObject CharMusicPlayer;
    public GameObject IngameMusicPlayer;
    public GameObject VictoryMusicPlayer;
    private GameObject[] Musics;
    public bool musicOn { get; set; }
    public bool leaveState { get; set; }

	public AudioClip button;
    public AudioClip jump;
    public AudioClip objectiveComplete;
    public AudioClip swing;
    public AudioClip death;
    public AudioClip enemyDeath;
    public AudioClip laugh;
    public AudioClip hit;
    public AudioClip guitar;
    public AudioClip leave;
	
	void Start () 
	{
        Musics = new GameObject[musicAmount];
        FillMusics();
	}
	
	void Update () 
	{
        ApplyMute();
	}

    void LateUpdate()
    {

    }

	public void PlaySound(char sound)
	{
		switch(sound)
		{
			case 'B':
                AudioSource.PlayClipAtPoint(button, Camera.main.transform.position);
				break;
            case 'J':
                AudioSource.PlayClipAtPoint(jump, Camera.main.transform.position);
                break;
            case 'O':
                AudioSource.PlayClipAtPoint(objectiveComplete, Camera.main.transform.position);
                break;
            case 'S':
                AudioSource.PlayClipAtPoint(swing, Camera.main.transform.position);
                break;
            case 'D':
                AudioSource.PlayClipAtPoint(death, Camera.main.transform.position);
                break;
            case 'd':
                AudioSource.PlayClipAtPoint(enemyDeath, Camera.main.transform.position);
                break;
            case 'L':
                AudioSource.PlayClipAtPoint(laugh, Camera.main.transform.position);
                break;
            case 'H':
                AudioSource.PlayClipAtPoint(hit, Camera.main.transform.position);
                break;
            case 'G':
                AudioSource.PlayClipAtPoint(guitar, Camera.main.transform.position);
                break;
            case 'l':
                AudioSource.PlayClipAtPoint(leave, Camera.main.transform.position);
                break;
		}
	}

    private void ApplyMute()
    {
        for(int i = 0; i < Musics.Length; ++i)
        {
            string tempName = Application.loadedLevelName + "(Clone)";
            if (musicOn && tempName == Musics[i].gameObject.name)
            {
                Musics[i].audio.mute = false;
                if (!Musics[i].audio.isPlaying)
                {
                    Musics[i].audio.Play();
                }
            }
            else
            {
                Musics[i].transform.parent = gameObject.transform;
                Musics[i].audio.mute = true;
                if (Musics[i].audio.isPlaying)
                {
                    Musics[i].audio.Stop();
                }
            }
        }
    }

    private void FillMusics()
    {
        GameObject add0 = (GameObject)Instantiate(MenuMusicPlayer, new Vector3(0, 0, 0), Quaternion.identity);
        Musics[0] = add0;
        Musics[0].transform.parent = gameObject.transform;
        Musics[0].audio.mute = true;

        GameObject add1 = (GameObject)Instantiate(CharMusicPlayer, new Vector3(0, 0, 0), Quaternion.identity);
        Musics[1] = add1;
        Musics[1].transform.parent = gameObject.transform;
        Musics[1].audio.mute = true;

        GameObject add2 = (GameObject)Instantiate(IngameMusicPlayer, new Vector3(0, 0, 0), Quaternion.identity);
        Musics[2] = add2;
        Musics[2].transform.parent = gameObject.transform;
        Musics[2].audio.mute = true;

        GameObject add3 = (GameObject)Instantiate(VictoryMusicPlayer, new Vector3(0, 0, 0), Quaternion.identity);
        Musics[3] = add3;
        Musics[3].transform.parent = gameObject.transform;
        Musics[3].audio.mute = true;
    }

    public void ToggleMusic(bool toggle)
    {
        musicOn = toggle;
    }

    public bool IsMusicOn()
    {
        return musicOn;
    }
}
