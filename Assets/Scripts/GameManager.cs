namespace GoblinRush
{
    using System.Collections;
    using UnityEngine;
    using System;
    using UnityEngine.UI;
    using System.Collections.Generic;
    using SDD.Events;
    using System.Linq;

    public enum GameState { gameMenu, gamePlay, gameNextLevel, gamePause, gameOver, gameVictory }

    public class GameManager : Manager<GameManager>
    {
        #region Economy
        [Header("Economy")]
        [SerializeField] private int startMoney;

        public int currentMoney { get; set; } = -1;
        #endregion

        #region House Management
        public House m_House { get; set; }
        #endregion

        #region Turret managment
        [Tooltip("Turret placement manager")]
        [SerializeField] private GameObject m_TurretPlacementManager;

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
                    if (hits.Where(hit => hit.transform.name == "PlacementZone").Count() > 0)
                    {

                        //get first placement zone
                        RaycastHit hitPlacementZone = hits.FirstOrDefault(hit => hit.transform.name == "PlacementZone");

                        //récupère l'objet Turret_PLacement         
                        GameObject m_TurretPlacementManagment = hitPlacementZone.transform.parent.gameObject;
                        //get turret placement hud
                        TurretPlacementHUD turretPlacementHUD = (TurretPlacementHUD)m_TurretPlacementManagment.GetComponentInChildren(typeof(TurretPlacementHUD));
                        //show HUD
                        turretPlacementHUD.ChangeHUDVisibility(true);
                        /*
                        //Create newTurret
                        GameObject m_newTurret = Instantiate(m_CrossbowTurretPrefab, m_TurretPlacement.transform.position, Quaternion.identity);
                        //Destroy Turret placement zone
                        Destroy(hitPlacementZone.transform.gameObject);
                        //Remove money from Turret
                        currentMoney -= m_newTurret.GetComponent<Turret>().getTurretMoneyCost();
                        */

                        //son de construction
                        FindObjectOfType<AudioManager>().Play("Construction");
                    }
                    else if (hits.Where(hit => hit.collider.name == "Arrow" || hit.collider.name == "Explosion").Count() > 0)
                    {
                        RaycastHit explosionChoice = hits.FirstOrDefault(hit => hit.transform.name == "Arrow" || hit.transform.name == "Explosion");
                        GameObject hitObject = explosionChoice.transform.gameObject;
                        SpriteRenderer hitSprite = (SpriteRenderer)hitObject.GetComponent(typeof(SpriteRenderer));
                        //récupère l'objet Turret_PLacement         
                        TurretPlacementHUD m_TurretPlacementHUD = (TurretPlacementHUD)hitObject.GetComponentInParent(typeof(TurretPlacementHUD));
                        //récupère la tourelle correspondante
                        GameObject m_TurretPrefab = m_TurretPlacementHUD.getCorrespTurret(hitSprite);
                        if (!m_TurretPrefab) return;
                        //si pas assez d'argent
                        if (m_TurretPrefab.GetComponent<Turret>().getTurretMoneyCost() > currentMoney) return;
                        //Create newTurret
                        GameObject m_newTurret = Instantiate(m_TurretPrefab, m_TurretPlacementHUD.transform.position, Quaternion.identity);
                        //Destroy Turret placement manager
                        Destroy(m_TurretPlacementHUD.transform.parent.gameObject);
                        //Remove money from Turret
                        currentMoney -= m_newTurret.GetComponent<Turret>().getTurretMoneyCost();

                        TurretScore += 1;
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
                    else if (hits.Where(hit => hit.collider.name == "RedCross").Count() > 0)
                    {
                        RaycastHit redCorss = hits.FirstOrDefault(hit => hit.collider.name == "RedCross");
                        GameObject m_Turret = redCorss.transform.gameObject;
                        Turret turret = (Turret)m_Turret.GetComponent(typeof(Turret));
                        currentMoney += (int)(turret.getTurretMoneyCost() / 1.5);
                        Instantiate(m_TurretPlacementManager, m_Turret.transform.position, Quaternion.identity);
                        Destroy(m_Turret);
                    }
                    else if (hits.Where(hit => hit.collider.name == "UpgradeArrow").Count() > 0)
                    {
                        RaycastHit upgradeArrow = hits.FirstOrDefault(hit => hit.collider.name == "UpgradeArrow");
                        GameObject m_Turret = upgradeArrow.transform.gameObject;
                        Turret turret = (Turret)m_Turret.GetComponent(typeof(Turret));
                        turret.m_TurretHUD.ChangeHUDVisibility(turret.HUDVisibilityOnUpgrade);
                        turret.NextTurretLevel();
                        TurretScore += 1;
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

        #region Score Management
        public float ElapsedTime { get; set; }
        public int EnemyKilled { get; set; }
        public int TurretScore { get; set; }
        public int Score
        {
            // Le score correspon au score des ennemis tués - Le nombre de tourelles utilisées - le temps pris
            get { return EnemyKilled - TurretScore - (int)(ElapsedTime * 0.1); }
            private set { }
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
            EventManager.Instance.AddListener<SaveButtonClickedEvent>(SaveButtonClicked);
            EventManager.Instance.AddListener<MainMenuLoadSaveButtonClicked>(MainMenuLoadSave);
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
            EventManager.Instance.RemoveListener<SaveButtonClickedEvent>(SaveButtonClicked);
            EventManager.Instance.RemoveListener<MainMenuLoadSaveButtonClicked>(MainMenuLoadSave);
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
            if (currentMoney == -1) currentMoney = startMoney;
            EnemyKilled = 0;
            TurretScore = 0;
            ElapsedTime = 0;
        }
        #endregion

        #region Callbacks to Events issued by MenuManager
        private void MainMenuButtonClicked(MainMenuButtonClickedEvent e)
        {
            LevelManager.Instance.LoadMainMenu(() => Menu());
        }
        private void PlayButtonClicked(PlayButtonClickedEvent e)
        {
            LevelManager.Instance.LoadNextLevel(() => Play());
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

        private void SaveButtonClicked(SaveButtonClickedEvent e)
        {
            DateTime dt = DateTime.Now;

            SaveManager.Instance.SaveLevel("Save_" + dt.ToString("yyyy_dd_MM_HH_mm_ss"));
        }

        private void MainMenuLoadSave(MainMenuLoadSaveButtonClicked e)
        {
            EventManager.Instance.Raise(new MainMenuLoadSave());
        }
        #endregion

        #region GameState methods
        private void Menu()
        {
            m_GameState = GameState.gameMenu;
            EventManager.Instance.Raise(new GameMenuEvent());
        }

        public void Play()
        {
            InitNewGame();
            m_GameState = GameState.gamePlay;

            EventManager.Instance.Raise(new GamePlayEvent());

            //son du thème
            FindObjectOfType<AudioManager>().Play("Theme");
        }

        private void Pause()
        {
            if (!IsPlaying) return;

            m_GameState = GameState.gamePause;
            EventManager.Instance.Raise(new GamePauseEvent());

            //pause du thème
            FindObjectOfType<AudioManager>().Pause("Theme");
        }

        private void Resume()
        {
            if (IsPlaying) return;

            m_GameState = GameState.gamePlay;
            EventManager.Instance.Raise(new GameResumeEvent());

            //son du thème
            FindObjectOfType<AudioManager>().Play("Theme");
        }

        private void Over()
        {
            //son de victoire
            FindObjectOfType<AudioManager>().Play("Lose");
            FindObjectOfType<AudioManager>().Stop("Theme");

            m_GameState = GameState.gameOver;
            EventManager.Instance.Raise(new GameOverEvent());
        }

        private void Win()
        {
            //son de victoire
            FindObjectOfType<AudioManager>().Play("Victory");
            FindObjectOfType<AudioManager>().Stop("Theme");

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
            LevelManager.Instance.LoadNextLevel(() => Play());
        }

        #endregion

        private void Update()
        {
            if (!IsPlaying) return;
            updateTurretBehaviour();

            ElapsedTime += Time.deltaTime;

            //On vérifie si on a gagne
            if (m_House && m_House.finished())
            {
                if (LevelManager.Instance.HaveNextLevel())
                {
                    GameNextLevel();
                }
                else
                {
                    int bestScore = PlayerPrefs.GetInt("BestScore", 0);
                    if (bestScore < Score) PlayerPrefs.SetInt("BestScore", Score);
                    Win();
                }
            }

            //On verifie si on a perdu
            if (!CastleTarget)
            {
                Over();
            }

        }
    }
}

