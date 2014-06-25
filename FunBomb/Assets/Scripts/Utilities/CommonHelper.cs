using UnityEngine;
using System.Collections;
using SimpleAI.Navigation;

public class CommonHelper
{
	public static Vector3[] PositionsByDirection(int gridIndex, int length, PathGrid.eNeighborDirection dirction,PathGrid grid ){
	//	UnityEngine.Debug.Log("Origin PosIndex:" + gridIndex);
		Vector3[] Positions = new Vector3[length];
		for (int i = 0; i < length; i++) {
			gridIndex = grid.GetNeighbor(gridIndex, dirction);
			Positions[i] = grid.GetPathNodePos(gridIndex);
		//	UnityEngine.Debug.Log("Search PosIndex:" + gridIndex);
		}
		return Positions;
	}
}

