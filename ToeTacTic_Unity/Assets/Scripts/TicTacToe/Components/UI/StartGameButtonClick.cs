public class StartGameButtonClick : AbstractButtonClick
{
	public override void OnClick()
	{
		if (TicTacToeGameManager.instance != null)
		{
			TicTacToeGameManager.instance.StartGame();
		}
	}
}