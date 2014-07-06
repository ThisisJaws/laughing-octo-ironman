using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{

    enum MenuState
    {
        MAIN_MENU,
        LEVEL_LIST,
        OPTIONS,
    }
    

	private Vector2 m_levelScroll;
    private MenuState m_CurrentState;
    private string [] m_LevelsToLoad;
    private GameManager m_GameManager;
    private SceneManager m_SceneManager;

	void Start()
	{
        m_CurrentState = MenuState.MAIN_MENU;
        m_GameManager = GameManager.Get();
        m_SceneManager = SceneManager.Get();
        m_LevelsToLoad = m_GameManager.ReturnLevelsInDIR();

	}

	void OnGUI()
	{

        switch (m_CurrentState)
        {
            case MenuState.MAIN_MENU:

                int menuWidth = 300;
                int menuHeight = 200;
                int boxPosX = (Screen.width / 2) - (menuWidth / 2);
                int boxPosY = (Screen.height / 2) - (menuHeight / 2);

                GUI.Box (new Rect (boxPosX,boxPosY, menuWidth, menuHeight), "Main Menu");	//the box to hold all the buttons		

			    if (GUI.Button (new Rect (boxPosX + 30, boxPosY + 30, 100, 60), "Play")) 
			    {
                    m_CurrentState = MenuState.LEVEL_LIST;
			    }
			    if (GUI.Button (new Rect (boxPosX + 150, boxPosY + 30, 100, 60), "Editor")) 
			    {
                    m_SceneManager.OpenScene("Editor", SceneManager.SceneLayer.EDITOR);
                    m_SceneManager.CloseScene("Menu");
			    }
			    if (GUI.Button (new Rect (boxPosX + 30, boxPosY + 110, 100, 60), "Options")) 
			    {
                    m_CurrentState = MenuState.OPTIONS;
			    }
			    if (GUI.Button (new Rect (boxPosX + 150, boxPosY + 110, 100, 60), "Exit")) 
			    {
                    Application.Quit();
			    }

                break;
            case MenuState.LEVEL_LIST:
                
                int LevelWidth = 500;
                int LevelHeight = 300;
                int lvlBoxPosX = (Screen.width / 2) - (LevelWidth / 2);
                int lvlBoxPosY = (Screen.height / 2) - (LevelHeight / 2);

                GUI.Box(new Rect(lvlBoxPosX, lvlBoxPosY, LevelWidth, LevelHeight), "Level Select");

                GUILayout.BeginArea(new Rect(lvlBoxPosX + 10, lvlBoxPosY + 30, LevelWidth - 10, LevelHeight - 50));
                m_levelScroll = GUILayout.BeginScrollView(m_levelScroll, GUILayout.Width(190), GUILayout.Height(380));
                foreach (string item in m_LevelsToLoad)
                {
                    if (GUILayout.Button(item))
                    {
                        m_GameManager.StartLevel(item);
                    }
                }
                GUILayout.EndScrollView();
                GUILayout.EndArea();
                break;
        }

	}
}
