using InventorySystem;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private InventoryDisplay _display;
    [SerializeField] private ItemData _testItem;

    private Inventory _inventory;

    public void Start()
    {
        _inventory = new Inventory(7);
        _inventory.RegisterItem(_testItem);

        _inventory.Initialize(_display);
        _inventory.AddItem(_testItem, 5);
    }

}
