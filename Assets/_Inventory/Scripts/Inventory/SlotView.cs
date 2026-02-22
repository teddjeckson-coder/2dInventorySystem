using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace InventorySystem
{
    internal class SlotView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private TMP_Text _text;

        public event Action OnClicked;
        
        private Color _originalColor;
        private Color _transparentColor;
        
        private void Awake()
        {
            //Initialize color and transparent color
            _originalColor = _image.color;
            var color = _originalColor ;
            color.a = 0f;
            _transparentColor = color;
            
            _image.color = _image.sprite == null ?  _transparentColor : _originalColor;
        }
        
        public void SetItem(Sprite sprite, int amount)
        {
            _image.sprite = sprite;
            _image.color = _image.sprite == null ?  _transparentColor : _originalColor;
            ChangeAmount(amount); 
        }

        public void ChangeAmount(int amount)
        {
            var clampedValue = Mathf.Clamp(amount, 1, int.MaxValue);
            _text.text = clampedValue == 1 ? "" : clampedValue.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            
            OnClicked?.Invoke();
        }
    }
}

