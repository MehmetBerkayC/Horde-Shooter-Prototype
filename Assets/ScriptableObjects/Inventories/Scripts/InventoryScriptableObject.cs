using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName ="InventoryName", menuName ="Inventory System/Inventory")]
public class InventoryScriptableObject : ScriptableObject, ISerializationCallbackReceiver
{
    public event EventHandler OnItemListChanged;

    public string SavePath;

    public ItemDatabaseObject ItemDatabase;
    public List<InventorySlot> Container = new List<InventorySlot>();

    public void AddItem(ItemData item, int amount)
    {
        if (item.IsStackable)
        {
            foreach (InventorySlot slot in Container)
            {
                if (slot.Item == item)
                {
                    slot.AddAmount(amount);
                    return;
                }
            }
            Container.Add(new InventorySlot(ItemDatabase.GetID[item], item, amount));
        }
        else
        {
            Container.Add(new InventorySlot(ItemDatabase.GetID[item], item, amount));
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<InventorySlot> GetItemList()
    {
        return Container;
    }

    /// -------------------------------------
    /// Saving and Loading
    
    public void Save()
    {
        string savedata = JsonUtility.ToJson(this, true);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, SavePath));
        binaryFormatter.Serialize(file, savedata);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, SavePath)))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, SavePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(binaryFormatter.Deserialize(file).ToString(), this);
            file.Close();
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        foreach (InventorySlot slot in Container)
        {
            slot.Item = ItemDatabase.GetItem[slot.ID]; // Get item from ID
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public int ID, Amount;
    public ItemData Item;

    public InventorySlot(int id, ItemData item, int amount)
    {
        ID = id;
        Item = item;
        Amount = amount;
    }

    public void AddAmount(int amount)
    {
        Amount += amount;
    }
}