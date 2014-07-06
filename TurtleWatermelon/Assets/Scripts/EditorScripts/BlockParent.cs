using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockParent : MonoBehaviour {


    private GameObject m_Player;
    private PlayerProperties m_PlayerProperties;
    private PlayerController m_PlayerController;
    private GameManager m_GameManager;

    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        if (m_Player)
        {
            m_PlayerController = m_Player.GetComponent<PlayerController>();
            m_PlayerProperties = m_Player.GetComponent<PlayerProperties>();
        }
        m_GameManager = GameManager.Get();

    }

    public void AddPlayerModifier(BlockEnum.BlockPropertyItem modifer)
    {
        if (m_PlayerProperties)
        {
            m_PlayerProperties.PropertyModifiers.Add(modifer);
        }
        else
        {
            Debug.Log("Trying to add a player modifer with null refrence");
        }
    }

    public void RemovePlayerModifier(BlockEnum.BlockPropertyItem modifer)
    {
        if (m_PlayerProperties)
        {
            m_PlayerProperties.PropertyModifiers.Remove(modifer);// this may need to change, it will only get rid of first occurance.
        }
        else
        {
            Debug.Log("Trying to remove a player modifer with null refrence");
        }
    }

    public void AddPlayerAction(BlockEnum.BlockPropertyItem action)
    {
        if (m_PlayerController)
        {
            m_PlayerController.AddPlayerAction(action);
        }
        else
        {
            Debug.Log("Trying to add a player action with a null refrence");
        }
    }

    public void AddGameAction(BlockEnum.BlockPropertyItem action)
    {
        if (m_GameManager)
        {
            m_GameManager.AddGameAction(action);
        }
        else
        {
            Debug.Log("Trying to add a game action with a null refrence");
        }
    }
}
