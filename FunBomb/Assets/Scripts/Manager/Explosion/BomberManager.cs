using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BomberManager : Singleton<BomberManager>
{
	private BomberPool m_BomberPool = BomberPool.Instance;
	public List<GameObject> m_BomberList = new List<GameObject>();
	public BomberManager(){
		SetBomberPath(PathHelper.ExplosionPath);
	}

	public void SetBomberPath(string path){
		m_BomberPool.Init(path);
	}

	public void LayBomber(Vector2 pos){
		GameObject bomber = m_BomberPool.DequeueFromPool();
		float pos_Z = bomber.transform.position.z;
		bomber.transform.position = new Vector3(pos.x, pos.y, pos_Z);
		m_BomberList.Add(bomber);
	}

	public void RecycleBomber(GameObject bomber){
		m_BomberPool.EnqueueToPool(bomber);
		m_BomberList.Remove(bomber);
	}
}

