namespace GoblinRush
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections.Generic;
    using SDD.Events;
    using System.Linq;

    public enum GameState { gameMenu, gamePlay, gameNextLevel, gamePause, gameOver, gameVictory }

    public class GameManager : Manager<GameManager>
    {
        #region Game State
        private GameState m_GameState;
        public bool IsPlaying { get { return m_GameState == GameState.gamePlay; } }
        #endregion

        #region Castle Management
        //Servira à checker la vie du chateau
        public GameObject CastleTarget { get; set; }
        #endregion

        #region Waypoint Management
        public Transform FirstWaypoint { get; set; }
        #endregion

        #region Events' subscription
        public override void SubscribeEvents()
        {
            base.SubscribeEvents();

            //MainMenuManager
            EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
            EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
            EventManager.Instance.AddListener<ResumeButtonClickedEvent>(ResumeButtonClicked);
            EventManager.Instance.AddListener<EscapeButtonClickedEvent>(EscapeButtonClicked);
            EventManager.Instance.AddListener<QuitButtonClickedEvent>(QuitButtonClicked);
            EventManager.Instance.AddListener<ParamettreButtonClickedEvent>(ParamettreButtonClicked);
        }

        public override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();

            //MainMenuManager
            EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
            EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
            EventManager.Instance.RemoveListener<ResumeButtonClickedEvent>(ResumeButtonClicked);
            EventManager.Instance.RemoveListener<EscapeButtonClickedEvent>(EscapeButtonClicked);
            EventManager.Instance.RemoveListener<QuitButtonClickedEvent>(QuitButtonClicked);
            EventManager.Instance.RemoveListener<ParamettreButtonClickedEvent>(ParamettreButtonClicked);

        }
        #endregion

        #region Manager implementation
        protected override IEnumerator InitCoroutine()
        {
            Menu();
            InitNewGame(); // essentiellement pour que les statistiques du jeu soient mise à jour en HUD
            yield break;
        }
        #endregion

        #region Game flow & Gameplay
        //Game initialization
        void InitNewGame(bool raiseStatsEvent = true)
        {

        }
        #endregion

        #region Callbacks to Events issued by MenuManager
        private void MainMenuButtonClicked(MainMenuButtonClickedEvent e)
        {
            Menu();
        }

        private void PlayButtonClicked(PlayButtonClickedEvent e)
        {
            Play();
        }

        private void ResumeButtonClicked(ResumeButtonClickedEvent e)
        {
            Resume();
        }

        private void EscapeButtonClicked(EscapeButtonClickedEvent e)
        {
            if (IsPlaying) Pause();
        }

        private void QuitButtonClicked(QuitButtonClickedEvent e)
        {
            Application.Quit();
        }

        private void ParamettreButtonClicked(ParamettreButtonClickedEvent e)
        {
            Debug.Log("Paramettre has been clicked\n TODO: Parametre Menu");
        }
        #endregion

        #region GameState methods
        private void Menu()
        {
            m_GameState = GameState.gameMenu;
            if (MusicLoopsManager.Instance) MusicLoopsManager.Instance.PlayMusic(Constants.MENU_MUSIC);
            EventManager.Instance.Raise(new GameMenuEvent());
        }

        private void Play()
        {
            InitNewGame();
            m_GameState = GameState.gamePlay;

            if (MusicLoopsManager.Instance) MusicLoopsManager.Instance.PlayMusic(Constants.GAMEPLAY_MUSIC);
            EventManager.Instance.Raise(new GamePlayEvent());
        }

        private void Pause()
        {
            if (!IsPlaying) return;

            m_GameState = GameState.gamePause;
            EventManager.Instance.Raise(new GamePauseEvent());
        }

        private void Resume()
        {
            if (IsPlaying) return;

            m_GameState = GameState.gamePlay;
            EventManager.Instance.Raise(new GameResumeEvent());
        }

        private void Over()
        {
            m_GameState = GameState.gameOver;
            EventManager.Instance.Raise(new GameOverEvent());
            if (SfxManager.Instance) SfxManager.Instance.PlaySfx2D(Constants.GAMEOVER_SFX);
        }
        #endregion
    }
}

