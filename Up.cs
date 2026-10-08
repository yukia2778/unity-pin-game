using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Up : MonoBehaviour {

    private Transform center;
    private Transform ballCounter;


    // Use this for initialization
    void Start () {
        center = GameObject.Find("Center").transform;
        ballCounter = GameObject.Find("BallCounter").transform;

    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -3) {
            BallUp();
        }
       
	}

    void BallUp() {
        if (Input.GetMouseButtonDown(0)) {
            StartCoroutine("FlyUp");
            if (ballCounter.childCount > 0) {
                ballCounter.GetChild(0).gameObject.SetActive(false);
            }
            
            
        }
    }

    

    IEnumerator FlyUp() {
        while (true) {
            transform.position += Vector3.up * 10f * Time.deltaTime;
            yield return null;
        }
    }
    private void OnTriggerEnter(Collider other) {

        if (other.tag == "UpLine") {
            transform.position = new Vector3(0, -3, 0);
            StopCoroutine("FlyUp");
            transform.SetParent(center);
            Destroy(ballCounter.GetChild(0).gameObject);
            GetComponent<LineRenderer>().enabled = true;
            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, center.position);
            other.GetComponent<AudioSource>().Play();
            this.tag = "Ball";
            Invoke("UPB", 0.1f);
        }
        if (other.tag == "Ball") {
            other.GetComponent<AudioSource>().Play();
            GameController.Instance.isOver = true;
            GameController.Instance.GameOver();
        }
    }

    void UPB() {
        if (ballCounter.childCount == 0 && !GameController.Instance.isOver) {
            GameController.Instance.NextLevel();
        }
        Spawn.Instance.UpdateBall();
    }
 

}
