using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Item", order = 0)]
    public class ItemData : ScriptableObject
    {
        public int iID;
        public string iname;
        public Sprite iicon;
        public int imaxStack = 64;
    }
}