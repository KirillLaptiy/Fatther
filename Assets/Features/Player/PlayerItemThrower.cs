using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Player
{
    public class PlayerItemThrower : MonoBehaviour
    {
        private PlayerInventory _playerInventory;
        
        private void Awake()
        {
            _playerInventory = GetComponent<PlayerInventory>();
        }
        
        public void OnDrop(InputValue value)
        {
            _playerInventory.ThrowItem(_playerInventory.SelectedItemIndex);
        }
    }
}
