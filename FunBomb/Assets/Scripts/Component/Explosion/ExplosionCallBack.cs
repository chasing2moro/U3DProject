using UnityEngine;
using System.Collections;

public class ExplosionCallBack : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ReturnToPool(){
		Debug.Log("AnimationCallBackFunction ReturnToPool()");
		ExplosionPool.Instance.EnqueueToPool(this.gameObject);
	}

	//
	public void ReturnBomberToPool(){

	}
}
