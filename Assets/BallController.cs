using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    private float visiblePosZ = -6.5f;

    private GameObject gameoverText;


    private int point = 0;

    private GameObject pointText;


    // Start is called before the first frame update
    void Start()
    {
        this.gameoverText = GameObject.Find("GameOverText");
        this.pointText = GameObject.Find("PointText");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.z < this.visiblePosZ)
        {
            this.gameoverText.GetComponent<Text>().text = "Game Over";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SmallStarTag")
        {
            point += 5;
        }
        else if (collision.gameObject.tag == "LargeStarTag")
        {
            point += 10;
        }
        else if (collision.gameObject.tag == "SmallCloudTag")
        {
            point += 15;
        }
        else if (collision.gameObject.tag == "LargeCloudTag")
        {
            point += 20;
        }

        this.pointText.GetComponent<Text>().text = point.ToString();
    }
}
