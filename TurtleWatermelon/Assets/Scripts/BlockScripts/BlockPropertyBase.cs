using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BlockPropertyBase : MonoBehaviour 
{
    public BlockEnum.Blocks BlockType;

    public List<BlockEnum.BlockPropertyItem> Propeties = new List<BlockEnum.BlockPropertyItem>();

    public List<BlockEnum.BlockPropertyItem> GetDefaultPropertieList()
    {
        return Propeties;
    }
}
