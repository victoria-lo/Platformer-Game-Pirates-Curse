using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public static class SaveLoad
{
    public static string scene;
    public static int goldCount;
    public static void Save(string sceneName)
    {
        SaveLoad.scene = sceneName;
        SaveLoad.goldCount = GoldCount.goldCount;
        GameObject.Find("AutosaveText").GetComponent<TMPro.TextMeshProUGUI>().alpha = 255;
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/savedData.gd"); //you can call it anything you want
        bf.Serialize(file, SaveLoad.scene);
        bf.Serialize(file, SaveLoad.goldCount);
        file.Close();
    }

    public static void Load()
    {

        if (File.Exists(Application.persistentDataPath + "/savedData.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedData.gd", FileMode.Open);
            SaveLoad.scene = (string)bf.Deserialize(file);
            SaveLoad.goldCount = (int)bf.Deserialize(file);
            file.Close();
            if (scene != null)
            {
                GoldCount.goldCount = SaveLoad.goldCount;
                SceneManager.LoadScene(scene);
            }
        }
    }
}
