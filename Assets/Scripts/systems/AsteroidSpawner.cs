using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    Transform worldSpaceCanvas;
    public Asteroid asteroidPrefab;
    public float spawnDistance = 12.0f;
    public float spawnRate = 1.0f;
    public int amountPerSpawn = 1;
    [Range(0.0f, 45.0f)]
    public float trajectoryVariance = 15.0f;

    public static int asteroidsHealth = 1;

    public int round = 0;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    public void Spawn()
    {
        for (int i = 0; i < this.amountPerSpawn; i++)
        {
            // Choose a random direction from the center of the spawner and
            // spawn the asteroid a distance away
            Vector2 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = spawnDirection * this.spawnDistance;

            // Offset the spawn point by the position of the spawner so its
            // relative to the spawner location
            spawnPoint += this.transform.position;

            // Calculate a random variance in the asteroid's rotation which will
            // cause its trajectory to change
            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            // Create the new asteroid by cloning the prefab and set a random
            // size within the range
            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation, worldSpaceCanvas);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);

            asteroid.health = Random.Range(1, asteroidsHealth);

            // Set the trajectory to move in the direction of the spawner
            Vector2 trajectory = rotation * -spawnDirection;
            asteroid.SetTrajectory(trajectory);
        }

        round++;

        if(round % 2 == 0)
        {
            asteroidsHealth++;
        }
        
    }

}
