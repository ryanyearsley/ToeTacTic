using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class TicTacToeGameManager : MonoBehaviour
{
	#region Singleton

	public static TicTacToeGameManager instance;

	private void SingletonInitialization()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}

	#endregion Singleton



	public event Action<GameState> changeGameStateEvent;

	private GameState currentGameState = GameState.MENU;
	private string[,] currentBoard;
	private Player currentPlayerTurn;

	[SerializeField]
	private TMP_Text winnerDisplayText;

	private void Awake()
	{
		SingletonInitialization();
		winnerDisplayText.text = "";
	}

	public GameState GetGameState()
	{
		return currentGameState;
	}

	public void ChangeGameState(GameState newGameState)
	{
		if (currentGameState != newGameState)
		{
			currentGameState = newGameState;
			changeGameStateEvent?.Invoke(currentGameState);
		}
	}

	public void StartGame()
	{
		ChangeGameState(GameState.GAME);
		currentBoard = new string[3, 3];
		currentPlayerTurn = Player.X;
		winnerDisplayText.text = currentPlayerTurn.ToString() + "'s Turn";
	}
	public Player OnGameTileClick(Vector2Int tileCoordinate)
	{
		Player output = currentPlayerTurn;
		currentBoard [tileCoordinate.x, tileCoordinate.y] = currentPlayerTurn.ToString();
		currentPlayerTurn = currentPlayerTurn == Player.X ? Player.O : Player.X;
		winnerDisplayText.text = currentPlayerTurn.ToString() + "'s Turn";
		String winner = TicTacToeUtility.CheckForWinner(currentBoard);
		if (winner != null)
		{
			StartCoroutine(GameOverRoutine(winner));
		}
		return output;
	}

	public IEnumerator GameOverRoutine (string gameOverDisplayText)
	{
		winnerDisplayText.text = "Winner: " + gameOverDisplayText;
		ChangeGameState(GameState.POST_GAME);
		yield return new WaitForSeconds(5);
		winnerDisplayText.text = "";
		ChangeGameState(GameState.MENU);
	}
}