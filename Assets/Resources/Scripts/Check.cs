using UnityEngine;
using System.Collections;

public class Check : MonoBehaviour {

    public int numObjects;
    private int maxHeight;

	// Use this for initialization
	void Start () {
        maxHeight = 0; 
        GameObject obj;
        for (int i = 0; i < numObjects; i++) {
            obj = GameObject.Find("FrameMarker" + i);
            obj.GetComponentInChildren<Renderer>().material.color = Color.cyan;
            obj.GetComponent<EnemyCapsule>().enabled = false;
        }
        InvokeRepeating("Tick", 5, 5);
	}
	
	// Update is called once per frame
	void Update () {
        GameObject ball = GameObject.Find("Ball");
        if (ball.transform.position.y > maxHeight){
            maxHeight = (int) ball.transform.position.y;
            Debug.Log("Ball reached height: " + maxHeight);
        }

	}

    void Tick() {
        System.Random rnd = new System.Random();
        int enemyId = rnd.Next(numObjects);
        int vanishId = rnd.Next(numObjects);

        GameObject obj;
        for(int i=0; i<numObjects; i++){
            obj = GameObject.Find("FrameMarker" + i);
            obj.GetComponentInChildren<CapsuleCollider>().enabled = true;
            obj.GetComponentInChildren<MeshRenderer>().enabled = true;
            obj.GetComponentInChildren<Renderer>().material.color = Color.cyan;
            obj.GetComponent<EnemyCapsule>().enabled = false;
        }
        GameObject enemy = GameObject.Find("FrameMarker" + enemyId);
        enemy.GetComponentInChildren<Renderer>().material.color = Color.red;
        enemy.GetComponent<EnemyCapsule>().enabled = true;

        GameObject gone = GameObject.Find("FrameMarker" + vanishId);
        gone.GetComponentInChildren<MeshRenderer>().enabled = false;
        gone.GetComponentInChildren<CapsuleCollider>().enabled = false;
    }

}
