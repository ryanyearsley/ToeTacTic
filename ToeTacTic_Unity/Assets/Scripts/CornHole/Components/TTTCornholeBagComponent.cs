using UnityEngine;

public class TTTCornholeBagComponent : TicTacToeSubscriberComponent
{
	private Player player;

	public Player GetPlayer()
	{
		return player;
	}

	[SerializeField]
	private Material playerXMaterial;

	[SerializeField]
	private Material playerOMaterial;

	protected override void OnChangeGameState(GameState gameState)
	{
		if (gameState == GameState.MENU)
			Destroy(this.gameObject);
	}

	public void SetPlayer(Player player)
	{
		this.player = player;
		Debug.Log("BagComponent: Setting player to " + player.ToString());
		MeshRenderer my_mr = gameObject.GetComponent<MeshRenderer>();
		if (player == Player.X)
			my_mr.material = playerXMaterial;
		else if (player == Player.O)
			my_mr.material = playerOMaterial;
	}

	public void Update()
	{
		if (transform.position.y < -10)
		{
			Destroy(this.gameObject);
		}
	}
}