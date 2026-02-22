using UnityEngine;
using UnityEngine.InputSystem;

namespace InventorySystem
{
    internal class DraggedController
    {
        private DraggedItem _draggedItem;
        private bool _isDragging = false;

        public DraggedController(DraggedItem draggedItem)
        {
            _draggedItem = Initialize(draggedItem);
            DisableDraggedItem();
        }

        public void StartDrag(Sprite sprite, int amount)
        {
            EnableDraggedItem();
            _draggedItem.ChangeData(sprite, amount);
            _draggedItem.SetPosition(MousePosition());
        }

        public void Update()
        {
            if (!_isDragging) return;
            
            _draggedItem.SetPosition(MousePosition());
        }

        public void Drop()
        {
            _draggedItem.SetPosition(Vector2.zero);
            DisableDraggedItem();
        }
        
        private DraggedItem Initialize(DraggedItem draggedItem)
            => GameObject.Instantiate(draggedItem);
        
        private void EnableDraggedItem()
            => _draggedItem.gameObject.SetActive(true);
        
        private void DisableDraggedItem()
            => _draggedItem.gameObject.SetActive(false);
        
        private Vector2 MousePosition()
            => Mouse.current.position.ReadValue();
    }
}