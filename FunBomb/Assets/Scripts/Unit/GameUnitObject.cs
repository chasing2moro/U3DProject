using UnityEngine;
using System.Collections;
using SimpleAI.Navigation;

public class GameUnitObject : MonoBehaviour
{
	public float m_Speed;
	private PathGrid										m_pathGrid;
	private NavigationAgentComponent_Batch m_navigationAgent;

	void OnGUI() {
		if (GUI.Button(new Rect(10, 10, 150, 100), "Lay Bomb")){
			Vector3 LayBombPos;
			if( m_pathGrid.GetPathNodePosByPos(this.gameObject.transform.position, out LayBombPos) ){
				(Instantiate( Resources.Load<GameObject>("Prefab/Props/Bomber") ) as GameObject).transform.position = LayBombPos;
			}


		}
	}

	// Use this for initialization
	void Start () 
	{
		m_navigationAgent = GetComponent<NavigationAgentComponent_Batch>();
		if ( m_navigationAgent.PathTerrain == null || !(m_navigationAgent.PathTerrain is PathGrid) )
		{
			Debug.LogError("Interaction_Touch was built to work with a PathGrid terrain; can't use it on other terrain types.");
		}
		else
		{
			m_pathGrid = m_navigationAgent.PathTerrain as PathGrid;
		}
	}
}

