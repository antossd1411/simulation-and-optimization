using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float speed = 0;
    // Start is called  the first frame update
    void Start()
    {
        this.speed = 18;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left")) {
            if (this.gameObject.transform.position.x >= -3) {
                    this.gameObject.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0), Space.World);
            }
        }
        if (Input.GetKey("right")) {
            if (this.gameObject.transform.position.x <= 75) {
                this.gameObject.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0), Space.World);
            }

        }
    }
}
