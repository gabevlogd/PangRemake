using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class SoundManager : MonoBehaviour, IObserver
{
	public List<AudioClip> AudioClips;

	public AudioSource PlayerEffectsSource;
	public AudioSource EnemiesEffectsSource;
	public AudioSource WorldEffectsSource;
	public AudioSource MusicSource;

	public static SoundManager Instance = null;

	public bool m_MusicOn { get; set; }

	private void Awake()
	{
		if (Instance == null) Instance = this;
		else if (Instance != this) Destroy(gameObject);
		m_MusicOn = true;
	}

	private void OnEnable() => RegisterToEvent();
	private void OnDisable() => UnregisterToEvent();


    /// <summary>
    /// Play a single clip through the sound effects source.
    /// </summary>
    /// <param name="clip">AudioClip to play</param>
    public void PlayPlayerSound(AudioClip clip)
	{
		Debug.Log("Playing: " + clip.name);
		PlayerEffectsSource.clip = clip;
		PlayerEffectsSource.Play();
	}

	public void PlayEnemiesSound(AudioClip clip)
	{
		Debug.Log("Playing: " + clip.name);
		EnemiesEffectsSource.clip = clip;
		EnemiesEffectsSource.Play();
	}

	public void PlayWorldSound(AudioClip clip)
	{
		Debug.Log("Playing: " + clip.name);
		WorldEffectsSource.clip = clip;
		WorldEffectsSource.Play();
	}

	/// <summary>
	/// Play a single clip through the music source.
	/// </summary>
	/// <param name="clip">AudioClip to play</param>
	public void PlayMusic(AudioClip clip)
	{
		//Debug.Log("Playing: " + clip.name);
		MusicSource.clip = clip;
		if (m_MusicOn) MusicSource.Play();
	}

    public void UpdateObserver(string message = null, int value = -1)
    {
		if (message == Constants.BALL) PlayEnemiesSound(AudioClips[0]);
		else if (message == Constants.HOOK) PlayPlayerSound(AudioClips[1]);
		else if (message == Constants.PICK_UP) PlayWorldSound(AudioClips[2]);
		else if (message == Constants.MUSIC) PlayMusic(AudioClips[3]);
	}

	private void RegisterToEvent()
    {
		Ball.Observable.Register(Constants.AUDIO, this);
		GrapplingGun.Observable.Register(Constants.AUDIO, this);
		Shield.Observable.Register(Constants.AUDIO, this);
		GameManager.Instance.Observable.Register(Constants.AUDIO, this);
    }

	private void UnregisterToEvent()
	{
		Ball.Observable.Unregister(Constants.AUDIO, this);
		GrapplingGun.Observable.Unregister(Constants.AUDIO, this);
		Shield.Observable.Unregister(Constants.AUDIO, this);
		GameManager.Instance.Observable.Unregister(Constants.AUDIO, this);
	}
}
