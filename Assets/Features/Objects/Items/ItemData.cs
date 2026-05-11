using UnityEngine;

namespace Features.Objects.Items
{
    [CreateAssetMenu(menuName = "Game/Item Data")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public string ItemName { get; private set; }
        [field: SerializeField] public GameObject WorldPrefab { get; private set; }
    }
}