using System.IO;
using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    internal class InventorySaveData
    {
        public InventorySlot[] slots;
    }

    internal static class SaveSystem
    {
        private static string SavePath =>
            Path.Combine(Application.persistentDataPath, "inventorySave.json");

        public static void SaveInventory(Inventory inventory)
        {
            InventorySaveData data = new InventorySaveData();
            data.slots = inventory.Slots.ToArray();

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(SavePath, json);

            Debug.Log("Inventory Saved, god bless it, finally");
        }

        public static void LoadInventory(Inventory inventory)
        {
            if (!File.Exists(SavePath))
                return;

            string json = File.ReadAllText(SavePath);
            InventorySaveData data = JsonUtility.FromJson<InventorySaveData>(json);

            for (int i = 0; i < inventory.Slots.Count; i++)
            {
                if (i < data.slots.Length)
                {
                    inventory.Slots[i].itemID = data.slots[i].itemID;
                    inventory.Slots[i].amount = data.slots[i].amount;
                }
            }

            Debug.Log("Inventory Loaded");
        }
    }
}