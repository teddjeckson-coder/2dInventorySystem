using System;
using InventorySystem;
using UnityEngine;

namespace InventorySystem
{
    internal class Root : MonoBehaviour
    {
        [SerializeField] private InventoryDisplay _display;
        [SerializeField] private ItemData _testItem;
        [SerializeField] private DraggedItem _draggedItem;

        private Inventory _inventory;
        private DraggedController _draggedController;

        public void Awake()
        {
            _inventory = new Inventory(7);
            _inventory.RegisterItem(_testItem);

            _inventory.Initialize(_display);
            _inventory.AddItem(_testItem, 5);

            _draggedController = new DraggedController(_draggedItem, this);
            _inventory.AddItem(_testItem, 6);
            _inventory.RemoveItem(0, 2);
        }

        public void Update()
        {
            _draggedController.Update();
        }
    }

}
