using System;
using Core.Data;

namespace Core.Models
{
    /// <summary>
    /// Класс, представляющий один слот инвентаря.
    /// </summary>
    [Serializable] // Важно для JSON сериализации
    public class InventorySlot
    {
        // ID предмета (для сохранения), само ItemData подгружается отдельно.
        public string itemID;
        public int amount;

        [NonSerialized] // Это поле не сохраняется в JSON, а подгружается из базы предметов при старте
        private ItemData cachedItemData;

        // Свойство для получения данных предмета
        public ItemData ItemData
        {
            get => cachedItemData;
            set => cachedItemData = value;
        }

        // Проверка, пуст ли слот
        public bool IsEmpty => string.IsNullOrEmpty(itemID)  amount == 0;

        // Конструктор для пустого слота
        public InventorySlot()
        {
            itemID = "";
            amount = 0;
            cachedItemData = null;
        }

        // Конструктор для заполненного слота
        public InventorySlot(ItemData data, int itemAmount)
        {
            AssignItem(data, itemAmount);
        }

        // Метод для заполнения слота
        public void AssignItem(ItemData data, int itemAmount)
        {
            cachedItemData = data;
            itemID = data ? data.itemID : "";
            amount = Math.Min(itemAmount, data?.maxStack ?? 0);
        }

        // Метод для очистки слота
        public void Clear()
        {
            cachedItemData = null;
            itemID = "";
            amount = 0;
        }

        // Можно ли добавить еще itemsToAdd штук в этот слот
        public bool CanAddAmount(int itemsToAdd)
        {
            if (IsEmpty  cachedItemData == null) return true;
            return amount + itemsToAdd <= cachedItemData.maxStack;
        }

        // Добавить количество (без проверки, только внутренняя логика)
        public void AddAmount(int itemsToAdd)
        {
            amount += itemsToAdd;
        }
    }
}