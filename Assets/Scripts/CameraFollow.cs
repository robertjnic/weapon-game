using UnityEngine;

namespace Sleepy {
    public class CameraFollow : MonoBehaviour {
        public Transform target;

        public float smoothSpeed;

        public Vector3 offset;

        private void FixedUpdate() {
            if (!target) return;

            Vector3 desiredPosition = new Vector3(
                target.position.x + offset.x,
                target.position.y + offset.y,
                transform.position.z // Keep camera's current Z
            );

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition,
                smoothSpeed * Time.deltaTime);

            transform.position = smoothedPosition;

            // transform.position = desiredPosition;
        }
    }
}