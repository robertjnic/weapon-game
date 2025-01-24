using System.Collections;
using UnityEngine;

namespace Sleepy {
    public class EnemySpawner : MonoBehaviour {
        [Header("Spawner Settings")]
        [Tooltip("The Enemy prefab to spawn.")]
        public GameObject enemyPrefab;

        [Tooltip("Time (in seconds) between spawns.")]
        public float spawnInterval = 3f;

        [Tooltip("How many enemies to spawn in total. Set to -1 for unlimited spawns.")]
        public int maxSpawns = 5;

        [Tooltip("Radius around the spawner to place enemies randomly.")]
        public float spawnRadius = 5f;

        private float timer;
        private int spawnCount;

        public int waveNumber = 0;
        public float waveDelay = 5f;
        public int enemiesPerWave = 5;

        private bool waveInProgress;
        private int enemiesSpawnedInWave;

        private void Update() {
            if (!waveInProgress) {
                StartCoroutine(SpawnWave());
            }
        }

        private IEnumerator SpawnWave() {
            waveInProgress = true;
            waveNumber++;

            // Spawn the wave
            for (int i = 0; i < enemiesPerWave; i++) {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnInterval);
            }

            // Wait for some condition, e.g., waveDelay
            yield return new WaitForSeconds(waveDelay);

            waveInProgress = false;
        }

        private void SpawnEnemy() {
            // random offset in a circle
            Vector2 randomPos = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = transform.position + new Vector3(randomPos.x, randomPos.y, 0f);

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            if (maxSpawns != -1) spawnCount++;
        }
    }
}