using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeSubscriberComponent : MonoBehaviour
{
	private void Start()
	{
		SubscribeToEvents();
	}
	private void OnDestroy()
	{
		UnsubscribeFromEvents();
	}
	protected virtual void SubscribeToEvents()
	{
		if (TicTacToeGameManager.instance != null)
		{
			Debug.Log("TTTSubComp: Subscribing to events.");
			TicTacToeGameManager.instance.changeGameStateEvent += OnChangeGameState;
			TicTacToeGameManager.instance.ticTacToeUpdateEvent += OnUpdateTicTacToeBoard;
			TicTacToeGameManager.instance.endPlayerTurnEvent += OnEndPlayerTurn;
		}
	}
	protected virtual void UnsubscribeFromEvents()
	{
		if (TicTacToeGameManager.instance != null)
		{
			Debug.Log("TTTSubComp: Subscribing to events.");
			TicTacToeGameManager.instance.changeGameStateEvent -= OnChangeGameState;
			TicTacToeGameManager.instance.ticTacToeUpdateEvent -= OnUpdateTicTacToeBoard;
			TicTacToeGameManager.instance.endPlayerTurnEvent -= OnEndPlayerTurn;
		}
	}
	protected virtual void OnChangeGameState(GameState gameState)
	{

	}
	protected virtual void OnUpdateTicTacToeBoard(TicTacToeTurn turn)
	{

	}
	protected virtual void OnEndPlayerTurn(Player player)
	{

	}

}
