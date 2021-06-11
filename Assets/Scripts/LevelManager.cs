

namespace GoblinRush
{
	using System.Collections;
	using System.Collections.Generic;
	using System;
	using UnityEngine;
	using System.Linq;
	using SDD.Events;

	public class LevelManager : Manager<LevelManager>
	{
		#region Manager implementation
		protected override IEnumerator InitCoroutine()
		{
			yield break;
		}
        #endregion

        #region Scene Management
		public SceneLoader m_sceneLoader { get; set; }

		[Header("Scene Management")]
		[SerializeField]
		private int[] levels;

		[SerializeField]
		private int mainMenu;

		[SerializeField]
		private int endScreen;

		[SerializeField]
		private int currentLevel;
		#endregion

		public override void SubscribeEvents()
		{
			base.SubscribeEvents();
		}

		public override void UnsubscribeEvents()
		{
			base.UnsubscribeEvents();
		}

		protected override void GamePlay(GamePlayEvent e)
		{

		}

		protected override void GameMenu(GameMenuEvent e)
		{
			Debug.Log("On est dans le menu !");
		}

        protected override void GameVictory(GameVictoryEvent e)
        {
        }

		public void LoadNextLevel()
        {
			Debug.Log(currentLevel);
			if(currentLevel + 1 >= levels.Length)
            {
				StartCoroutine(m_sceneLoader.LoadLevel(endScreen));
			}
			else
            {
				StartCoroutine(m_sceneLoader.LoadLevel(levels[++currentLevel]));
			}
		}

		public bool haveNextLevel()
        {
			return currentLevel + 1 < levels.Length;
		}

		public void LoadMainMenu()
        {
			StartCoroutine(m_sceneLoader.LoadLevel(mainMenu));
			currentLevel = -1;
		}
	}
}