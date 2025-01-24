using System;
using UnityEngine;

namespace Sleepy {
    public class PlayerMouse : MonoBehaviour {

        public bool isFacingRight;
        
        private Camera cam;

        private void Awake() {
            cam = Camera.main;
        }

        private void Update() {
            var angle = GetAngleToMouse();
            
            // 4. Apply rotation: 0° in code -> sprite faces right
            // This assumes your sprite is drawn facing right in its default orientation.
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            DetermineSpriteOrientation(angle);
        }

        private float GetAngleToMouse() {
            // 1. Get mouse position in world space
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            // 2. Calculate direction
            Vector3 direction = mousePos - transform.position;

            // 3. Convert to angle in degrees
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return angle;
        }

        private void DetermineSpriteOrientation(float angle) {
            // 5. Flip sprite *vertically* if angle is out of [-90, 90]
            //    (Meaning if the gun/weapon would appear upside-down, we flip Y.)
            if (angle > 90f || angle < -90f) {
                // flip Y
                transform.localScale = new Vector3(
                    transform.localScale.x,
                    -Mathf.Abs(transform.localScale.y),
                    transform.localScale.z
                );

                isFacingRight = false;
            }
            else {
                // un-flip (ensure Y is positive)
                transform.localScale = new Vector3(
                    transform.localScale.x,
                    Mathf.Abs(transform.localScale.y),
                    transform.localScale.z
                );

                isFacingRight = true;
            }
        }
    }
}