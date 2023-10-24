using UnityEngine;
using UnityEngine.UI;

public class TicTacToeTileButton : AbstractButtonClick
{

	[SerializeField]
	private Vector2Int tileCoordinate;

	private Image tileImage;

	[SerializeField]
	private Sprite emptySprite;
	[SerializeField]
	private Sprite xSprite;
	[SerializeField]
	private Sprite ySprite;

	private bool isVacant = false;
	protected override void Awake()
	{
		base.Awake();
		tileImage = GetComponent<Image>();
	}
	public void ClearTile()
	{
		tileImage.sprite = emptySprite;
		isVacant = true;
	}
	public override void OnClick()
	{
		if (isVacant == true && TicTacToeGameManager.instance != null  && TicTacToeGameManager.instance.GetGameState() == GameState.GAME)
		{
			Player currentPlayer = TicTacToeGameManager.instance.GetCurrentPlayerTurn();
			TicTacToeGameManager.instance.UpdateBoard(currentPlayer, tileCoordinate);
			tileImage.sprite = currentPlayer == Player.X ? xSprite : ySprite;
			isVacant = false;
			TicTacToeGameManager.instance.EndPlayerTurn(currentPlayer);
			TicTacToeGameManager.instance.CheckForWinner();
		}
	}
}
