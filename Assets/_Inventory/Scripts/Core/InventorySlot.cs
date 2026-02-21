using System;
using UnityEditor.PackageManager.Requests;

namespace InventorySystem
{
    [Serializable]
    internal class InventorySlot
    {
        public int ItemID { get; private set; }
        public int Amount { get; private set; }

        public InventorySlot()
        {
            Clear();
        }

        public bool isEmptySlot
            => ItemID == 0;

        public void Clear()
        {
            ItemID = 0;
            Amount = 0;
        }

        public void SetItem(int newID, int newAmount)
        {
            ItemID = newID;
            Amount = newAmount;
        }

        public void SetAmount(int newAmount)
        {
            Amount = newAmount;
        }
        public void SetID(int newID)
        {
            ItemID = newID;
        }

    }
}