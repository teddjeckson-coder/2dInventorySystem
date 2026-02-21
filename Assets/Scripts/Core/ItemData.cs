using UnityEngine;

namespace Core.Data
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item", order = 0)]
    public class ItemData : ScriptableObject
    {
        [SerializeField] public string iID;
        [SerializeField] public string iname;
        [SerializeField] public Sprite iicon;
        [SerializeField] public int imaxStack = 64;
    }
}