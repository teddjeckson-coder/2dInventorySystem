using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    [RequireComponent(typeof(RectTransform))]
    internal class DraggedItem : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        [SerializeField] 
        private TMP_Text _amountText;
        private RectTransform _rectTransform;

        private Color _originalColor;
        private Color _transparentColor;
        
        private void Awake()
        {
            _rectTransform = gameObject.GetComponent<RectTransform>();  
            
            //Initialize color and transparent color
            _originalColor = _image.color;
            var color = _originalColor ;
            color.a = 0f;
            _transparentColor = color;
        }

        public void SetPosition(Vector2 position)
            => _rectTransform.transform.position = position;

        public void ChangeData(Sprite sprite, int amount)
        {
            ChangeImage(sprite);
            ChangeAmount(amount);
        }

        public void ChangeImage(Sprite draggedItem)
        {
            _image.color = _originalColor;
            _image.sprite = draggedItem;
        }
        public void ChangeAmount(int amount)
        {
            var clampedValue = Mathf.Clamp(amount, 1, int.MaxValue);
            _amountText.text = clampedValue == 1 ? "" : clampedValue.ToString();
        }

        public void Clear()
        {
            _image.sprite = null;
            _image.color = _transparentColor;
            ChangeAmount(0);
            
        }
    }
}