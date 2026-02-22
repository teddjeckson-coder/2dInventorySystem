using InventorySystem;
using UnityEngine;

namespace InventorySystem
{
    internal class Root : MonoBehaviour
    {
        [SerializeField] private InventoryDisplay _display;
        [SerializeField] private ItemData _testItem;

        private Inventory _inventory;

        public void Awake()
        {
            _inventory = new Inventory(7);
            _inventory.RegisterItem(_testItem);

            _inventory.Initialize(_display);
            _inventory.AddItem(_testItem, 5);
            _inventory.AddItem(_testItem, 6);
            _inventory.RemoveItem(0, 2);
        }

    }

}
