using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeTurn
{
	private Player player;
	private Vector2Int coordinate;

	public TicTacToeTurn(Player player, Vector2Int coord)
	{
		this.player = player;
		this.coordinate = coord;
	}

	public Player GetPlayer()
	{
		return player;
	}

	public Vector2Int GetCoordinate()
	{
		return coordinate;
	}
}
