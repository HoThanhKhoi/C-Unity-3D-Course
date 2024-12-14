using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class Movement : MonoBehaviour
{
	[Header("Input")]
	[SerializeField] private InputAction thrustAction;
	[SerializeField] private InputAction rotationAction;
	[SerializeField] private float thrustPower = 100f;
	[SerializeField] private float rotationPower = 50f;

	[Header("SFX")]
	[SerializeField] private AudioClip mainEngineSFX;

	[Header("Particles")]
	[SerializeField] private ParticleSystem mainEngineParticles;
	[SerializeField] private ParticleSystem leftThrusterParticles;
	[SerializeField] private ParticleSystem rightThrusterParticles;

	private Rigidbody rb;
	private AudioSource audioSource;

	private void OnEnable()
	{
		EnableInputActions();
	}

	private void OnDisable()
	{
		DisableInputActions();
	}

	private void Awake()
	{
		InitializeComponents();
	}

	private void FixedUpdate()
	{
		HandleThrust();
		HandleRotation();
	}

	private void EnableInputActions()
	{
		thrustAction.Enable();
		rotationAction.Enable();
	}

	private void DisableInputActions()
	{
		thrustAction.Disable();
		rotationAction.Disable();
	}

	private void InitializeComponents()
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	private void HandleThrust()
	{
		if (thrustAction.IsPressed())
		{
			ApplyThrust();
			PlayEngineEffects();
		}
		else
		{
			StopEngineEffects();
		}
	}

	private void ApplyThrust()
	{
		rb.AddRelativeForce(Vector3.up * thrustPower * Time.fixedDeltaTime);
	}

	private void PlayEngineEffects()
	{
		if (!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(mainEngineSFX);
		}

		if (!mainEngineParticles.isPlaying)
		{
			mainEngineParticles.Play();
		}
	}

	private void StopEngineEffects()
	{
		if (audioSource.isPlaying)
		{
			audioSource.Stop();
		}

		if (mainEngineParticles.isPlaying)
		{
			mainEngineParticles.Stop();
		}
	}

	private void HandleRotation()
	{
		float rotationInput = rotationAction.ReadValue<float>();
		if (Mathf.Abs(rotationInput) > Mathf.Epsilon)
		{
			ApplyRotation(rotationInput);
		}
		StopThrusterParticlesIfNeeded(rotationInput);
	}

	private void ApplyRotation(float direction)
	{
		FreezeRotation(true); //disable rigidbody physics rotation 
		transform.Rotate(GetRotationDirection(direction) * rotationPower * Time.fixedDeltaTime);
		FreezeRotation(false); 

		PlayThrusterEffects(direction);
	}

	private Vector3 GetRotationDirection(float direction)
	{
		//if else
		return direction > 0 ? Vector3.back : Vector3.forward;
	}

	private void FreezeRotation(bool freeze)
	{
		rb.freezeRotation = freeze;
	}

	private void PlayThrusterEffects(float direction)
	{
		if (direction > 0)
		{
			PlayParticleEffect(leftThrusterParticles);
			StopParticleEffect(rightThrusterParticles);
		}
		else
		{
			PlayParticleEffect(rightThrusterParticles);
			StopParticleEffect(leftThrusterParticles);
		}
	}

	private void StopThrusterParticlesIfNeeded(float rotationInput)
	{
		if (Mathf.Approximately(rotationInput, 0))
		{
			StopParticleEffect(leftThrusterParticles);
			StopParticleEffect(rightThrusterParticles);
		}
	}

	private void PlayParticleEffect(ParticleSystem particleSystem)
	{
		if (!particleSystem.isPlaying)
		{
			particleSystem.Play();
		}
	}

	private void StopParticleEffect(ParticleSystem particleSystem)
	{
		if (particleSystem.isPlaying)
		{
			particleSystem.Stop();
		}
	}
}
