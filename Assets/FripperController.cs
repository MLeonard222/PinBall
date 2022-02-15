using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour
{
    private HingeJoint myHingeJoint;

    private float defaultAngle = 20;

    private float flickAngle = -20;

    // �����Ń^�b�`���n�߂��w�̃��X�g
    List<int> leftFingerList = new List<int>();
    // �E���Ń^�b�`���n�߂��w�̃��X�g
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
            // �^�b�`���Ă���w�̐������J��Ԃ�
            foreach (Touch touch in Input.touches)
            {
                // �^�b�`�̊J�n�����m
                if (touch.phase == TouchPhase.Began)
                {
                    // �^�b�`�̊J�n����ʂ̍����̂Ƃ������w���X�g�Ɏw��ID��ǉ�
                    if (touch.position.x < 540.0f && tag == "LeftFripperTag")
                    {
                        SetAngle(this.flickAngle);
                        leftFingerList.Add(touch.fingerId);
                    }
                    // �^�b�`�̊J�n����ʂ̉E���̂Ƃ��E���w���X�g�Ɏw��ID��ǉ�
                    if (touch.position.x >= 540.0f && tag == "RightFripperTag")
                    {
                        SetAngle(this.flickAngle);
                        rightFingerList.Add(touch.fingerId);
                    }
                }

                // �w�����ꂽ�Ƃ����X�g���炻�̎w��ID���폜
                if (touch.phase == TouchPhase.Ended)
                {
                    leftFingerList.Remove(touch.fingerId);
                    rightFingerList.Remove(touch.fingerId);
                }
            }

            // ���Ń^�b�`���n�߂��w����ʂ��炷�ׂė��ꂽ�Ƃ�
            if (leftFingerList.Count == 0 && tag == "LeftFripperTag")
            {
                SetAngle(this.defaultAngle);
            }
            // �E�Ń^�b�`���n�߂��w����ʂ��炷�ׂė��ꂽ�Ƃ�
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
