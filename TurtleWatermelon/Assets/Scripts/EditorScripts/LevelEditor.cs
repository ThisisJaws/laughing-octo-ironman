﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelEditor : MonoBehaviour {

    public GameObject PlaceingPrefab;
    public float BlockSnappingDistance;
    public GameObject LevelItems;

    private GameManager m_GameManger;
    private bool m_PlacingActive = false;
    private GameObject m_PlacingObject = null;
    private BlockEnum.Blocks m_CurrentBlockToPlaceType = BlockEnum.Blocks.NORMAL_BLOCK;
    private BlockEnum.Blocks m_CurrentBlockPropetieType = BlockEnum.Blocks.JUMP_MODIFIER_BLOCK;
    private List<BlockEnum.BlockPropertyItem> m_CurrentBlockProperties = new List<BlockEnum.BlockPropertyItem>();
    private Vector2 m_BlockScrollPosition;
    private Vector2 m_PropertieScrollPosition;
    private Vector2 m_LevelScrollPosition;
    private Rect m_GUIBoxRectLeft;
    private Rect m_GUIBoxRectRight;
    private string m_SaveFileName = "Level1.txt";
    private string [] m_LevelsInDIR;
    private bool m_ShowErrorGUI;
    private string m_CurrentErrorMessage;

	void Start () 
    {
        m_GUIBoxRectLeft = new Rect(0, 0, 200, 800);
        m_GameManger = GameManager.Get();
        m_LevelsInDIR = m_GameManger.ReturnLevelsInDIR();
	}

	void Update () 
    {
        HandlePlacingBlocks();
	}

    void HandlePlacingBlocks()
    {
        // make sure the mouse is not in the GUI
        if (!m_GUIBoxRectLeft.Contains(Input.mousePosition) && !m_GUIBoxRectRight.Contains(Input.mousePosition))
        {
            if (Input.GetMouseButton(0))
            {
                // if we are not placing an object then we should instantiate the placing object
                if (!m_PlacingActive)
                {
                    // raycast, if you hit the background collider then spawn the placing object at the hit, offset Z so it is on this side of the wall. 
                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                    {
                        if (hit.transform.tag == "EditorLevelCollider")
                        {
                            m_PlacingObject = GameObject.Instantiate(PlaceingPrefab, new Vector3(hit.point.x, hit.point.y, -(PlaceingPrefab.transform.localScale.z / 2)), Quaternion.identity) as GameObject;
                            m_PlacingObject.transform.parent = LevelItems.transform;
                            m_PlacingActive = true;
                        }
                    }
                }
                else
                {
                    // if you have placed an object and it is in the scene
                    if (m_PlacingObject != null)
                    {
                        // if your mouse is hitting the editor collider and the mouse is held down then move the placing object to match the mouse
                        RaycastHit hit;
                        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                        {
                            if (hit.transform.tag == "EditorLevelCollider")
                            {
                                m_PlacingObject.transform.position = new Vector3(hit.point.x, hit.point.y, -(PlaceingPrefab.transform.localScale.z / 2));
                            }
                        }

                        #region Snapping

                        // These raycast out from the placing block, if it hits another block then it snaps to it. 
                        // this is all done by the distance of the raycast, increase that and you will increase the distance the blocks snap at.
                        RaycastHit LeftHit;
                        if (Physics.Raycast(m_PlacingObject.transform.position, Vector3.left, out LeftHit, BlockSnappingDistance))
                        {
                            if (LeftHit.transform.tag == "EditorBlock")
                            {
                                m_PlacingObject.transform.position = new Vector3(LeftHit.transform.position.x + PlaceingPrefab.transform.localScale.x,
                                                                                   LeftHit.transform.position.y,
                                                                                   -(PlaceingPrefab.transform.localScale.z / 2));
                            }
                        }
                        RaycastHit RightHit;
                        if (Physics.Raycast(m_PlacingObject.transform.position, Vector3.right, out RightHit, BlockSnappingDistance))
                        {
                            if (RightHit.transform.tag == "EditorBlock")
                            {
                                m_PlacingObject.transform.position = new Vector3(RightHit.transform.position.x - PlaceingPrefab.transform.localScale.x,
                                                                                   RightHit.transform.position.y,
                                                                                   -(PlaceingPrefab.transform.localScale.z / 2));
                            }
                        }
                        RaycastHit TopHit;
                        if (Physics.Raycast(m_PlacingObject.transform.position, Vector3.up, out TopHit, BlockSnappingDistance))
                        {
                            if (TopHit.transform.tag == "EditorBlock")
                            {
                                m_PlacingObject.transform.position = new Vector3(TopHit.transform.position.x,
                                                                                   TopHit.transform.position.y - PlaceingPrefab.transform.localScale.y,
                                                                                   -(PlaceingPrefab.transform.localScale.z / 2));
                            }
                        }
                        RaycastHit BottomHit;
                        if (Physics.Raycast(m_PlacingObject.transform.position, Vector3.down, out BottomHit, BlockSnappingDistance))
                        {
                            if (BottomHit.transform.tag == "EditorBlock")
                            {
                                m_PlacingObject.transform.position = new Vector3(BottomHit.transform.position.x,
                                                                                   BottomHit.transform.position.y + PlaceingPrefab.transform.localScale.y,
                                                                                   -(PlaceingPrefab.transform.localScale.z / 2));
                            }
                        }
                        #endregion
                    }
                }
            }

            // if you have let go of the mouse, meaning you wan to place a block here
            if (Input.GetMouseButtonUp(0))
            {
                // instantiate the right block, set its parent, and add the current editor propertys onto it.
                if (m_PlacingObject != null)
                {
                    // instantiate object
                    GameObject block = GameObject.Instantiate(m_GameManger.GetPrefabFromType(m_CurrentBlockToPlaceType), m_PlacingObject.transform.position, Quaternion.identity) as GameObject;
                    // set its parent
                    block.transform.parent = LevelItems.transform;
                    // set its parent component
                    BlockPropertyBase blockBase = block.GetComponent<BlockPropertyBase>();
                    blockBase.SetBlockParentComponent(LevelItems.GetComponent<BlockParent>());
                    // set all the block propertys
                    List<BlockEnum.BlockPropertyItem> propertys = blockBase.Propeties;
                    propertys.Clear();
                    foreach (BlockEnum.BlockPropertyItem prop in m_CurrentBlockProperties)
                    {
                        propertys.Add(new BlockEnum.BlockPropertyItem(prop));
                    }
                    //destroy placing block
                    GameObject.Destroy(m_PlacingObject);
                    m_PlacingObject = null;
                    m_PlacingActive = false;
                }

            }
            // remove block if the raycast hits it.
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    if (hit.transform.tag == "EditorBlock")
                    {
                        GameObject.Destroy(hit.transform.gameObject);
                    }
                }
            }
        }
    }

    void OnGUI()
    {
 
        GUI.Box(m_GUIBoxRectLeft, "Editor Options");
        GUILayout.Space(20);
        GUILayout.Label("Blocks");
        m_BlockScrollPosition = GUILayout.BeginScrollView(m_BlockScrollPosition, GUILayout.Width(190), GUILayout.Height(380));
        foreach (BlockEnum.BlockItem block in m_GameManger.BlockListItems)
        {
            if (GUILayout.Button(block.type.ToString()))
            {
                m_CurrentBlockToPlaceType = block.type;
            }
        }
        GUILayout.EndScrollView();
        GUILayout.Label("Block Properties:");

        // this will update the propertys in the gui, well get the new propertys when it needs to be updated.
        // notice that you need to shallow copy the list and cant assign due to refrence issues.
        if (m_CurrentBlockPropetieType != m_CurrentBlockToPlaceType)
        {
            GameObject blockPrefab = m_GameManger.GetPrefabFromType(m_CurrentBlockToPlaceType);
            BlockPropertyBase blockPropertie = blockPrefab.GetComponent<BlockPropertyBase>();

            m_CurrentBlockProperties.Clear();
            List<BlockEnum.BlockPropertyItem> propertys = blockPropertie.GetPropertyList();
            foreach (BlockEnum.BlockPropertyItem prop in propertys)
            {
                m_CurrentBlockProperties.Add(new BlockEnum.BlockPropertyItem(prop));
            }
            m_CurrentBlockPropetieType = m_CurrentBlockToPlaceType;
        }

        m_PropertieScrollPosition = GUILayout.BeginScrollView(m_PropertieScrollPosition, GUILayout.Width(190), GUILayout.Height(350));
        if (m_CurrentBlockProperties.Count > 0)
        {
            foreach (BlockEnum.BlockPropertyItem item in m_CurrentBlockProperties)
            {
                GUILayout.Label(item.type.ToString());
                float value;
                if (float.TryParse(GUILayout.TextField(item.value.ToString()), out value))
                {
                    item.value = value;
                }
            }
            
        }
        GUILayout.EndScrollView();

        m_GUIBoxRectRight = new Rect(Screen.width - 120, 0, 120, 300);
        GUI.Box(m_GUIBoxRectRight, "Editor Options");
        GUILayout.BeginArea(m_GUIBoxRectRight);
        GUILayout.Space(30);
        GUILayout.Label("Load Level");
        m_LevelScrollPosition = GUILayout.BeginScrollView(m_LevelScrollPosition, GUILayout.Width(120), GUILayout.Height(100));
        foreach (string item in m_LevelsInDIR)
        {
            if (GUILayout.Button(item))
            {
                m_GameManger.LoadLevelFromFile(item, LevelItems);
            }
        }
        GUILayout.EndScrollView();
        GUILayout.Space(20);
        if (GUILayout.Button("Save Level"))
        {
            m_GameManger.SaveLevelToFile(m_SaveFileName, LevelItems, GUIErrorHandling);
            m_LevelsInDIR = m_GameManger.ReturnLevelsInDIR();
        }
        m_SaveFileName = GUILayout.TextField(m_SaveFileName);
        
        GUILayout.EndArea();

        if (m_ShowErrorGUI)
        {
            GUI.Box(new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 50, 200, 100), "ERROR");
            GUI.Label(new Rect((Screen.width / 2) - 90, (Screen.height / 2) - 25, 200, 100), m_CurrentErrorMessage);
            if (GUI.Button(new Rect((Screen.width / 2) - 37, (Screen.height / 2),75,50),"OK"))
            {
                m_ShowErrorGUI = false;
                m_CurrentErrorMessage = "";
            }
        }


    }

    void GUIErrorHandling(string error)
    {
        m_CurrentErrorMessage = error;
        m_ShowErrorGUI = true;
    }
}
