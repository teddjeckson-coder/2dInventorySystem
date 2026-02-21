using System;
using UnityEditor.PackageManager.Requests;

namespace InventorySystem
{
    [Serializable]
    internal class InventorySlot
    {
        public int itemID;
        public int amount;

        public InventorySlot()
        {
            Clear();
        }

        public bool isEmptySlot
            => itemID == 0;

        public void Clear()
        {
            itemID = 0;
            amount = 0;
        }

        public void SetItem(int newID, int newAmount)
        {
            itemID = newID;
            amount = newAmount;
        }

    }
}