using System;
using CollsionSelectionManager;
using EventManagement;
using UnityEngine;

namespace Games.FlappyBird
{
	public class PlayerScoreOnObstaclePass : MonoBehaviour
	{
		#region PUBLIC_VARS

		#endregion

		#region PRIVATE_VARS
		[SerializeField]private GameEvent updateScoreOnCollide;

		#endregion
		
		#region UNITY_CALLBACKS

		private void OnTriggerEnter2D(Collider2D other)
		{
			CollisionSelectionManager2D.Instance.OnEnterTrigger(transform, CollisionObjectName.ScoreObject, other,()=>Debug.Log(other.transform.name));
		}
		#endregion

		#region PUBLIC_METHODS

		#endregion

		#region PRIVATE_METHODS

		#endregion

	}
}