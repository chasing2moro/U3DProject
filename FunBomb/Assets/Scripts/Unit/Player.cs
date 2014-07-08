using UnityEngine;
using System.Collections;

public class Player : GameUnitObject
{
	public void MoveTo(Vector2 ScreenPos){
		Vector3 interactPos = CameraHelper.ScreenPosToBackgroundPos (ScreenPos);
		int index = m_pathGrid.GetCellIndex (interactPos);
		if(index == SimpleAI.Planning.Node.kInvalidIndex)
			return;
		int[] indexArray = new int[]{ index };
		m_navigationAgent.MoveToIndex(indexArray);
	}

}

