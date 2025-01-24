using UnityEngine;

namespace Sleepy {
    [CreateAssetMenu(fileName = "Weapon", menuName = "Create Weapon", order = 0)]
    public class Weapon : ScriptableObject {
        public float fireRate = 1f;
        public float damage = 10f;
        public float projectileSpeed = 20f;
        public Sprite weaponSprite;
        public GameObject projectilePrefab;
        public AudioClip fireSound;
    }
}