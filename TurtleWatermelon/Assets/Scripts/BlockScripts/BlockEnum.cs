using UnityEngine;
using System;
using System.Collections;

public class BlockEnum : MonoBehaviour {

    [Serializable]
    public class BlockItem
    {
        public Blocks type;
        public GameObject prefab;
    }

    [Serializable]
    public class BlockPropertyItem
    {
        public BlockPropertyItem(BlockProperty propertyType, float propertyValue)
        {
            type = propertyType;
            value = propertyValue;
        }
        public BlockPropertyItem(BlockPropertyItem item)
        {
            type = item.type;
            value = item.value;
        }
        public BlockProperty type;
        public float value;
    }

    public enum Blocks
    {
        NORMAL_BLOCK,
        SPEED_BLOCK,
        JUMP_MODIFIER_BLOCK,
        DEATH_BLOCK,
        START_BLOCK,
        END_BLOCK,
        JUMP_ACTIVE_BLOCK

    }

    public enum BlockProperty
    {
        SPEED_MODIFY,
        JUMP_MODIFIY,
        DEATH,
        JUMP_ACTIVE,
        END_GAME,
        RESET_GAME_VALUES,
        START_GAME,
    }

    public enum BlockAction
    {
        GAME_ACTION,
        PLAYER_ACTION,
        PLAYER_MODIFIER, 
        UNASSIGNED
    }

    public static BlockAction PropertyToAction(BlockProperty property)
    {
        switch (property)
        {
            case BlockProperty.JUMP_ACTIVE:
                return BlockAction.PLAYER_ACTION;

            case BlockProperty.JUMP_MODIFIY:
            case BlockProperty.SPEED_MODIFY:
                return BlockAction.PLAYER_MODIFIER;

            case BlockProperty.START_GAME:
            case BlockProperty.END_GAME:
            case BlockProperty.DEATH:
            case BlockProperty.RESET_GAME_VALUES:
                return BlockAction.GAME_ACTION;
            default:
                return BlockAction.UNASSIGNED;
        }
    }
}
