using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace LittleWarrior.Managers
{
    public class LW_DataController : MonoBehaviour
    {
        private string _DefaultGameDataFileName = "defaultdata.json";

        private void LoadGameData()
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, 
                                           _DefaultGameDataFileName);
            if(File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
            }
            else
            {
                Debug.LogError(">>>>: Cannot load game data!");
            }
        }
    }
}

