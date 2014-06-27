using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleAI.Navigation;

public class ExplosionManger : MonoBehaviour
{

	//#error CompareBaseObjectsInternal can only be called from the main thread.   
	private ExplosionPool m_ExplosionPoolInstance;

	//
//	private  LinkedList< GameObject >[] m_ExplosionListArray;
//	private void InitExplosionListArray(){
//		for (int i = 0; i < (int)PathGrid.eNeighborDirection.kNumNeighbors; i++) {
//			m_ExplosionListArray[i] = new LinkedList<GameObject>();
//		}
//	}
//	private LinkedList<GameObject> ExplosionListByDirection(PathGrid.eNeighborDirection direction){
//		return m_ExplosionListArray[(int)direction];
//	}

	private  List< GameObject > m_ExplosionList;
	private List<Vector3[]> m_PositionsList;
	//
	private PathGrid m_PathGrid;

	int m_ExplosionIndex = 69;
	public int m_length = 3;
	public string m_ExplosionPrefabPath = "Prefab/Effect/Explosion";

	void Awake(){

		m_ExplosionPoolInstance = ExplosionPool.Instance;
		m_ExplosionPoolInstance.Init(m_ExplosionPrefabPath);

//		m_ExplosionListArray = new LinkedList<GameObject>[(int)PathGrid.eNeighborDirection.kNumNeighbors];
	//	InitExplosionListArray();

		m_ExplosionList = new List<GameObject>();
		m_PositionsList = new List<Vector3[]>();


	}

	// Use this for initialization
	void Start ()
	{
		m_PathGrid = GameObject.Find("PathGridWithObstacles").GetComponent<PathGridComponent>().PathGrid;
		if(m_PathGrid == null)
			UnityEngine.Debug.LogError("m_PathGrid == null");

		Explose();
	}

	void Explose(){
#if flase
		//Get Position
		m_PositionsList.Clear();
		for (int i = 0; i < (int)PathGrid.eNeighborDirection.kNumNeighbors; i++) {
			Vector3[] positions = CommonHelper.PositionsByDirection(m_ExplosionIndex, m_length, (PathGrid.eNeighborDirection) i, m_PathGrid);
			m_PositionsList.Add(positions);
		}

		//Take Object From Pool
		m_ExplosionList.Clear();
		GameObject gameObject = m_ExplosionPoolInstance.DequeueFromPool();
		m_ExplosionList.Add(gameObject);
		for (int i = 0; 
		     i < m_length * (int)PathGrid.eNeighborDirection.kNumNeighbors; 
		     i++) {
			gameObject = m_ExplosionPoolInstance.DequeueFromPool();
			m_ExplosionList.Add(gameObject);
		}

		//Set Position
		int index = 0;
		m_ExplosionList[index].transform.position = m_PathGrid.GetPathNodePos(m_ExplosionIndex);
		for (int i = 0; i < (int)PathGrid.eNeighborDirection.kNumNeighbors; i++) {
			Vector3[] positions = m_PositionsList[(int)i];
			for (int j = 0; j < m_length; j++) {
				index = (j * (int)PathGrid.eNeighborDirection.kNumNeighbors) + i;
				m_ExplosionList[index + 1].transform.position = positions[j];
			}
		}
#else
		m_ExplosionIndex = m_PathGrid.GetPathNodeIndex( this.gameObject.transform.position );
		
		m_ExplosionList.Clear();

		//add the first one
		GameObject gameObject = m_ExplosionPoolInstance.DequeueFromPool();
		gameObject.transform.position = m_PathGrid.GetPathNodePos(m_ExplosionIndex);
		gameObject.GetComponent<Animator>().SetTrigger("Explosion");
		m_ExplosionList.Add(gameObject);

		//add others
		for (int i = 0; i < (int)PathGrid.eNeighborDirection.kNumNeighbors; i++) {
			Vector3[] positions = CommonHelper.PositionsByDirection(m_ExplosionIndex, m_length, (PathGrid.eNeighborDirection) i, m_PathGrid);
			for (int j = 0; j < m_length; j++) {
				gameObject = m_ExplosionPoolInstance.DequeueFromPool();
				gameObject.transform.position = positions[j];
				gameObject.GetComponent<Animator>().SetTrigger("Explosion");
				m_ExplosionList.Add(gameObject);
			}
		}
#endif
	}

	//Return Object To Pool
	void ReturnExplosion(){
		foreach (GameObject item in m_ExplosionList) {
			m_ExplosionPoolInstance.EnqueueToPool(item);
		}
		m_ExplosionList.Clear();
	}




	// Update is called once per frame
	void Update ()
	{
		
	}
}

