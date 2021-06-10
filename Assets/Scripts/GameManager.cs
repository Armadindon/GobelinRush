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

        #region Turret managment
        [Header("Turret prebab")]
        [Tooltip("Cannon Turret prefab")]
        [SerializeField] private GameObject m_CannonTurretPrefab;

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
                    if (hits.Where(hit => hit.transform.name == "Placement_Zone").Count() > 0 && m_CannonTurretPrefab.GetComponent<Cannon_Turret>().getMoneyCost() <= currentMoney)
                    {
                        //get first placement zone
                        RaycastHit hitPlacementZone = hits.FirstOrDefault(hit => hit.transform.name == "Placement_Zone");

                        //récupère l'objet Turret_PLacement         
                        GameObject m_TurretPlacement = hitPlacementZone.transform.parent.gameObject;
                        //Create newTurret
                        GameObject m_newTurretTurret = Instantiate(m_CannonTurretPrefab, m_TurretPlacement.transform.position, Quaternion.identity);
                        //Destroy Turret placement zone
                        Destroy(hitPlacementZone.transform.gameObject);
                        //Remove money from Turret
                        currentMoney -= m_newTurretTurret.GetComponent<Cannon_Turret>().getMoneyCost();
                    }
                    else if (hits.Where(hit => hit.collider.name.Contains("Hitbox") && hit.transform.name.Contains("Turret")).Count() > 0)
                    {
                        //get first turret hit 
                        RaycastHit hitTurret = hits.FirstOrDefault(hit => hit.collider.name.Contains("Hitbox") && hit.transform.name.Contains("Turret"));

                        //get game object
                        GameObject m_CannonTurret = hitTurret.transform.gameObject;
                        //cast game object
                        Cannon_Turret cannonTurret = (Cannon_Turret)m_CannonTurret.GetComponent(typeof(Cannon_Turret));
                        //show visibility range
                        cannonTurret.m_TurretHUD.ChangeHUDVisibility(true);
                    }
                    else if (hits.Where(hit => hit.collider.name == "RedCross" ).Count() > 0)
                    {
                        //get first placement zone
                        RaycastHit redCorss = hits.FirstOrDefault(hit => hit.collider.name == "RedCross");
                        GameObject m_CannonTurret = redCorss.transform.gameObject;
                        Instantiate(m_TurretPlacementPrefab, m_CannonTurret.transform.position, Quaternion.identity);
                        Destroy(m_CannonTurret);
                    }
                    else if (hits.Where(hit => hit.collider.name == "UpgradeArrow").Count() > 0)
                    {
                        //get first placement zone
                        RaycastHit upgradeArrow = hits.FirstOrDefault(hit => hit.collider.name == "UpgradeArrow");
                        GameObject m_CannonTurret = upgradeArrow.transform.gameObject;
                        Cannon_Turret cannonTurret = (Cannon_Turret)m_CannonTurret.GetComponent(typeof(Cannon_Turret));
                        cannonTurret.m_TurretHUD.ChangeHUDVisibility(cannonTurret.HUDVisibilityOnUpgrade);
                        cannonTurret.NextTurretLevel();
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
                        GameObject m_CannonTurret = hitTurret.transform.gameObject;
                        //cast game object
                        Cannon_Turret cannonTurret = (Cannon_Turret)m_CannonTurret.GetComponent(typeof(Cannon_Turret));
                        //show visibility range
                        cannonTurret.ChangeVisibilityRange(true);
                    }
                }
            }
        }

        #endregion

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

        private void Update()
        {
            updateTurretBehaviour();
        }
    }
}

