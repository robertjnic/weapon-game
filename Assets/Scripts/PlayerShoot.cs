using MoreMountains.Feedbacks;
using Sleepy;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour {
    [Header("References")]
    [SerializeField]
    private Weapon currentWeapon; // Assign a Weapon ScriptableObject here

    [SerializeField]
    private Transform firePoint; // Where the bullets/spawn come from

    [SerializeField]
    private AudioSource audioSource; // For playing SFX (optional)

    private float nextFireTime = 0f; // Tracks when we can fire next

    public MMF_Player shootFeedback;

    private MMF_Rotation rotationFeedback;

    private PlayerMouse playerMouse;

    private Camera cam;

    private void Awake() {
        rotationFeedback = shootFeedback.GetFeedbackOfType<MMF_Rotation>();
        playerMouse = GetComponent<PlayerMouse>();
        cam = Camera.main;
    }

    public void OnAttack(InputValue value) {
        // Basic single-shot approach
        if (Time.time >= nextFireTime) {
            FireWeapon();
            nextFireTime = Time.time + (1f / currentWeapon.fireRate);
        }
    }

    private void FireWeapon() {
        if (currentWeapon.projectilePrefab) {
            // TODO: move this to the player mouse class as a method, take in "to" and "from" params
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            Vector3 direction = (mousePos - firePoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, angle);

            GameObject bullet = Instantiate(
                currentWeapon.projectilePrefab,
                firePoint.position,
                bulletRotation
            );

            Projectile projectileScript = bullet.GetComponent<Projectile>();
            if (projectileScript != null) {
                projectileScript.SetDamage(currentWeapon.damage);
                projectileScript.SetSpeed(currentWeapon.projectileSpeed);
            }


            shootFeedback.PlayFeedbacks();
        }

        if (currentWeapon.fireSound != null && audioSource != null) {
            audioSource.PlayOneShot(currentWeapon.fireSound);
        }

        Debug.Log($"Fired weapon: {currentWeapon.name}");
    }
}