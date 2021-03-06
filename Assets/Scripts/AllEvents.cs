using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

#region GameManager Events
public class GameMenuEvent : SDD.Events.Event
{
}
public class GamePlayEvent : SDD.Events.Event
{
}
public class GamePauseEvent : SDD.Events.Event
{
}
public class GameResumeEvent : SDD.Events.Event
{
}
public class GameOverEvent : SDD.Events.Event
{
}
public class GameVictoryEvent : SDD.Events.Event
{
}
public class GameNextLevelEvent : SDD.Events.Event
{
}

public class MainMenuLoadSave : SDD.Events.Event
{
}


public class GameStatisticsChangedEvent : SDD.Events.Event
{
	public float eBestScore { get; set; }
	public float eScore { get; set; }
	public int eNLives { get; set; }
}
#endregion

#region MenuManager Events
public class EscapeButtonClickedEvent : SDD.Events.Event
{
}
public class PlayButtonClickedEvent : SDD.Events.Event
{
}
public class ResumeButtonClickedEvent : SDD.Events.Event
{
}
public class MainMenuButtonClickedEvent : SDD.Events.Event
{
}

public class QuitButtonClickedEvent : SDD.Events.Event
{ }

public class ParamettreButtonClickedEvent : SDD.Events.Event
{ }

public class NextLevelButtonClickedEvent : SDD.Events.Event
{ }

public class BackToMenuButtonClickedEvent : SDD.Events.Event
{ }

public class SaveButtonClickedEvent : SDD.Events.Event
{ }

public class MainMenuLoadSaveButtonClicked : SDD.Events.Event
{ }

public class LoadSaveEvent : SDD.Events.Event
{
	public SaveData Save { get; set; }

	public LoadSaveEvent(SaveData s) => Save = s;
}
#endregion

#region Score Event
public class ScoreItemEvent : SDD.Events.Event
{
	public float eScore;
}
#endregion
