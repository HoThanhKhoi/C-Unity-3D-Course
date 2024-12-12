using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float lifetime;
	[SerializeField] private float velocity;
	[SerializeField] private Transform player;
	Vector3 playerPos;

	private void Start()
	{
		//playerPos = player.transform.position;
	}

	private void Update()
	{
		playerPos = player.transform.position;

		MoveTowardsPlayer();
		DestroyWhenReach();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Debug.Log("Hit Player");
			Destroy(this.gameObject);
		}
	}

	void MoveTowardsPlayer()
	{

		transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
	}

	void DestroyWhenReach()
	{
		if (transform.position == playerPos)
		{
			Destroy(this.gameObject);
		}
	}



}
