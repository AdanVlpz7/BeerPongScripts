using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
[RequireComponent(typeof(UserManager))]
public class SaveScript : MonoBehaviour
{
    private UserManager userManager;
    private string savePath; 

    // Start is called before the first frame update
    void Start()
    {
        userManager = GetComponent<UserManager>();
        savePath = Application.persistentDataPath + "/gameSaved.save";
        LoadBallsData();
    }

    public void SaveBallsData()
    {
        var save = new Save()
        {
            SavedBallList = userManager.ballArrayAdded,
            indexBallSaved = AlternatingShopBtn.ballArrayIndex,
        };
        var binaryFormatter = new BinaryFormatter();
        using (var fileStream = File.Create(savePath))
        {
            binaryFormatter.Serialize(fileStream, save);
        }
        Debug.Log("[SaveScript] Data Saved.");
    }
    public void LoadBallsData()
    {
        if (File.Exists(savePath))
        {
            Save save;
            var binaryFormatter = new BinaryFormatter();
            using (var fileStream = File.Open(savePath, FileMode.Open))
            {
                save = (Save)binaryFormatter.Deserialize(fileStream);
            }
            userManager.ballArrayAdded = save.SavedBallList;
            AlternatingShopBtn.ballArrayIndex = save.indexBallSaved;
            Debug.Log("[SaveScript] Data Loaded");
        }
        else
        {
            Debug.LogWarning("[SaveScript] File doesn´t exist.");
        }
        
    }
}
