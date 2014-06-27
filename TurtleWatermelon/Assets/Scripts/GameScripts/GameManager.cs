using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

public class GameManager : MonoBehaviour {


    public List<BlockEnum.BlockItem> BlockListItems = new List<BlockEnum.BlockItem>();

    private static GameManager m_Instance;

    void Start()
    {
        m_Instance = this;
    }

    public static GameManager Get()
    {
        return m_Instance;
    }

    public GameObject GetPrefabFromType(BlockEnum.Blocks type)
    {
        foreach (BlockEnum.BlockItem block in BlockListItems)
        {
            if (block.type == type)
            {
                return block.prefab;
            }
        }
        return null;
    }

	public bool SaveLevelToFile(string filename, GameObject ParentObject)
    {
        StringBuilder strBuilder = new StringBuilder();
        int ItemCounter = 0;

        // get all objects under the parent ( every block in scene )
        BlockPropertyBase[] LevelItems = ParentObject.GetComponentsInChildren<BlockPropertyBase>();
     
        foreach (BlockPropertyBase item in LevelItems)
        {
            Transform transform = item.gameObject.transform;
            // append the postion
            strBuilder.Append((int)item.BlockType + "," + transform.position.x.ToString() + "," + transform.position.y.ToString() + "," + transform.position.z.ToString());
            // loop through all propertys on the block and append those.
            foreach (BlockEnum.BlockPropertyItem property in item.Propeties)
            {
                strBuilder.Append("," + (int)property.type + "," + property.value.ToString());
            }
            strBuilder.Append("\n");
            ItemCounter++;
        }
        // write to file.
        System.IO.StreamWriter writer = new System.IO.StreamWriter(Application.dataPath + "\\LevelData\\" + filename);
        writer.Write(strBuilder.ToString());
        writer.Close();
        return true;
    }

    public  bool LoadLevelFromFile(string filename, GameObject ParentObject)
    {
        //destroy all blocks in scene.
        foreach (Transform child in ParentObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        System.IO.StreamReader reader = new System.IO.StreamReader(Application.dataPath + "\\LevelData\\" + filename);
        string line;
        // read file line by line
        while ((line = reader.ReadLine()) != null)
        {
            // split line into arry based on comma
            string [] lineItems = line.Split(',');
            // spawn a block with the files position ( no error checking on the parsing )
            GameObject block = GameObject.Instantiate(GetPrefabFromType((BlockEnum.Blocks)int.Parse(lineItems[0])), new Vector3(float.Parse(lineItems[1]), float.Parse(lineItems[2]), float.Parse(lineItems[3])), Quaternion.identity) as GameObject;
            block.transform.parent = ParentObject.transform;
            BlockPropertyBase blockPropertys = block.GetComponent<BlockPropertyBase>();
            // remove base propertys 
            blockPropertys.Propeties.Clear();
            // add on propertys from the file, no error checking.
            for (int i = 4; i < lineItems.Length; i+=2)
            {
                blockPropertys.Propeties.Add(new BlockEnum.BlockPropertyItem((BlockEnum.BlockProperty)int.Parse(lineItems[i]), float.Parse(lineItems[(i + 1)])));
            }
        }

        return false;
    }
}
