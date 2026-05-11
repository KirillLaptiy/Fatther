using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Transform cameraTransform;

        [Header("Interaction Zone")] [SerializeField]
        private Vector3 zoneSize = new Vector3(0.8f, 0.8f, 1.2f);

        [SerializeField] private float zoneDistance = 1.2f;

        [Header("Layers")] [SerializeField] private LayerMask grabbableLayerMask = ~0;
        [SerializeField] private LayerMask moveableLayerMask;

        [Header("Move Object")] [SerializeField]
        private float moveObjectSpeed = 8f;

        private readonly Collider[] results = new Collider[1];

        private bool grabRequested;
        private bool isMovingObject;

        private GameObject movingObject;
        private Rigidbody movingRigidbody;
        private float movingObjectDistance;

        public PlayerInventory PlayerInventory { get; private set; }

        private void Awake()
        {
            PlayerInventory = GetComponent<PlayerInventory>();

            if (cameraTransform == null && Camera.main != null)
                cameraTransform = Camera.main.transform;
        }

        // private void Update()
        // {
        //     if (grabRequested)
        //         GrabItem();
        // }

        // private void FixedUpdate()
        // {
        //     if (isMovingObject)
        //         MoveObject();
        // }

        public void OnInteract(InputValue value)
        {
            // value.isPressed: Long press, try to move object
            var foundObject = GetObjectInInteractionZone();

            if (!foundObject) return;

            if (!value.isPressed && foundObject.TryGetComponent(out IInteractable interactable))
                interactable.Interact(this);
        }

        [CanBeNull]
        private GameObject GetObjectInInteractionZone()
        {
            if (cameraTransform == null)
                return null;

            int foundCount = Physics.OverlapBoxNonAlloc(
                GetZoneCenter(),
                zoneSize * 0.5f,
                results,
                GetZoneRotation(),
                LayerMask.GetMask(GameLayers.interactable),
                QueryTriggerInteraction.Collide
            );

            if (foundCount == 0)
                return null;

            return results[0].gameObject;
        }

        // private bool TryStartMoveObject()
        // {
        //     var foundObject = GetObjectInInteractionZone(moveableLayerMask);
        //
        //     if (foundObject == null)
        //         return false;
        //
        //     movingObject = foundObject;
        //     movingRigidbody = movingObject.GetComponent<Rigidbody>();
        //     movingObjectDistance = Vector3.Distance(cameraTransform.position, movingObject.transform.position);
        //     isMovingObject = true;
        //
        //     return true;
        // }
        //
        // private void MoveObject()
        // {
        //     if (movingObject == null || cameraTransform == null)
        //     {
        //         StopMoveObject();
        //         return;
        //     }
        //
        //     Vector3 targetPosition = cameraTransform.position + cameraTransform.forward * movingObjectDistance;
        //
        //     if (movingRigidbody != null)
        //     {
        //         Vector3 nextPosition = Vector3.Lerp(
        //             movingRigidbody.position,
        //             targetPosition,
        //             moveObjectSpeed * Time.fixedDeltaTime
        //         );
        //
        //         movingRigidbody.MovePosition(nextPosition);
        //     }
        //     else
        //     {
        //         movingObject.transform.position = Vector3.Lerp(
        //             movingObject.transform.position,
        //             targetPosition,
        //             moveObjectSpeed * Time.fixedDeltaTime
        //         );
        //     }
        // }
        //
        // private void StopMoveObject()
        // {
        //     isMovingObject = false;
        //     movingObject = null;
        //     movingRigidbody = null;
        // }

        // private void GrabItem()
        // {
        //     grabRequested = false;
        //
        //     var foundItem = GetObjectInInteractionZone(grabbableLayerMask);
        //
        //     if (foundItem == null)
        //         return;
        //
        //     Debug.Log($"Grab item: {foundItem.name}");
        //
        //     Inventory.AddItem(foundItem);
        // }

        private Vector3 GetZoneCenter()
        {
            return cameraTransform.position + cameraTransform.forward * zoneDistance;
        }

        private Quaternion GetZoneRotation()
        {
            return cameraTransform.rotation;
        }

        private void OnDrawGizmos()
        {
            if (cameraTransform == null)
                return;

            Gizmos.color = new Color(0f, 1f, 0.3f, 0.25f);
            Gizmos.matrix = Matrix4x4.TRS(GetZoneCenter(), GetZoneRotation(), Vector3.one);

            Gizmos.DrawCube(Vector3.zero, zoneSize);

            Gizmos.color = new Color(0f, 1f, 0.3f, 1f);
            Gizmos.DrawWireCube(Vector3.zero, zoneSize);

            Gizmos.matrix = Matrix4x4.identity;
        }
    }
}