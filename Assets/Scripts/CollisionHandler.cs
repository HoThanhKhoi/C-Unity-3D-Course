using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Movement))]
public class CollisionHandler : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private float levelLoadDelay = 2f;
	[SerializeField] private float crashDelay = 2f;

	[Header("SFX")]
	[SerializeField] private AudioClip crashSFX;
	[SerializeField] private AudioClip successSFX;

	[Header("Particles")]
	[SerializeField] private ParticleSystem crashParticles;
	[SerializeField] private ParticleSystem successParticles;

	private AudioSource audioSource;
	private Movement movement;
	private bool isControllable = true;

	private void Awake()
	{
		InitializeComponents();
	}

	private void InitializeComponents()
	{
		audioSource = GetComponent<AudioSource>();
		movement = GetComponent<Movement>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!isControllable) return;

		HandleCollision(collision.gameObject.tag);
	}

	private void HandleCollision(string collisionTag)
	{
		switch (collisionTag)
		{
			case "Finish":
				StartSuccessSequence(successSFX, successParticles, levelLoadDelay);
				break;

			case "Fuel":
				HandleFuelPickup();
				break;

			case "Obstacle":
				StartCrashSequence(crashSFX, crashParticles, crashDelay);
				break;

			default:
				Debug.LogWarning($"Unhandled collision with tag: {collisionTag}");
				break;
		}
	}


	private void StartSuccessSequence(AudioClip clip, ParticleSystem particles, float delay)
	{
		isControllable = false;

		PlaySoundEffect(clip);
		PlayParticles(particles);
		DisablePlayerControl();
		StartCoroutine(DelayedLoadNextLevel(delay));
	}

	private void StartCrashSequence(AudioClip clip, ParticleSystem particles, float delay)
	{
		isControllable = false;

		PlaySoundEffect(clip);
		PlayParticles(particles);
		DisablePlayerControl();
		StartCoroutine(DelayedReloadLevel(delay));
	}

	private void PlaySoundEffect(AudioClip clip)
	{
		audioSource.Stop();
		if (clip != null)
		{
			audioSource.PlayOneShot(clip);
		}
	}

	private void PlayParticles(ParticleSystem particles)
	{
		if (particles != null && !particles.isPlaying)
		{
			particles.Play();
		}
	}

	private void DisablePlayerControl()
	{
		if (movement != null)
		{
			movement.enabled = false;
		}
	}

	private void HandleFuelPickup()
	{
		Debug.Log("Fuel collected");
	}

	#region Level Load Handling
	private IEnumerator DelayedLoadNextLevel(float delay)
	{
		yield return new WaitForSeconds(delay);
		LoadNextLevel();
	}

	private IEnumerator DelayedReloadLevel(float delay)
	{
		yield return new WaitForSeconds(delay);
		ReloadLevel();
	}

	private void LoadNextLevel()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
		//the genius that i am
		SceneManager.LoadScene(nextSceneIndex);
	}

	private void ReloadLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	#endregion
}
