using Features.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Features.Objects.Items
{
    public class PickupItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemData itemData;
        
        public void Interact(PlayerInteractor interactor)
        {
            interactor.PlayerInventory.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}

