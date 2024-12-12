using UnityEngine;

public class Scorer : MonoBehaviour
{
	int hitCount = 0;

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Environment")
		{
			hitCount++;
			other.gameObject.tag = "Hit";
			Debug.Log("Hit Count: " + hitCount);
		}
		else if (other.gameObject.tag == "Hit")
		{
			Debug.Log("You hit something you already hit!");
			return;
		}
	}
}
