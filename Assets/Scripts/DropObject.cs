using UnityEngine;

public class DropObject : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float secondBeforeDrop = 3f;

    MeshRenderer meshRenderer;
    Rigidbody rb;
	bool isDropping = false;

    void Start()
    {
		meshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        meshRenderer.enabled = false;

    }

    void Update()
    {
		if (Time.time > secondBeforeDrop)
		{
            meshRenderer.enabled = true;
            rb.useGravity = true;
		}
    }
}
