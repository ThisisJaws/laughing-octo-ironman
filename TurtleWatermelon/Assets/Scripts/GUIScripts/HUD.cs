using UnityEngine;
using System;
using System.Collections;

public class HUD : MonoBehaviour {

    public GUIStyle LabelStyle;

    private GameManager m_GameManager;
    

    void Start()
    {
        m_GameManager = GameManager.Get();
    }

    void OnGUI()
    {
        if (m_GameManager == null)
        {
            m_GameManager = GameManager.Get();
            return;
        }

        GUI.Box(new Rect(Screen.width - 150, 0, 150, 50),"");

        TimeSpan span = new TimeSpan(0,0,0,0,(int)(m_GameManager.ReturnLevelTime() * 1000));
        string timer = span.Minutes.ToString("D2") + ":" + span.Seconds.ToString("D2") + ":" + span.Milliseconds.ToString("D2");
        GUI.Label(new Rect(Screen.width - 140, 0, 130, 50), timer, LabelStyle);
    
    }
}
