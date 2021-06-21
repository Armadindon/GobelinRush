﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;
using System;

public abstract class SingletonGameStateObserver<T> :  Singleton<T>,IEventHandler where T:Component
{
	public virtual void SubscribeEvents()
	{
		EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
		EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
		EventManager.Instance.AddListener<GamePauseEvent>(GamePause);
		EventManager.Instance.AddListener<GameResumeEvent>(GameResume);
		EventManager.Instance.AddListener<GameOverEvent>(GameOver);
		EventManager.Instance.AddListener<GameVictoryEvent>(GameVictory);
		EventManager.Instance.AddListener<GameStatisticsChangedEvent>(GameStatisticsChanged);
		EventManager.Instance.AddListener<GameNextLevelEvent>(GameNextLevel);
		EventManager.Instance.AddListener<MainMenuLoadSave>(MainMenuLoadSave);
		EventManager.Instance.AddListener<LoadSaveEvent>(LoadSaveEvent);
	}

	public virtual void UnsubscribeEvents()
	{
		EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
		EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
		EventManager.Instance.RemoveListener<GamePauseEvent>(GamePause);
		EventManager.Instance.RemoveListener<GameResumeEvent>(GameResume);
		EventManager.Instance.RemoveListener<GameOverEvent>(GameOver);
		EventManager.Instance.RemoveListener<GameVictoryEvent>(GameVictory);
		EventManager.Instance.RemoveListener<GameStatisticsChangedEvent>(GameStatisticsChanged);
		EventManager.Instance.RemoveListener<GameNextLevelEvent>(GameNextLevel);
		EventManager.Instance.RemoveListener<MainMenuLoadSave>(MainMenuLoadSave);
		EventManager.Instance.RemoveListener<LoadSaveEvent>(LoadSaveEvent);
	}

	protected override void Awake()
	{
		base.Awake();
		SubscribeEvents();
	}

	protected virtual void OnDestroy()
	{
		UnsubscribeEvents();
	}

	protected virtual void GameMenu(GameMenuEvent e)
	{
	}

	protected virtual void GamePlay(GamePlayEvent e)
	{
	}

	protected virtual void GamePause(GamePauseEvent e)
	{
	}

	protected virtual void GameResume(GameResumeEvent e)
	{
	}

	protected virtual void GameOver(GameOverEvent e)
	{
	}

	protected virtual void GameVictory(GameVictoryEvent e)
	{
	}

	protected virtual void GameStatisticsChanged(GameStatisticsChangedEvent e)
	{
	}

	protected virtual void GameNextLevel(GameNextLevelEvent e) 
	{
	}

	protected virtual void MainMenuLoadSave(MainMenuLoadSave e)
	{
	}

	protected virtual void LoadSaveEvent(LoadSaveEvent e)
	{
	}


}
