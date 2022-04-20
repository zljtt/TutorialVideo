using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

[Serializable]
public class PlayerData
{
    public Dictionary<string, bool> collected;
    public int score;

    [NonSerialized] // it will notify the compiler not to serialize this field even if it is inside a Serializable class
    public int doNotSerialize;
}

public class LevelManager : MonoBehaviour
{
    public PlayerData data;
    public string directory;

    /*  
    *   Event Sequence:
    *   Awake
    *   OnEnable
    *   Start
    *   FixedUpdate
    *   Undate
    */
    void Awake()
    {
        // obtain all collectables in the scene
        GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectable");
        // obtain stored data
        BinaryFormatter formatter = new BinaryFormatter();
        directory = Application.persistentDataPath + "/collectables.dat";
        if (File.Exists(directory))
        {
            FileStream file = File.Open(directory, FileMode.Open);
            data = formatter.Deserialize(file) as PlayerData;
            // disable tbe ones that have been collected
            foreach (GameObject collectable in collectables)
            {
                collectable.SetActive(!data.collected[collectable.name]);
            }
            file.Close();
        }
        else
        {
            data = new PlayerData();
            // init state for every collectables
            data.collected = new Dictionary<string, bool>();
            foreach (GameObject collectable in collectables)
            {
                data.collected[collectable.name] = false;
            }
            // init score
            data.score = 0;
        }
    }

    void OnDestroy()
    {
        // write into file
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(directory);
        formatter.Serialize(file, data);
        file.Close();
    }
}
