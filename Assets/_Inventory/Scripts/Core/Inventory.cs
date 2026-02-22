using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    internal class Inventory
    {
        private DraggedController _draggedController;
        private int _draggedFromSlot = -1;

        private InventoryDisplay _display;
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
        public void SetDraggedController(DraggedController controller)
        {
            _draggedController = controller;
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
                        {
                            RefreshDisplay();
                            return true;
                        }
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
                    {
                        RefreshDisplay();
                        return true;
                    }
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
            RefreshDisplay();
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
                Debug.Log(from.isEmptySlot);
                Debug.Log(from.ItemID);
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

        private void RefreshDisplay()
        {
            for (int i = 0; i < InventorySize; i++)
            {
                var slot = Slots[i];

                if (!slot.isEmptySlot) 
                {
                    var itemData = GetItemData(slot.ItemID);
                    _display.SlotViews[i].SetItem(itemData.iicon, slot.Amount);
                }
                else
                {
                    _display.SlotViews[i].SetItem(null, 0); // <-- ВАЖНО
                }

            }
        }
        public void Initialize(InventoryDisplay display)
        {
            _display = display;

            // Если UI слотов меньше чем логических — создаём недостающие
            while (_display.SlotViews.Count < InventorySize)
            {
                _display.CreateSlot();
            }

            // Если UI слотов больше чем нужно — обрезаем лишние
            while (_display.SlotViews.Count > InventorySize)
            {
                var last = _display.SlotViews[_display.SlotViews.Count - 1];
                GameObject.Destroy(last.gameObject);
                _display.SlotViews.RemoveAt(_display.SlotViews.Count - 1);
            }

            // Подписываемся на клики
            for (int i = 0; i < InventorySize; i++)
            {
                int index = i;
                _display.SlotViews[i].OnClicked += () => OnSlotClicked(index);
            }

            RefreshDisplay();
        }

        private void OnSlotClicked(int index)
        {
            var slot = Slots[index];

            // Если сейчас ничего не тащим
            if (_draggedFromSlot == -1)
            {
                if (slot.isEmptySlot) return;

                var itemData = GetItemData(slot.ItemID);

                _draggedFromSlot = index;



                _draggedController.StartDrag(itemData.iicon, slot.Amount);
            }
            else
            {
                // Пытаемся перенести
                TransferItem(_draggedFromSlot, index);

                _draggedController.Drop();
                _draggedFromSlot = -1;

                RefreshDisplay();
            }
        }
    }
}