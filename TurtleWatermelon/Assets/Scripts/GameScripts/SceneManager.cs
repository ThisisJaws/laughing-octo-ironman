using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour {

    public enum SceneLayer
    {
        MAIN,
        EDITOR,
        MENU,
        HUD,
        LOADING,
        RESULTS,
        OPTIONS
    }

    class SceneHolderItem
    {
        public SceneHolderItem(SceneLayer _layer, GameObject _gameObject, GameObject _sceneRoot)
        {
            layer = _layer;
            gameObject = _gameObject;
        }
        public SceneLayer layer;
        public GameObject gameObject;
        public GameObject sceneRoot;
    }

    private static SceneManager m_Instance;
    private List<SceneHolderItem> m_SceneLayerItems = new List<SceneHolderItem>();
    private List<string> m_ScenesOpen = new List<string>();

    public static SceneManager Get()
    {
        return m_Instance;
    }

    void Awake()
    {
        m_Instance = this;
    }

	void Start () 
    {
        //loop over the layer enum and create a gameobject as a child of the components gameobject.
        foreach (SceneLayer item in SceneLayer.GetValues(typeof(SceneLayer)))
        {
            GameObject itemObj = new GameObject(item.ToString());
            itemObj.transform.parent = transform;
            m_SceneLayerItems.Add(new SceneHolderItem(item, itemObj, null)); 
        }

        OpenScene("Menu", SceneLayer.MENU);
        OpenScene("Options", SceneLayer.OPTIONS);
	
	}

    public void OpenScene(string name, SceneLayer layer, Action LoadedCallback = null)
    {
        if (LoadedCallback != null)
        {
            StartCoroutine(OpenSceneCoroutine(name, layer, LoadedCallback));
        }
        else
        {
            StartCoroutine(OpenSceneCoroutine(name, layer, null));
        }
    }
    IEnumerator OpenSceneCoroutine(string name, SceneLayer layer, Action callback)
    {
        // check that the scene is not open already;
        if (IsSceneOpen(name))
        {
            Debug.LogError("ERROR: Trying to open a already open scene.");
            yield break;
        }

        //load the level
        Application.LoadLevelAdditive(name);

        // wait for it to load
        while (Application.isLoadingLevel)
        {
            yield return new WaitForEndOfFrame();
        }

        // find the scene in the hierarchy
        GameObject sceneLoaded = GameObject.Find(name);

     
        // assing the loaded scene to the correct layer, remove any object already in that layer.
        foreach (SceneHolderItem item in m_SceneLayerItems)
        {
            if (item.layer == layer)
            {
                if (item.sceneRoot != null)
                {
                    GameObject.Destroy(item.sceneRoot);
                }
                sceneLoaded.transform.parent = item.gameObject.transform;
                item.sceneRoot = sceneLoaded;
            }
        }
        
        // add to open scenes
        m_ScenesOpen.Add(name);
        // callback to say scene is loaded.
        if (callback != null)
        {
            callback();
        }
    }

    public void CloseAllScenes( SceneLayer [] exceptions)
    {
        foreach (string openScene in m_ScenesOpen)
        {
            foreach (SceneHolderItem item in m_SceneLayerItems)
            {
                if (item.sceneRoot != null && !exceptions.Any(m => m == item.layer))
                {
                    if (item.sceneRoot.name == openScene)
                    {
                        GameObject.Destroy(item.sceneRoot);
                        item.sceneRoot = null;
                    }
                }
            }
        }
        m_ScenesOpen.Clear();
    }
    public void CloseAllScenes()
    {
        foreach (string openScene in m_ScenesOpen)
        {
            foreach (SceneHolderItem item in m_SceneLayerItems)
            {
                if (item.sceneRoot != null)
                {
                    if (item.sceneRoot.name == openScene)
                    {
                        GameObject.Destroy(item.sceneRoot);
                        item.sceneRoot = null;
                    }
                }
            }
        }
        m_ScenesOpen.Clear();
    }

    public void CloseScene(string name)
    {
       if (IsSceneOpen(name))
       {
           foreach (SceneHolderItem item in m_SceneLayerItems)
           {
               if (item.sceneRoot != null)
               {
                   if (item.sceneRoot.name == name)
                   {
                       GameObject.Destroy(item.sceneRoot);
                       item.sceneRoot = null;
                       m_ScenesOpen.Remove(name);
                   }
               }
           }
       }
       else
       {
           Debug.LogError("ERROR: Trying to close a scene that is not open.");
       }
    }

    public bool IsSceneOpen(string name)
    {
        foreach (string item in m_ScenesOpen)
        {
            if (item == name)
            {
                return true;
            }
        }
        return false;
    }
}
