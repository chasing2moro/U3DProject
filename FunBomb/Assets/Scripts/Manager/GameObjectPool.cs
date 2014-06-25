using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectPool : UnityEngine.Object
{
	
	protected Queue<GameObject>                  m_GameObjectPool;
	protected string											m_GameObjectPath;
	protected GameObject 								m_StaffGameObject;


	//Initialize
	public GameObjectPool(string gameObjectPath){
		Init(gameObjectPath);
	}
	public GameObjectPool(){
	}
//	~ GameObjectPool(){
//		Debug.Log("~GameObjectPool()");
//		Release();
//	}
	void Dispose() {
		Debug.Log("Dispose()");
		Release();
	}

	public virtual void Init(string gameObjectPath){
		m_GameObjectPath = gameObjectPath;
		m_GameObjectPool = new Queue<GameObject> ();
		//Debug.Log("m_GameObjectPath:" + m_GameObjectPath);
		m_StaffGameObject = Resources.Load<GameObject>(m_GameObjectPath);
		if(m_StaffGameObject == null)
			UnityEngine.Debug.LogError("m_StaffGameObject == null");
	}
	public void Release(){
		foreach (GameObject item in m_GameObjectPool) {
			GameObject.Destroy(item);
		}
		m_GameObjectPool.Clear();

	//	if(m_StaffGameObject != null)
		//	Resources.UnloadAsset(m_StaffGameObject);
	}

	//Take object
	public GameObject DequeueFromPool(){
		if (m_GameObjectPool.Count == 0) {
			GameObject tempGameObj = GameObject.Instantiate(m_StaffGameObject) as GameObject;
			if(tempGameObj == null)
				UnityEngine.Debug.LogError("tempGameObj == null");
			return tempGameObj;
		}
		return m_GameObjectPool.Dequeue ();
	}
	
	public void EnqueueToPool(GameObject gameObject){
		m_GameObjectPool.Enqueue(gameObject);
	}

}

