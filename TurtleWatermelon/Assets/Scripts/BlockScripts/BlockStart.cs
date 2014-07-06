using UnityEngine;
using System.Collections;

public class BlockStart : BlockPropertyBase {

    public override void OnCollisionEnter(Collision collision)
    {
        // we dont want to do anything on enter
    }

    override public void OnCollisionExit(Collision collision)
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
}
