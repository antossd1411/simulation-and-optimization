using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Text timeText;
    public void setTime(string time) {
        // Get canvas of player
        GameObject canvas = this.transform.Find("Canvas").gameObject;
        this.timeText = canvas.transform.Find("Time").gameObject.GetComponent<Text>();
        this.timeText.text = time;
    }
    // Start is called before the first frame update
    void Start()
    {
        // this.timeText = this.transform.Find("Time").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.x < -40) {
            GameObject.Destroy(this.gameObject);
        }
    }
}
