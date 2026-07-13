using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    public List<GameObject> levelMapList = new List<GameObject>();
    public void LevelUp(int level)
    {
        foreach (GameObject obj in levelMapList)
        {
            obj.SetActive(false);
        }

        if (level / 3 >= levelMapList.Count - 1)
        {
            levelMapList[levelMapList.Count - 1].SetActive(true);
            return;
        }

        levelMapList[level / 3].SetActive(true);
    }
}
