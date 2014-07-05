using UnityEngine;
using System.Collections;

public class OptionsGUI : MonoBehaviour {


    private bool m_OptionsOpen;
    private SceneManager m_SceneManager;

    void Start()
    {
        m_SceneManager = SceneManager.Get();
    }

	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_OptionsOpen = !m_OptionsOpen;
        }
	}

    void OnGUI()
    {
        if (m_OptionsOpen)
        {
            GUI.Box(new Rect((Screen.width / 2) - 75, (Screen.height / 2) - 150, 150, 300), "Options");
            if (GUI.Button(new Rect((Screen.width / 2) - 60, (Screen.height / 2) - 100, 120, 50), "Exit"))
            {
                SceneManager.SceneLayer [] exceptions = new SceneManager.SceneLayer [] { SceneManager.SceneLayer.OPTIONS };
                m_SceneManager.CloseAllScenes(exceptions);
                m_SceneManager.OpenScene("Menu", SceneManager.SceneLayer.MENU);
                m_OptionsOpen = !m_OptionsOpen;

            }

        }


    }
}
