using System;
using CollsionSelectionManager;
using UnityEngine;

namespace Games.FlappyBird
{
	public class PlayerObstacleCollision : MonoBehaviour
	{

		#region PUBLIC_VARS
		
		#endregion

		#region PRIVATE_VARS

		#endregion

		#region UNITY_CALLBACKS

		private void OnTriggerEnter2D(Collider2D other)
		{
			CollisionSelectionManager2D.Instance.OnEnterTrigger(transform, CollisionObjectName.Obstacle, other,() => AudioManager.Instance.PlayAudio(AudioFor.Die,AudioType.SFX),() => AudioManager.Instance.PlayAudio(AudioFor.Hit,AudioType.GamePlay));
		}
		#endregion

		#region PUBLIC_METHODS

		#endregion

		#region PRIVATE_METHODS

		#endregion

	}
}