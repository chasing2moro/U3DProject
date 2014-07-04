using UnityEngine;
using System.Collections;
using SimpleAI.Navigation;

//[ExecuteInEditMode]
public class ObstacleEventReceiver : MonoBehaviour
{
	void OnTap(TapGesture gesture) { 
		Debug.Log("tap pos:" + gesture.Position);
	}
	#region ExecuteInEditMode
	private PathGridComponent m_PathGridComponent;
	public bool m_RepaintColliderBox = true;

	void Start(){
//		m_PathGridComponent = this.transform.parent.gameObject.GetComponent<PathGridComponent>();
//		if(m_PathGridComponent == null)
//			Debug.LogError("m_PathGridComponent == null");
		RepaintCollderBox();
	}

	void Update(){
		if(m_RepaintColliderBox){
			RepaintCollderBox();
			m_RepaintColliderBox = false;
			Debug.Log("Repaint ColliderBox Finish");
		}
	}

	private void RepaintCollderBox(){
		m_PathGridComponent = this.transform.parent.gameObject.GetComponent<PathGridComponent>();
		if(m_PathGridComponent == null)
			Debug.LogError("m_PathGridComponent == null");

	  


	//	Debug.Log(this.collider.bounds.min + " " + this.collider.bounds.max);
		Debug.Log(	new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -0.5f) + " " +  new Vector3(m_PathGridComponent.m_cellSize * m_PathGridComponent.m_numberOfColumns, m_PathGridComponent.m_cellSize * m_PathGridComponent.m_numberOfRows, 0.5f));
		this.collider.bounds.SetMinMax(new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -0.5f),
		                               new Vector3(m_PathGridComponent.m_cellSize * m_PathGridComponent.m_numberOfColumns, m_PathGridComponent.m_cellSize * m_PathGridComponent.m_numberOfRows, 0.5f)
		                               );
		Debug.Log("====");
		//this.collider.bounds.center = gridBounds.center;
		//this.collider.bounds.size = gridBounds.size;
	}
	#endregion
}

