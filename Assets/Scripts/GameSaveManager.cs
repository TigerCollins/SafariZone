using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSaveManager : MonoBehaviour
{
    public KeyCode saveButton;
    public static GameSaveManager instance;
    public int sceneCount = 0;

    [Header("Save variables - Creatures")]
    public List<Creature> creatureList = new List<Creature>();

    [Header("Save variables - Inventory")]
    public Backpack inventory;

    [Header("Save variables - Items")]
    public List<Item> itemList = new List<Item>();

    [Header("Save variables - Player")]
    public PlayerData playerData;

    private void Awake()
    {
        /*   if(instance == null)
           {
               instance = this;
               DontDestroyOnLoad(this);
           }

           else if(instance != this)
           {
               Destroy(this);
           }
           */
        sceneCount = PlayerPrefs.GetInt("Scene Count");
        if (sceneCount == 0)
        {
            LoadGame();
            sceneCount += 1;
            PlayerPrefs.SetInt("Scene Count", sceneCount);
            
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(saveButton))
        {
            SaveGame();
        }
    }

    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }

    public void SaveGame()
    {
        if(!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/creature_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/creature_data");
            Debug.Log("MADE NEW SAVE DIRECTORY FOR CREATURES");
        }

        if (!Directory.Exists(Application.persistentDataPath + "/game_save/inventory_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/inventory_data");
            Debug.Log("MADE NEW SAVE DIRECTORY FOR THE INVENTORY");
        }

        if (!Directory.Exists(Application.persistentDataPath + "/game_save/item_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/item_data");
            Debug.Log("MADE NEW SAVE DIRECTORY FOR THE ITEMS");
        }

        SaveCreatures();
        
        SaveItems();
        SaveInventory();

    }

    public void SaveItems()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/game_save/item_data/" + itemList[i] + "_details.txt");
            var json = JsonUtility.ToJson(itemList[i]);
            bf.Serialize(file, json);
            file.Close();
            Debug.Log("SAVED " + itemList[i]);
        }
    }

    public void SaveInventory()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/inventory_data/player_inventory.txt");
        var json = JsonUtility.ToJson(inventory);
        bf.Serialize(file, json);
        file.Close();
        Debug.Log("SAVED INVENTORY");
    }

    public void SaveCreatures()
    {
        for (int i = 0; i < creatureList.Count; i++)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/game_save/creature_data/" + creatureList[i] + "_details.txt");
            var json = JsonUtility.ToJson(creatureList[i]);
            bf.Serialize(file, json);
            file.Close();
            Debug.Log("SAVED " + creatureList[i]);
        }
    }

    public void SavePlayer()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/player_data/player_variables_data.txt");
        var json = JsonUtility.ToJson(playerData);
        bf.Serialize(file, json);
        file.Close();
        Debug.Log("SAVED PLAYER INFO");
    }
    
    public void LoadGame()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/creature_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/creature_data");
        }

        LoadCreatures();
        
        LoadItems();
        LoadInventory();
    }

    public void LoadCreatures()
    {
        for (int i = 0; i < creatureList.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + "/game_save/creature_data/" + creatureList[i] + "_details.txt"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/game_save/creature_data/" + creatureList[i] + "_details.txt", FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), creatureList[i]);
                file.Close();
                Debug.Log("LOADED " + creatureList[i]);
            }
        }
    }

    public void LoadInventory()
    {
        if (File.Exists(Application.persistentDataPath + "/game_save/inventory_data/player_inventory.txt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/inventory_data/player_inventory.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), inventory);
            file.Close();
            Debug.Log("LOADED PLAYER INVENTORY");
        }
    }

    public void LoadPlayerDetails()
    {
        if (File.Exists(Application.persistentDataPath + "/game_save/inventory_data/player_inventory.txt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/player_data/player_variables_data.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), playerData);
            file.Close();
            Debug.Log("LOADED PLAYER INFO");
        }
    }

    public void LoadItems()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + "/game_save/item_data/" + itemList[i] + "_details.txt"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/game_save/item_data/" + itemList[i] + "_details.txt", FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), itemList[i]);
                file.Close();
                Debug.Log("LOADED " + itemList[i]);
            }
        }
    }

    public void WipePlayerPref()
    {
        PlayerPrefs.SetInt("Scene Count", 0);
    }
  /*  public void OnLevelWasLoaded()
    {
        if(SceneManager.GetActiveScene().name == "Main Menu")
        {
            saveButton.onClick.Equals
        }
    }
    */
}
