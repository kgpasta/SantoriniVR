using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour {

    private Player m_PlayerOwner = null;

    private void Awake()
    {
        m_PlayerOwner = transform.parent.gameObject.GetComponent<Player>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
