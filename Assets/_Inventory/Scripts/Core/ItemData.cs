using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Item", order = 0)]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField]
        public int ID { get; private set; }
        public string iname;
        public Sprite iicon;
        public int imaxStack = 64;
    }
}

