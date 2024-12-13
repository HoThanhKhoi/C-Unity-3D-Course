using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
	[SerializeField] InputAction thrust;
	[SerializeField] InputAction rotation;
	[SerializeField] float thrustPower = 100f;
	[SerializeField] float rotationPower = 50f;
	Rigidbody rb;



	private void OnEnable()
	{
		thrust.Enable();
		rotation.Enable();
	}

	private void OnDisable()
	{
		thrust.Disable();
		rotation.Disable();
	}

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		Thrusting();
		Rotate();
	}

	private void Thrusting()
	{

		if (thrust.IsPressed())
		{
			rb.AddRelativeForce(Vector3.up * thrustPower * Time.fixedDeltaTime);
		}
	}

	private void Rotate()
	{
		float rotationInput = rotation.ReadValue<float>();

		if (rotationInput < 0)
		{
			rb.freezeRotation = true;
			transform.Rotate(Vector3.forward * rotationPower * Time.fixedDeltaTime);
			rb.freezeRotation = false;
		}
		else if (rotationInput > 0)
		{
			rb.freezeRotation = true;
			transform.Rotate(Vector3.back * rotationPower * Time.fixedDeltaTime);
			rb.freezeRotation = false;
		}
	}
}
