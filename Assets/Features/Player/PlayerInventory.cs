using System.Collections.Generic;
using System.Linq;
using Features.Objects.Items;
using UnityEngine;

namespace Features.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        private readonly List<ItemData> _items = new();

        public ItemData SelectedItem { get; private set; }
        public int SelectedItemIndex { get; private set; }

        public void AddItem(ItemData itemData)
        {
            _items.Add(itemData);
            SelectItem(_items.Count - 1);
        }

        public void ThrowItem(int itemIndex)
        {
            if (_items.Count == 0)
                return;

            if (itemIndex < 0 || itemIndex >= _items.Count)
                return;

            Instantiate(
                _items[itemIndex].WorldPrefab,
                transform.position + transform.forward,
                transform.rotation
            );

            _items.RemoveAt(itemIndex);

            if (_items.Count > 0)
                SelectItem(Mathf.Clamp(itemIndex, 0, _items.Count - 1));
            else
                ClearSelection();
        }

        public void SelectItem(int index)
        {
            if (index < 0 || index >= _items.Count)
                return;

            SelectedItem = _items[index];
            SelectedItemIndex = index;
        }

        private void ClearSelection()
        {
            SelectedItem = null;
            SelectedItemIndex = -1;
        }
    }
}