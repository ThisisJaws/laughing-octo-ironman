using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public GameObject MenuBox;
	public GameObject MenuCamera;
	public int NumberOfLevels;

	private Vector2 m_ScrollViewVector = Vector2.zero;
	private bool m_ShowMainMenu = false;
	private bool m_ShowLevelSelect = false;
	private Rect m_LevelSelectBox;

	void Start()
	{
		m_LevelSelectBox = new Rect (10, 10, 1110, 650);
		m_ShowMainMenu = true;
	}

	void OnGUI()
	{
		if (m_ShowMainMenu) 
		{
			GUI.Box (new Rect (420, 100, 300, 200), "Main Menu");	//the box to hold all the buttons		
			
			
			if (GUI.Button (new Rect (450, 140, 100, 60), "Start")) 
			{
				m_ShowLevelSelect = true;	//show the level select
			}
			
			if (GUI.Button (new Rect (600, 140, 100, 60), "Load")) 
			{
				//load saved game
				Loading();
			}
			
			if (GUI.Button (new Rect (450, 220, 100, 60), "Editor")) 
			{
                Application.LoadLevel("Editor"); 
			}
			
			if (GUI.Button (new Rect (600, 220, 100, 60), "Exit")) 
			{
                Application.Quit();
			}

		}

		if (m_ShowLevelSelect) //if the  Start button is pressed come here and show the level browser
		{
			m_ShowMainMenu = false;	//hiding the main menu

			GUI.Box (m_LevelSelectBox, "Level Select");

			m_ScrollViewVector = GUI.BeginScrollView (new Rect (10, 10, 1110, 650), m_ScrollViewVector, new Rect (0, 0, 1000, 800));


			
			GUI.EndScrollView ();

			//GUI.Box(m_LevelSelectBox, "Level Select");
			//m_ScrollViewVector = GUILayout.BeginScrollView(m_ScrollViewVector, GUILayout.Width(1110), GUILayout.Height(800));
			//GUILayout.EndScrollView();

			if (GUI.Button (new Rect(1000, 660, 60,40), "Back"))
			{
				//going back to the main menu if the player presses back button
				m_ShowLevelSelect = false;
				m_ShowMainMenu = true;
			}
		}

	}

	void Loading ()
	{
		//load from a text file here?
	}

}
