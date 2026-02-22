using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    internal class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] 
        private SlotView slotPrefab;
        [SerializeField] 
        private Transform parent;

        private List<SlotView> _slotViews = new();
        
        public SlotView CreateSlot()
        {
            var slot = Instantiate(slotPrefab, parent);
            _slotViews.Add(slot);
            return slot;
        }
    }
    
}