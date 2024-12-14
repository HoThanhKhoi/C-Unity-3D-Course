using System.Collections;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
	[Header("Movement Configuration")]
	[SerializeField] private Vector3 positionA;
	[SerializeField] private Vector3 positionB;
	[SerializeField] private float movementSpeed = 5f;
	[SerializeField] private float waitTime = 2f;

	private bool isMovingToB = true;
	private Coroutine movementCoroutine;

	private void Start()
	{
		movementCoroutine = StartCoroutine(MoveBetweenPoints());
	}

	private IEnumerator MoveBetweenPoints()
	{
		while (true)
		{
			Vector3 targetPosition = isMovingToB ? positionB : positionA;
			yield return StartCoroutine(MoveToPosition(targetPosition));
			yield return new WaitForSeconds(waitTime);
			isMovingToB = !isMovingToB;
		}
	}

	private IEnumerator MoveToPosition(Vector3 target)
	{
		while (Vector3.Distance(transform.position, target) > 0.01f)
		{
			transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
			yield return null;
		}

		transform.position = target;
	}

	public void SetPositions(Vector3 newPositionA, Vector3 newPositionB)
	{
		positionA = newPositionA;
		positionB = newPositionB;

		if (movementCoroutine != null)
		{
			StopCoroutine(movementCoroutine);
		}

		movementCoroutine = StartCoroutine(MoveBetweenPoints());
	}
}
