using UnityEngine;

public class GamePanelUI : AbstractPanelUI
{
	private TicTacToeTileButton[] tileButtons;

	protected override void InitializePanel()
	{
		Debug.Log("GamePanelUI: Getting TileButtons in children");
		tileButtons = GetComponentsInChildren<TicTacToeTileButton>();
		base.InitializePanel();
	}

	protected override void OnUIChange(GameState gameState)
	{
		base.OnUIChange(gameState);
		if (gameState == GameState.GAME)
		{
			Debug.Log("GamePanelUI: Activating Game Panel, clearing tile buttons");
			foreach (TicTacToeTileButton tile in tileButtons)
			{
				tile.ClearTile();
			}
		}
	}
}