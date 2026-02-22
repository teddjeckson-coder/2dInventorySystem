using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    internal class Inventory
    {
        public List<InventorySlot> Slots { get; private set; }
        public int InventorySize { get; private set; }

        private Dictionary<int, ItemData> itemDB;

        public Inventory(int invSize)
        {
            Slots = new List<InventorySlot>(invSize);

            for (int i = 0; i < invSize; i++)
                Slots.Add(new InventorySlot());

            itemDB = new Dictionary<int, ItemData>();
            InventorySize = invSize;
        }

        public ItemData GetItemData(int id)
        {
            return itemDB.ContainsKey(id) ? itemDB[id] : null;
        }

        public bool AddItem(ItemData item, int amount)
        {
            if (item == null) return false;

            for (int i = 0; i < InventorySize; i++)
            {
                var slot = Slots[i];

                if (!slot.isEmptySlot && slot.ItemID == item.ID)
                {
                    var spaceLeft = item.imaxStack - slot.Amount;

                    if (spaceLeft > 0)
                    {
                        int addAmount = Mathf.Min(spaceLeft, amount);
                        slot.SetAmount(slot.Amount + addAmount);
                        amount -= addAmount;

                        if (amount <= 0)
                            return true;
                    }
                }
            }

            for (int i = 0; i < InventorySize; i++)
            {
                var slot = Slots[i];

                if (slot.isEmptySlot)
                {
                    int addAmount = Mathf.Min(item.imaxStack, amount);
                    slot.SetItem(item.ID, addAmount);
                    amount -= addAmount;

                    if (amount <= 0)
                        return true;
                }
            }

            return false;
        }

        public bool RemoveItem(int slotIndex, int amount)
        {
            if (!IsValidIndex(slotIndex)) return false;

            var slot = Slots[slotIndex];
            if (slot.isEmptySlot) return false;

            if (slot.Amount < amount)
                return false;

            slot.SetAmount(slot.Amount - amount);

            if (slot.Amount <= 0)
                slot.Clear();

            return true;
        }

        public bool TransferItem(int fromSlot, int toSlot)
        {
            if (!IsValidIndex(fromSlot) || !IsValidIndex(toSlot))
                return false;

            if (fromSlot == toSlot)
                return false;

            var from = Slots[fromSlot];
            var to = Slots[toSlot];

            if (from.isEmptySlot)
                return false;

            if (to.isEmptySlot)
            {
                to.SetItem(from.ItemID, from.Amount);
                from.Clear();
                return true;
            }

            if (from.ItemID == to.ItemID)
            {
                var itemData = GetItemData(from.ItemID);
                int maxStack = itemData.imaxStack;

                int total = from.Amount + to.Amount;

                if (total <= maxStack)
                {
                    to.SetAmount(total);
                    from.Clear();
                }
                else
                {
                    to.SetAmount(maxStack);
                    from.SetAmount(total - maxStack);
                }

                return true;
            }
            int tempID = to.ItemID;
            int tempAmount = to.Amount;

            to.SetItem(from.ItemID, from.Amount);
            from.SetItem(tempID, tempAmount);

            return true;
        }

        public bool IsInventoryFull()
        {
            foreach (var slot in Slots)
            {
                if (slot.isEmptySlot)
                    return false;
            }
            return true;
        }

        private bool IsValidIndex(int index)
        {
            return index >= 0 && index < InventorySize;
        }
        public void RegisterItem(ItemData item)
        {
            if (!itemDB.ContainsKey(item.ID))
                itemDB.Add(item.ID, item);
        }
    }
}