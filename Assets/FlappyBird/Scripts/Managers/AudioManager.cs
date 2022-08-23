using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.FlappyBird
{
	public class AudioManager : MonoBehaviour
	{

		#region PUBLIC_VARS
		public static AudioManager Instance { get; private set; }
		#endregion

		#region PRIVATE_VARS

		[SerializeField] private AudioSource gamePlayAudioSource,uiAudioSource,sfxAudioSource;
		[SerializeField] private AudioDataContainer audioDataContainer;

		#endregion

		#region UNITY_CALLBACKS

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
		}
		#endregion

		#region PUBLIC_METHODS

		public void PlayAudio(AudioFor audioFor, AudioType audioType,bool playInLoop = false)
		{
			AudioData audioData = audioDataContainer.GetAudioData(audioFor);
			int randomAudioClipIndex = Random.Range(0, audioData.audioClip.Length);
			switch (audioType)
			{
				case AudioType.GamePlay:
					PlayAudio(audioData.audioClip[randomAudioClipIndex],gamePlayAudioSource,playInLoop);
					break;
				
				case AudioType.UI:
					PlayAudio(audioData.audioClip[randomAudioClipIndex],uiAudioSource,playInLoop);
					break;
				
				case AudioType.SFX:
					PlayAudio(audioData.audioClip[randomAudioClipIndex],sfxAudioSource,playInLoop);
					break;
			}
			
		}
		#endregion

		#region PRIVATE_METHODS

		private void PlayAudio(AudioClip audioClip,AudioSource audioSource, bool canLoop = false)
		{
			audioSource.clip = audioClip;
			audioSource.loop = canLoop;
			audioSource.Play();
		}
		#endregion

	}
}