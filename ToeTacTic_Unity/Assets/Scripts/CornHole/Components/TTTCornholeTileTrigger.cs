using System.Collections.Generic;
using UnityEngine;

public class TTTCornholeTileTrigger : TicTacToeSubscriberComponent
{
	[SerializeField]
	private Vector2Int tileCoordinate;

	[SerializeField]
	private Material neutralMaterial;

	[SerializeField]
	private Material xMaterial;

	[SerializeField]
	private Material oMaterial;

	private Renderer _renderer;
	private Player currentOccupyingPlayer = Player.EMPTY;
	private List<TTTCornholeBagComponent> xOccupants = new List<TTTCornholeBagComponent>();
	private List<TTTCornholeBagComponent> oOccupants = new List<TTTCornholeBagComponent>();

	private void Awake()
	{
		_renderer = transform.parent.GetComponent<Renderer>();
		_renderer.material = neutralMaterial;
		currentOccupyingPlayer = Player.EMPTY;
	}

	protected override void OnChangeGameState(GameState gameState)
	{
		if (gameState == GameState.MENU)
		{
			_renderer.material = neutralMaterial;
			currentOccupyingPlayer = Player.EMPTY;
		}
		else if (gameState == GameState.POST_GAME)
		{
			xOccupants.Clear();
			oOccupants.Clear();
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		Debug.Log("TileTrigger: TriggerEnter Detected!");
		TTTCornholeBagComponent newOccupant = other.GetComponent<TTTCornholeBagComponent>();
		if (newOccupant != null)
		{
			if (newOccupant.GetPlayer() == Player.X)
				xOccupants.Add(newOccupant);
			else
				oOccupants.Add(newOccupant);
		}
		CalculateWinningOccupant();
	}

	public void OnTriggerExit(Collider other)
	{
		Debug.Log("TileTrigger: TriggerExit Detected!");
		TTTCornholeBagComponent leavingOccupant = other.GetComponent<TTTCornholeBagComponent>();
		if (leavingOccupant != null)
		{
			if (leavingOccupant.GetPlayer() == Player.X)
				xOccupants.Remove(leavingOccupant);
			else
				oOccupants.Remove(leavingOccupant);
		}
		CalculateWinningOccupant();
	}

	public void CalculateWinningOccupant()
	{
		if (xOccupants.Count == oOccupants.Count)
		{
			_renderer.material = neutralMaterial;
			currentOccupyingPlayer = Player.EMPTY;
		}
		else if (xOccupants.Count > oOccupants.Count)
		{
			_renderer.material = xMaterial;
			currentOccupyingPlayer = Player.X;
		}
		else
		{
			_renderer.material = oMaterial;
			currentOccupyingPlayer = Player.O;
		}
		if (TicTacToeGameManager.instance != null)
		{
			TicTacToeGameManager.instance.UpdateBoard(currentOccupyingPlayer, tileCoordinate);
		}
	}
}