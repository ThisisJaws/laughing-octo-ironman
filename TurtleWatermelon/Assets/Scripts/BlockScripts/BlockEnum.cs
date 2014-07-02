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
        JUMP_BLOCK,
        DEATH_BLOCK,
        START_BLOCK,
        END_BLOCK,

    }

    public enum BlockProperty
    {
        SPEED_MODIFY,
        JUMP_MODIFIY,
        DEATH,
        JUMP_ACTIVE,
    }
}
