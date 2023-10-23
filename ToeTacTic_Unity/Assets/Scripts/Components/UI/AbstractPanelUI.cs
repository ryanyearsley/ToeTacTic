using System.Collections.Generic;
using UnityEngine;

public class AbstractPanelUI : MonoBehaviour
{
	protected GameObject panelObject;

	[SerializeField]
	protected List<GameState> panelActiveStates;

	public virtual void Start()
	{
		InitializePanel();
	}

	protected virtual void InitializePanel()
	{
		panelObject = transform.GetChild(0).gameObject;
		panelObject.SetActive(true);
		if (TicTacToeGameManager.instance != null)
		{
			TicTacToeGameManager.instance.changeGameStateEvent += OnUIChange;
		}
		OnUIChange(GameState.MENU);
	}

	protected virtual void OnUIChange(GameState gameState)
	{
		panelObject.SetActive(panelActiveStates.Contains(gameState) ? true : false);
	}
}