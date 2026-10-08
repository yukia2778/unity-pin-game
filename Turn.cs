using UnityEngine;
using System.Collections;

public class Turn : MonoBehaviour {

    public int Velocity = 1;//速率
    public int RotateDir = 1;//1为顺时针，2为逆时针
    public bool Swtich = false;

    public static Turn Instance;
    private void Awake() {
        Instance = this;
    }
    // Use this for initialization
    void Start() {
       
    }

    // Update is called once per frame
    void Update() {
        // 控制中心点是否旋转
        if (Swtich) {
            if (RotateDir == 1) {
                transform.Rotate(0, 0, -Velocity * Time.deltaTime);
            }

            if (RotateDir == 2) {
                transform.Rotate(0, 0, Velocity * Time.deltaTime);
            }
        }
        
    }

   

    public void OnPlay() {
        Swtich = true;
    }

    public void OnStop() {
        Swtich = false;
    }
}
