using MoreMountains.Feedbacks;
using UnityEngine;

namespace Sleepy {
    public class EnemyController : MonoBehaviour {
        public int health;
        public float moveSpeed;
        public MMF_Player hitEffect;

        private Transform player;
        private Rigidbody2D rb;
        private Animator animator;
        private BoxCollider2D boxCollider;
        private Transform modelTransform;

        private bool isDead;

        private void Awake() {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        public void Update() {
            // todo
            // look for the player
            // move towards the player at some speed
        }

        private void FixedUpdate() {
            if (isDead) return;

            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;

            float speed = rb.velocity.sqrMagnitude;
            bool isMoving = (speed > 0.01f);
            animator.SetBool("IsMoving", isMoving);

            // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // angle -= 180f;
            // rb.MoveRotation(angle);

            if (player.position.x < transform.position.x) {
                // Player is LEFT of enemy -> flip Y
                transform.localScale = new Vector3(
                    -Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
            }
            else {
                // Player is RIGHT of enemy -> ensure Y is positive
                transform.localScale = new Vector3(
                    Mathf.Abs(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("Projectile")) {
                health--;
                hitEffect.PlayFeedbacks();
                if (health <= 0) {
                    animator.SetTrigger("Death");
                    boxCollider.enabled = false;
                    rb.velocity = Vector2.zero;
                    isDead = true;
                }
            }
        }
    }
}