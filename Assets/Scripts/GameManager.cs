namespace GoblinRush
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections.Generic;
    using SDD.Events;
    using System.Linq;
    using System;

    public enum GameState { gameMenu, gamePlay, gameNextLevel, gamePause, gameOver, gameVictory }

    public class GameManager : Manager<GameManager>
    {
        #region Economy
        [Header("Economy")]
        [SerializeField] private int startMoney;

        public int currentMoney { get; set; }
        #endregion

        #region House Management
        public House m_house { get; set; }
        #endregion

        #region Turret managment
        [Header("Turret prebab")]
        [Tooltip("Crossbow Turret prefab")]
        [SerializeField] private GameObject m_CrossbowTurretPrefab;

        [Tooltip("Turret placement")]
        [SerializeField] private GameObject m_TurretPlacementPrefab;

        /// <summary>
        /// Placement turret on click
        /// Visiblity range on right click
        /// </summary>
        private void updateTurretBehaviour()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                //create ray
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //get all hit by ray
                RaycastHit[] hits = Physics.RaycastAll(ray);

                //on left click
                if (Input.GetMouseButtonDown(0))
                {
                    if (hits.Where(hit => hit.transform.name == "Placement_Zone").Count() > 0 && m_CrossbowTurretPrefab.GetComponent<Turret>().getTurretMoneyCost() <= currentMoney)
                    {
                        //get first placement zone
                        RaycastHit hitPlacementZone = hits.FirstOrDefault(hit => hit.transform.name == "Placement_Zone");

                        //récupère l'objet Turret_PLacement         
                        GameObject m_TurretPlacement = hitPlacementZone.transform.parent.gameObject;
                        //Create newTurret
                        GameObject m_newTurret = Instantiate(m_CrossbowTurretPrefab, m_TurretPlacement.transform.position, Quaternion.identity);
                        //Destroy Turret placement zone
                        Destroy(hitPlacementZone.transform.gameObject);
                        //Remove money from Turret
                        currentMoney -= m_newTurret.GetComponent<Turret>().getTurretMoneyCost();
                    }
                    else if (hits.Where(hit => hit.collider.name.Contains("Hitbox") && hit.transform.name.Contains("Turret")).Count() > 0)
                    {
                        //get first turret hit 
                        RaycastHit hitTurret = hits.FirstOrDefault(hit => hit.collider.name.Contains("Hitbox") && hit.transform.name.Contains("Turret"));

                        //get game object
                        GameObject m_Turret = hitTurret.transform.gameObject;
                        //cast game object
                        Turret turret = (Turret)m_Turret.GetComponent(typeof(Turret));
                        //show visibility range
                        turret.m_TurretHUD.ChangeHUDVisibility(true);
                    }
                    else if (hits.Where(hit => hit.collider.name == "RedCross" ).Count() > 0)
                    {
                        RaycastHit redCorss = hits.FirstOrDefault(hit => hit.collider.name == "RedCross");
                        GameObject m_Turret = redCorss.transform.gameObject;
                        Instantiate(m_TurretPlacementPrefab, m_Turret.transform.position, Quaternion.identity);
                        Destroy(m_Turret);
                    }
                    else if (hits.Where(hit => hit.collider.name == "UpgradeArrow").Count() > 0)
                    {
                        RaycastHit upgradeArrow = hits.FirstOrDefault(hit => hit.collider.name == "UpgradeArrow");
                        GameObject m_Turret = upgradeArrow.transform.gameObject;
                        Turret turret = (Turret)m_Turret.GetComponent(typeof(Turret));
                        turret.m_TurretHUD.ChangeHUDVisibility(turret.HUDVisibilityOnUpgrade);
                        turret.NextTurretLevel();
                    }

                }

                //on right click
                if (Input.GetMouseButtonDown(1))
                {
                    //get first turret hit 
                    RaycastHit hitTurret = hits.FirstOrDefault(hit => hit.collider.name.Contains("Hitbox") && hit.transform.name.Contains("Turret"));

                    if (hitTurret.transform != null)
                    {
                        //get game object
                        GameObject m_Turret = hitTurret.transform.gameObject;
                        //cast game object
                        Turret turret = (Turret)m_Turret.GetComponent(typeof(Turret));
                        //show visibility range
                        turret.ChangeVisibilityRange(true);
                    }
                }
            }
        }

        #endregion

        #region Game State
        [Header("Game State Management")]
        [SerializeField]
        private GameState m_GameState;
        public bool IsPlaying { get { return m_GameState == GameState.gamePlay; } }

        private void HandleByGameState()
        {
            switch (m_GameState)
            {
                case GameState.gameMenu:
                    Menu();
                    break;
                case GameState.gamePlay:
                    Play();
                    break;
                case GameState.gameNextLevel:
                    break;
                case GameState.gamePause:
                    Pause();
                    break;
                case GameState.gameOver:
                    Over();
                    break;
                case GameState.gameVictory:
                    Win();
                    break;
                default:
                    break;
            }
        }
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
            EventManager.Instance.AddListener<NextLevelButtonClickedEvent>(NextLevelButtonClicked);
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
            EventManager.Instance.RemoveListener<NextLevelButtonClickedEvent>(NextLevelButtonClicked);
        }
        #endregion

        #region Manager implementation
        protected override IEnumerator InitCoroutine()
        {
            HandleByGameState();
            yield break;
        }
        #endregion

        #region Game flow & Gameplay
        //Game initialization
        void InitNewGame()
        {
            currentMoney = startMoney;
        }
        #endregion

        #region Callbacks to Events issued by MenuManager
        private void MainMenuButtonClicked(MainMenuButtonClickedEvent e)
        {
            Menu();
        }
        private void PlayButtonClicked(PlayButtonClickedEvent e)
        {
            StartCoroutine(playAfterTransition(1.0f));
            LevelManager.Instance.LoadNextLevel();
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

        private void NextLevelButtonClicked(NextLevelButtonClickedEvent e)
        {
            if (m_GameState == GameState.gameNextLevel) LoadNextLevel();
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

        private void Win()
        {
            m_GameState = GameState.gameVictory;
            EventManager.Instance.Raise(new GameVictoryEvent());
        }

        private void GameNextLevel()
        {
            m_GameState = GameState.gameNextLevel;
            EventManager.Instance.Raise(new GameNextLevelEvent());
        }

        private void LoadNextLevel()
        {
            if (LevelManager.Instance.haveNextLevel())
            {
                StartCoroutine(playAfterTransition(1.5f));
            }
            else
            {
                StartCoroutine(overAfterTransition(1.5f));
            }
            LevelManager.Instance.LoadNextLevel();
        }

        private IEnumerator playAfterTransition(float duration)
        {
            yield return new WaitForSeconds(duration);
            Play();
        }

        private IEnumerator overAfterTransition(float duration)
        {
            yield return new WaitForSeconds(duration);
            Over();
        }
        #endregion

        private void Update()
        {
            updateTurretBehaviour();
            if (m_house && m_house.finished() && m_GameState != GameState.gameNextLevel) GameNextLevel();
        }
    }
}

