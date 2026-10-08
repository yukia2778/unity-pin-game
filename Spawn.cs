using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Spawn : MonoBehaviour {

    private GameObject ball;
    private GameObject upBall;
    private GameObject lText;
    private Transform ballCounter;
    private Transform ballArr;

    

    public static Spawn Instance;
    private void Awake() {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        ball = Resources.Load("Prefabs/Ball") as GameObject;
        upBall = Resources.Load("Prefabs/UpBall") as GameObject;
        lText = Resources.Load("Prefabs/LText") as GameObject;
        ballCounter = GameObject.Find("BallCounter").transform;
        ballArr = GameObject.Find("BallArr").transform;
         
        SetLevel(GameController.Instance.Level);
        SmallBall();
    }
	

    void SetLevel(int level) {
        StartCoroutine("Level", level);
    }

    IEnumerator Level(int level) {
        for (int i = 0; i < level * 2; i++) {
            //创建球
            GameObject b = Instantiate(ball);
            b.transform.SetParent(transform);
            b.transform.localPosition = transform.position + Vector3.right * 3;
            float z = i * 360f / (level * 2);//设置旋转角度
            b.transform.RotateAround(transform.position, Vector3.forward, z);
            LineRenderer line = b.GetComponent<LineRenderer>();
            line.SetPosition(0, b.transform.position);
            line.SetPosition(1, transform.position);
            yield return null;
        }
        Turn.Instance.Swtich = true;
        //更新每个球线段的的起始坐标
        while (true) {
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).GetComponent<LineRenderer>().SetPosition(0, transform.GetChild(i).position);
                //禁用Up组件
                if (transform.GetChild(i).GetComponent<Up>()) {
                    transform.GetChild(i).GetComponent<Up>().enabled = false;
                }
            }
            yield return null;
        }
    }

    void SmallBall() {
        for (int i = 0; i < GameController.Instance.BallCount; i++) {
            //创建底部小球
            GameObject b = Instantiate(upBall);
            b.transform.SetParent(ballArr);
            b.transform.position = Vector3.zero + Vector3.down * 5 + Vector3.down * 0.5f * i;
            //创建小球上的文字
            GameObject l = Instantiate(lText);
            l.transform.SetParent(ballCounter.transform);
            Vector3 pos = Camera.main.WorldToScreenPoint(b.transform.position);
            l.transform.position = pos;
            l.GetComponent<Text>().text = (GameController.Instance.BallCount - i).ToString();
            if (i == 0) {
                b.GetComponent<Up>().enabled = true;
            }
        }
       
    }

    public void UpdateBall() {
        print(ballArr.childCount +"---"+ ballCounter.childCount);
        //更新小球的位置和文本的位置
        for (int i = 0; i < ballArr.childCount; i++) {
            ballArr.GetChild(i).transform.position = Vector3.zero + Vector3.down * 5 + Vector3.down * 0.5f * i;
            ballArr.GetChild(0).GetComponent<Up>().enabled = true;
            Vector3 pos = Camera.main.WorldToScreenPoint(ballArr.GetChild(i).transform.position);
            ballCounter.GetChild(i).position = pos;
        }
       
    }

    
}
