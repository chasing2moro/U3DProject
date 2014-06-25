using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionManager : MonoBehaviour
{
	
	private Queue<GameObject>                  m_ExplosionPool;

	private GameObject DequeueExplosionFromPool(){
		if (m_ExplosionPool.Count == 0) {
			return Resources.Load<GameObject>("path");
		}
		return m_ExplosionPool.Dequeue ();
	}
	void Awark(){
		m_ExplosionPool = new Queue<GameObject> ();
	}
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}

