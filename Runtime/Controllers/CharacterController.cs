using UnityEngine;

namespace GameHelperControllerKit.Controllers
{
    public enum MovementMode { Mode2D, Mode3D }
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController : MonoBehaviour
    {
        [Header("Movement Settings")]
        public MovementMode movementMode = MovementMode.Mode3D;
        public float moveSpeed = 0f;

        private Rigidbody rb3D;
        private Rigidbody2D rb2D;
        private Vector3 movement3D;
        private Vector2 movement2D;

        private void Awake()
        {
            rb3D = GetComponent<Rigidbody>();
            rb2D = GetComponent<Rigidbody2D>();

            // Eğer Obje 3D modundaysa Rigidbody2D'yi devre dışı bırak
            if (movementMode == MovementMode.Mode3D && rb2D != null)
            {
                rb2D.simulated = false;
            }
            else if (movementMode == MovementMode.Mode2D && rb3D != null)
            {
                rb3D.isKinematic = true;
            }
        }

        private void Update()
        {
            // Girişleri al
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            if (movementMode == MovementMode.Mode3D)
            {
                movement3D = new Vector3(moveHorizontal, 0.0f, moveVertical);
            }
            else // Mode2D
            {
                movement2D = new Vector2(moveHorizontal, moveVertical);
            }
        }

        private void FixedUpdate()
        {
            if (movementMode == MovementMode.Mode3D && rb3D != null)
            {
                Vector3 newPosition = rb3D.position + movement3D * moveSpeed * Time.fixedDeltaTime;
                rb3D.MovePosition(newPosition);
            }
            else if (movementMode == MovementMode.Mode2D && rb2D != null)
            {
                Vector2 newPosition = rb2D.position + movement2D * moveSpeed * Time.fixedDeltaTime;
                rb2D.MovePosition(newPosition);
            }
        }
    }
}