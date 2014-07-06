using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BlockPropertyBase : MonoBehaviour 
{
    public BlockEnum.Blocks BlockType;
    public List<BlockEnum.BlockPropertyItem> Propeties = new List<BlockEnum.BlockPropertyItem>();
    protected BlockParent m_BlockParent;

    public void SetBlockParentComponent(BlockParent ParentComp) { m_BlockParent = ParentComp; }

    public List<BlockEnum.BlockPropertyItem> GetPropertyList()
    {
        return Propeties;
    }

    virtual public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (BlockEnum.BlockPropertyItem item in Propeties)
            {
                BlockEnum.BlockAction action = BlockEnum.PropertyToAction(item.type);
                switch (action)
                {
                    case BlockEnum.BlockAction.PLAYER_MODIFIER:
                        m_BlockParent.AddPlayerModifier(item);
                        break;
                    case BlockEnum.BlockAction.PLAYER_ACTION:
                        m_BlockParent.AddPlayerAction(item);
                        break;
                    case BlockEnum.BlockAction.GAME_ACTION:
                        m_BlockParent.AddGameAction(item);
                        break;
                    case BlockEnum.BlockAction.UNASSIGNED:
                        Debug.LogError("ERROR: Trying to assign a property without a action associated with it");
                        break;
                }
            }
        }
    }

    virtual public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (BlockEnum.BlockPropertyItem item in Propeties)
            {
                BlockEnum.BlockAction action = BlockEnum.PropertyToAction(item.type);
                switch (action)
                {
                    case BlockEnum.BlockAction.PLAYER_MODIFIER:
                        m_BlockParent.RemovePlayerModifier(item);
                        break;
                    case BlockEnum.BlockAction.UNASSIGNED:
                        Debug.LogError("ERROR: Trying to assign a property without a action associated with it");
                        break;
                }
            }
        }
    }

}
