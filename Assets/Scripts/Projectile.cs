using UnityEngine;

namespace Sleepy {
    public class Projectile : MonoBehaviour {
        private float damage;
        private float speed;
        private Rigidbody2D rb;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start() {
            rb.velocity = transform.right * speed;
        }

        public void SetDamage(float dmg) => damage = dmg;
        public void SetSpeed(float spd) => speed = spd;

        private void OnTriggerEnter2D(Collider2D other) {
            Destroy(gameObject);
        }
    }
}