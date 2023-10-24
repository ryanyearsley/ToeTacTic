using System;
using System.Collections;
using TMPro;
using UnityEngine;

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

	public event Action<TicTacToeTurn> ticTacToeUpdateEvent;

	public event Action<Player> endPlayerTurnEvent;

	private GameState currentGameState = GameState.MENU;
	private string[,] currentBoard;
	private Player currentPlayerTurn;

	[SerializeField]
	private TMP_Text messageDisplayText;

	private void Awake()
	{
		SingletonInitialization();
		messageDisplayText.text = "";
	}

	public GameState GetGameState()
	{
		return currentGameState;
	}

	public Player GetCurrentPlayerTurn()
	{
		return currentPlayerTurn;
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
		UpdatePlayerTurnDisplay(currentPlayerTurn);
	}

	public void UpdateBoard(Player player, Vector2Int tileCoordinate)
	{
		TicTacToeTurn turn = new TicTacToeTurn(player, tileCoordinate);
		ticTacToeUpdateEvent?.Invoke(turn);
		string updateValue = "";
		if (player != Player.EMPTY)
		{
			updateValue = player.ToString();
		}
		currentBoard[tileCoordinate.x, tileCoordinate.y] = updateValue;
	}

	public void EndPlayerTurn(Player player)
	{
		endPlayerTurnEvent?.Invoke(player);
		currentPlayerTurn = player == Player.X ? Player.O : Player.X;
		UpdatePlayerTurnDisplay(currentPlayerTurn);
	}

	private void UpdatePlayerTurnDisplay (Player player)
	{
		Color playerColor = player == Player.X ? Color.blue : Color.red;
		string playerColorName = player == Player.X ? "Blue" : "Red";
		messageDisplayText.color = playerColor;
		messageDisplayText.text = playerColorName + "'s Turn";
	}

	public void CheckForWinner()
	{
		String winner = TicTacToeUtility.CheckForWinner(currentBoard);
		if (winner != null)
		{
			StartCoroutine(GameOverRoutine(winner));
		}
	}

	public IEnumerator GameOverRoutine(string gameOverDisplayText)
	{
		messageDisplayText.color = Color.gray;
		if (gameOverDisplayText != "DRAW")
		{
			string playerColorName = gameOverDisplayText == "X" ? "Blue" : "Red";
			Color playerColor = gameOverDisplayText == "X" ? Color.blue : Color.red;
			messageDisplayText.text = "Winner: " + playerColorName;
			messageDisplayText.color = playerColor;
		}
		else
		{
			messageDisplayText.text = "DRAW";
			messageDisplayText.color = Color.white;
		}
		ChangeGameState(GameState.POST_GAME);
		yield return new WaitForSeconds(5);
		messageDisplayText.text = "";
		ChangeGameState(GameState.MENU);
	}
}