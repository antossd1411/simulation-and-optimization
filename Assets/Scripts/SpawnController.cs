using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private List<float> simulacion;
    public GameObject playerTemplate;
    private bool ready = false;
    private float timer = 0;
    private float time = 0;
    public GameObject queue;

    public void getSimulacion(List<float> database) {
        this.simulacion = database;
        this.updateTime();
        this.ready = true;
    }

    private void updateTime() {
        if (this.simulacion.Count > 0) {
        this.time = this.simulacion[0];
        this.simulacion.RemoveAt(0);
        } else {
            this.ready = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ready) {
            this.timer += Time.fixedDeltaTime;
            if (this.timer >= this.time * 4) {
                this.timer = 0;
                GameObject player = GameObject.Instantiate(playerTemplate, this.transform);
                queue.gameObject.GetComponent<QueueController>().updatePlayerPosition(player);
                player.gameObject.GetComponent<PlayerController>().setTime((this.time).ToString("F"));
                this.updateTime();
            }
        }
    }
}

