using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    internal class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private SlotView slotPrefab;
        [SerializeField] private Transform parent;

        public List<SlotView> SlotViews { get; private set; } = new List<SlotView>();

        public SlotView CreateSlot()
        {
            var slot = Instantiate(slotPrefab, parent);
            SlotViews.Add(slot);
            return slot;
        }
    }
}