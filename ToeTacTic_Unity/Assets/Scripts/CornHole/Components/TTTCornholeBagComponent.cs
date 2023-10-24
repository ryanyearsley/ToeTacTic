using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTTCornholeBagComponent : MonoBehaviour
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
