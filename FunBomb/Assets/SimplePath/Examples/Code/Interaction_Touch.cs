using UnityEngine;
using System.Collections;
using SimpleAI.Navigation;
using System.Collections.Generic;

[RequireComponent(typeof(NavigationAgentComponent))]
public class Interaction_Touch : MonoBehaviour
{
	private NavigationAgentComponent 		m_navigationAgent;
	private PathGrid						m_pathGrid;
	private LinkedList<int> swipeIndexLinkedList = new LinkedList<int> ();
	private List<int> swipeIndexList;

	public bool m_debugShowPath = true;
	void Awake()
	{
		m_navigationAgent = GetComponent<NavigationAgentComponent>();
	}

	// Use this for initialization
	void Start () 
	{
		if ( m_navigationAgent.PathTerrain == null || !(m_navigationAgent.PathTerrain is PathGrid) )
		{
			Debug.LogError("Interaction_Wander was built to work with a PathGrid terrain; can't use it on other terrain types.");
		}
		else
		{
			m_pathGrid = m_navigationAgent.PathTerrain as PathGrid;
		}
		if(m_debugShowPath)
			swipeIndexList = new List<int> ();
	}
	
	void OnTap(TapGesture gesture) {
		Vector3 interactPos = CameraHelper.ScreenPosToBackgroundPos (gesture.StartPosition);
		interactPos = m_pathGrid.GetNearestCellCenter (interactPos);
		m_navigationAgent.MoveToPosition (interactPos, 1);
	}

	void OnSwipe(SwipeGesture gesture){
		swipeIndexLinkedList.Clear ();
		if (m_debugShowPath)
			swipeIndexList.Clear ();
		foreach (Vector2 item in gesture.VectorsInProgress) {
			Vector3 bgPos = CameraHelper.ScreenPosToBackgroundPos (item);
			int index =	m_pathGrid.GetCellIndex(bgPos);
			if(m_pathGrid.IsBlocked(index))
				continue;
			if(!swipeIndexLinkedList.Contains(index))
				swipeIndexLinkedList.AddLast(index);
			if (m_debugShowPath){
				if(!swipeIndexList.Contains(index))
					swipeIndexList.Add(index);
			}
		}

		m_navigationAgent.MoveToIndex (swipeIndexLinkedList.First.Value);
	}

	private void OnNavigationRequestSucceeded()
	{
		if (swipeIndexLinkedList.Count > 0) {
			m_navigationAgent.MoveToIndex (swipeIndexLinkedList.First.Value);
			swipeIndexLinkedList.RemoveFirst ();
		}
	}
	
	void OnDrawGizmos()
	{
		if (m_debugShowPath) {
			if (swipeIndexList != null && swipeIndexList.Count > 0) {
				Gizmos.color = Color.red;
				for (int i = 1; i < swipeIndexList.Count; i++) {
					Vector3 start = m_pathGrid.GetCellCenter (swipeIndexList [i - 1]);
					Vector3 end = m_pathGrid.GetCellCenter (swipeIndexList [i]);
					Gizmos.DrawLine (start, end);
					
					Gizmos.DrawCube (start, new Vector3 (0.2f, 0.2f, 0.2f));
				}
			}
		}
	}
	
}

