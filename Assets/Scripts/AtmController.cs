using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AtmController : MonoBehaviour
{

    private Vector3 finalPos = new Vector3(-42, 0, -12);
    private List<Probabilidades> database;
    private float time, timer = 0;
    public bool isBussy, ready = false;
    private GameObject client;
    public GameObject queueController;
    public void setDatabase(List<Probabilidades> database) {
        this.database = database;
    }

    public void getQueueController(GameObject queue) {
        this.queueController = queue;
    }
    private void Simulate() {
        float random = Random.Range(0F, 1F);
        for (int i = 0; i < this.database.Count; i++) {
            if (random > this.database[i].getInferior() && random < this.database[i].getSuperior()) {
                this.time = this.database[i].getValue();
            }
        }
    }
    private void OnTriggerEnter(Collider player) {
        this.isBussy = true;
        this.client = player.gameObject;
        this.Simulate();
        GameObject canvas = player.transform.Find("Canvas").gameObject;
        GameObject text = canvas.transform.Find("Time").gameObject;
        text.GetComponent<Text>().text = this.time.ToString("F1");
        this.ready = true;
    }
    void OnTriggerExit(Collider player)
    {
        GameObject.Destroy(this.client.gameObject.GetComponent<Rigidbody>());
        GameObject.Destroy(this.client.gameObject.GetComponent<BoxCollider>());
        queueController.gameObject.GetComponent<QueueController>().substractQueue(this.gameObject.transform.position);
        // this.isBussy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ready) {
            this.timer += Time.fixedDeltaTime;
            if (this.timer >= this.time * 4) {
                this.isBussy = false;
                // this.client.GetComponent<NavMeshAgent>().Move(this.transform.position + Vector3.back * 4);
                this.client.GetComponent<NavMeshAgent>().SetDestination(this.finalPos);
                GameObject.Destroy(this.client.gameObject, 6);
                this.timer = 0;
                this.ready = false;
            }
        }
    }
}
