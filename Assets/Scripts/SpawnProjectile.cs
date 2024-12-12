using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
	[Header("Projectile Config")]
	public GameObject projectilePrefab;
	public float spawnInterval = 3.0f;

	[Header("Spawn Area Config")]
	public float minX = -10f;  
	public float maxX = 10f;   
	public float minZ = -10f;  
	public float maxZ = 10f;   
	public float spawnHeight = 15f;

	private void Start()
	{
		InvokeRepeating(nameof(Spawner), 0f, spawnInterval);
	}

	private void Spawner()
	{
		if (projectilePrefab == null)
		{
			Debug.LogWarning("Projectile prefab is not assigned!");
			return;
		}

		Vector3 spawnPosition = new Vector3(
			Random.Range(minX, maxX),
			spawnHeight,
			Random.Range(minZ, maxZ)
		);

		Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
	}
}
