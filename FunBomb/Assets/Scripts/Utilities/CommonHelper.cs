using UnityEngine;
using System.Collections;
using SimpleAI.Navigation;

public class CommonHelper : Singleton<CommonHelper>
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
	public static Vector3 HidenPos3D = new Vector3(-100, -100, 0);
	public static Vector2 HidenPos2D = new Vector2(-100, -100);
}

