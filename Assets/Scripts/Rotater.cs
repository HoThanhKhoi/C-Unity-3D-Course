using UnityEngine;

public class Rotater : MonoBehaviour
{
	MeshRenderer meshRenderer;
	Rigidbody rb;

	[SerializeField] float rotationSpeed = 10f;

	private void Start()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		rb = GetComponent<Rigidbody>();

		rb.useGravity = false;
		meshRenderer.material.color = Color.cyan;
	}

	void Update()
    {
        transform.Rotate(Vector3.right * rotationSpeed);
    }
}
