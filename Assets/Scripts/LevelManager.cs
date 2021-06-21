

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
		public SceneLoader m_SceneLoader { get; set; }

		[Header("Scene Management")]
		[SerializeField]
		private int[] levels;

		[SerializeField]
		private int mainMenu;

		[SerializeField]
		private int endScreen;

		[SerializeField]
		private int currentLevel;

		public int CurrentLevel { 
			get { return currentLevel; }
			set { currentLevel = value; }
		}
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

		public void LoadNextLevel(Action executeAfterSceneChange = null)
        {
			if(currentLevel + 1 >= levels.Length)
            {
				StartCoroutine(m_SceneLoader.LoadLevel(endScreen, executeAfterSceneChange));
			}
			else
            {
				StartCoroutine(m_SceneLoader.LoadLevel(levels[++currentLevel], executeAfterSceneChange));
			}
		}

		public void LoadLevel(int level, Action executeAfterSceneChange = null)
        {
			currentLevel = level;
			StartCoroutine(m_SceneLoader.LoadLevel(levels[currentLevel], executeAfterSceneChange));
		}

		public bool HaveNextLevel()
        {
			return currentLevel + 1 < levels.Length;
		}

		public void LoadMainMenu(Action executeAfterSceneChange = null)
        {
			StartCoroutine(m_SceneLoader.LoadLevel(mainMenu, executeAfterSceneChange));
			currentLevel = -1;
		}
	}
}