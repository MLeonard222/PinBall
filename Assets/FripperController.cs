using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour
{
    private HingeJoint myHingeJoint;

    private float defaultAngle = 20;

    private float flickAngle = -20;

    // 左側でタッチし始めた指のリスト
    List<int> leftFingerList = new List<int>();
    // 右側でタッチし始めた指のリスト
    List<int> rightFingerList = new List<int>();

    void Start()
    {
        this.myHingeJoint = GetComponent<HingeJoint>();

        SetAngle(this.defaultAngle);
    }

    void Update()
    {
        if (Input.touchCount == 0)
        {
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)) && tag == "LeftFripperTag")
            {
                SetAngle(this.flickAngle);
            }

            if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S)) && tag == "RightFripperTag")
            {
                SetAngle(this.flickAngle);
            }

            if ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S)) && tag == "LeftFripperTag")
            {
                SetAngle(this.defaultAngle);
            }

            if ((Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S)) && tag == "RightFripperTag")
            {
                SetAngle(this.defaultAngle);
            }
        }
        

        else
        {
            // タッチしている指の数だけ繰り返し
            foreach (Touch touch in Input.touches)
            {
                // タッチの開始を検知
                if (touch.phase == TouchPhase.Began)
                {
                    // タッチの開始が画面の左側のとき左側指リストに指のIDを追加
                    if (touch.position.x < 540.0f && tag == "LeftFripperTag")
                    {
                        SetAngle(this.flickAngle);
                        leftFingerList.Add(touch.fingerId);
                    }
                    // タッチの開始が画面の右側のとき右側指リストに指のIDを追加
                    if (touch.position.x >= 540.0f && tag == "RightFripperTag")
                    {
                        SetAngle(this.flickAngle);
                        rightFingerList.Add(touch.fingerId);
                    }
                }

                // 指が離れたときリストからその指のIDを削除
                if (touch.phase == TouchPhase.Ended)
                {
                    leftFingerList.Remove(touch.fingerId);
                    rightFingerList.Remove(touch.fingerId);
                }
            }

            // 左でタッチし始めた指が画面からすべて離れたとき
            if (leftFingerList.Count == 0 && tag == "LeftFripperTag")
            {
                SetAngle(this.defaultAngle);
            }
            // 右でタッチし始めた指が画面からすべて離れたとき
            if (rightFingerList.Count == 0 && tag == "RightFripperTag")
            {
                SetAngle(this.defaultAngle);
            }
        }
    }

    public void SetAngle(float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }

}
