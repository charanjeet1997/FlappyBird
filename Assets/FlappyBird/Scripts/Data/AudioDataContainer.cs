using System;
using UnityEngine;

namespace Games.FlappyBird
{
	[CreateAssetMenu(fileName = "AudioDataContainer" ,menuName = "Containers/AudioDataContainer")]
	public class AudioDataContainer : ScriptableObject
	{

		#region PUBLIC_VARS

		public AudioData[] AudioDatas => audioDatas;

		#endregion

		#region PRIVATE_VARS
		[SerializeField] private AudioData[] audioDatas;

		#endregion

		#region UNITY_CALLBACKS

		#endregion

		#region PUBLIC_METHODS

		public AudioData GetAudioData(AudioFor audioFor)
		{
			return Array.Find(audioDatas, x => x.audioFor == audioFor);
		}
		#endregion

		#region PRIVATE_METHODS

		#endregion

	}
	
	[Serializable]
	public class AudioData
	{
		public AudioClip[] audioClip;
		public AudioFor audioFor;
		public AudioType audioType;
	}

	public enum AudioFor
	{
		Hit,
		Point,
		Fly,
		Die
	}

	public enum AudioType
	{
		GamePlay,
		UI,
		SFX
	}
}