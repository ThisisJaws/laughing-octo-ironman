using UnityEngine;
using System.Collections;

public class LoadingGUI : MonoBehaviour {

    public Texture2D Background;
    public GUIStyle LabelStyle;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);
        GUI.Label(new Rect((Screen.width / 2), (Screen.height / 2), 100, 100), "LOADING...", LabelStyle);
    }
}
