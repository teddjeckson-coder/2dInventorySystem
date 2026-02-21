using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    internal class SlotView : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private TMP_Text _text;
        
        public void SetItem(Sprite sprite, int amount)
        {
            _image.sprite = sprite;
            ChangeAmount(amount); 
        }

        public void ChangeAmount(int amount)
        {
            var clampedValue = Mathf.Clamp(amount, 1, int.MaxValue);
            _text.text = clampedValue == 1 ? "" : clampedValue.ToString();
        }
    }
}

