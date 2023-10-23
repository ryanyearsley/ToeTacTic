using UnityEngine;

public class TicTacToeUtility : MonoBehaviour
{
	public static string CheckForWinner(string[,] board)
	{
		// Check rows
		for (int row = 0; row < 3; row++)
		{
			if (board[row, 0] == board[row, 1] && board[row, 1] == board[row, 2] && !string.IsNullOrEmpty(board[row, 0]))
			{
				return board[row, 0];
			}
		}

		// Check columns
		for (int col = 0; col < 3; col++)
		{
			if (board[0, col] == board[1, col] && board[1, col] == board[2, col] && !string.IsNullOrEmpty(board[0, col]))
			{
				return board[0, col];
			}
		}

		// Check diagonals
		if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && !string.IsNullOrEmpty(board[0, 0]))
		{
			return board[0, 0];
		}
		if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && !string.IsNullOrEmpty(board[0, 2]))
		{
			return board[0, 2];
		}
		bool isDraw = true;
		for (int x = 0; x < board.GetLength(0); x++)
		{
			for (int y = 0; y < board.GetLength(1); y++)
			{
				if (string.IsNullOrEmpty(board[x,y]))
				{
					isDraw = false;
				}
			}
		}
		if (isDraw)
			return "DRAW";
		return null; // No winner yet
	}
}