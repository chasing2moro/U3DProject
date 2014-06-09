using UnityEngine;
using System.Collections;
using SimpleAI.Navigation;
using System.Collections.Generic;

[RequireComponent(typeof(NavigationAgentComponent))]
public class Interaction_Touch : MonoBehaviour
{
	private NavigationAgentComponent 		m_navigationAgent;
	private PathGrid										m_pathGrid;


	private Vector2										m_swipeLastRecordVec;
	private Vector2										m_swipeCurrentVec;
	private Vector2										m_swipeNextVec;
	private int													swipeTempColumn;
	private List<int> 										swipeIndexListRaw;
	private List<int> 										swipeIndexList;
	private int													swipeIndexListIndex;
	private SearchPathType 							searchType = SearchPathType.TapToWalk;

	//Debug show in Scene
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
			Debug.LogError("Interaction_Touch was built to work with a PathGrid terrain; can't use it on other terrain types.");
		}
		else
		{
			m_pathGrid = m_navigationAgent.PathTerrain as PathGrid;
		}

			swipeIndexList = new List<int> ();
			swipeIndexListRaw = new List<int>();
			swipeIndexListIndex = 0;
	
	}
	
	void OnTap(TapGesture gesture) {
		searchType = SearchPathType.TapToWalk;

		Vector3 interactPos = CameraHelper.ScreenPosToBackgroundPos (gesture.StartPosition);
		interactPos = m_pathGrid.GetNearestCellCenter (interactPos);
		m_navigationAgent.MoveToPosition (interactPos, 1);
	}

	void OnSwipe(SwipeGesture gesture){
		searchType = SearchPathType.SwipeToWalk;
		swipeIndexList.Clear ();
		swipeIndexListRaw.Clear();

		foreach (Vector2 item in gesture.VectorsInProgress) {
			Vector3 bgPos = CameraHelper.ScreenPosToBackgroundPos (item);
			int index =	m_pathGrid.GetCellIndex(bgPos);
			if(m_pathGrid.IsBlocked(index))
				continue;
			if(!swipeIndexListRaw.Contains(index))
				swipeIndexListRaw.Add(index);
		}


		for (int i = 0; i < swipeIndexListRaw.Count; i++) {
			if(i == 0){
				swipeIndexList.Add(swipeIndexListRaw[i]);
				m_swipeLastRecordVec = new Vector2( m_pathGrid.GetRow(swipeIndexListRaw[i]), m_pathGrid.GetColumn(swipeIndexListRaw[i]) );
				continue;
			}
			if(i == swipeIndexListRaw.Count - 1){
				swipeIndexList.Add(swipeIndexListRaw[i]);
				continue;//break
			}
			if(i == 1){
				m_swipeCurrentVec = new Vector2( m_pathGrid.GetRow(swipeIndexListRaw[i]),  m_pathGrid.GetColumn(swipeIndexListRaw[i]) );
			}else{
				m_swipeCurrentVec = m_swipeNextVec;
			}
			m_swipeNextVec = new Vector2( m_pathGrid.GetRow(swipeIndexListRaw[i + 1]),  m_pathGrid.GetColumn(swipeIndexListRaw[i + 1]) );

			if(Vector2.Dot((m_swipeCurrentVec - m_swipeLastRecordVec).normalized ,  (m_swipeNextVec - m_swipeCurrentVec).normalized) < 0.9f){
				m_swipeLastRecordVec = new Vector2( m_pathGrid.GetRow(swipeIndexListRaw[i]), m_pathGrid.GetColumn(swipeIndexListRaw[i]) );
				swipeIndexList.Add(swipeIndexListRaw[i]);
			}

		}
		
		swipeIndexListIndex = 0;
		m_navigationAgent.MoveToIndex (swipeIndexList[swipeIndexListIndex]);
	}

	private void OnNavigationRequestSucceeded()
	{
		if (swipeIndexList.Count > 0 &&
		    ++swipeIndexListIndex <  swipeIndexList.Count && 
		    searchType == SearchPathType.SwipeToWalk) {
				m_navigationAgent.MoveToIndex (swipeIndexList[swipeIndexListIndex]);
		}
	}
	
	void OnDrawGizmos()
	{
		//Debug show in Scene
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

