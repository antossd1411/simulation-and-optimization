using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation
{
    private int servers = 0;
    private float lambda, mu = 0;
    public GameObject atmTemplate;
    public GameObject atmContainer;
    public GameObject spawn;
    private GameObject queueController;
    public Simulation(int servers, float lambda, float mu) {
        this.servers = servers;
        this.lambda = lambda;
        this.mu = mu;
    }

    public void getTemplate(GameObject template, GameObject container, GameObject queue) {
        this.queueController = queue;
        this.atmContainer = container;
        for (int i = 1; i < this.atmContainer.transform.childCount; i++) {
            GameObject.Destroy(this.atmContainer.transform.GetChild(i).gameObject);
        }
        this.atmTemplate = template;
    }

    public void setMuDatabase(List<Probabilidades> database) {
        for (int i = 0; i < this.atmContainer.transform.childCount; i++) {
            GameObject atm = this.atmContainer.transform.GetChild(i).gameObject;
            GameObject controller = atm.transform.Find("AtmController").gameObject;
            controller.gameObject.GetComponent<AtmController>().setDatabase(database);
        }
    }

    public void setLambdaDatabase(GameObject spawn, List<Probabilidades> database) {
        List<float> simulacion = new List<float>();
        bool found = false;
        int x = 0;
        float random = 0;
        for (int i = 0; i < 25; i++) {
            random = Random.Range(0F, 1F);
            x = 0;
            while (!found) {
                if (random > database[x].getInferior() && random < database[x].getSuperior()) {
                    simulacion.Add(database[x].getValue());
                    found = true;
                }
                x++;
            }
            found = false;
        }
        spawn.gameObject.GetComponent<SpawnController>().getSimulacion(simulacion);
    }
    public void generateServers() {
        int position = 9;
        float y = 0;
        float z = 0;
        if (this.servers >= 1) {
            for (int i = 0; i < this.servers; i++) {
                GameObject atm = GameObject.Instantiate(atmTemplate, atmContainer.transform);
                y = atm.transform.position.y;
                z = atm.transform.position.z;
                atm.transform.position = new Vector3(position, y, z);
                GameObject controller = atm.transform.Find("AtmController").gameObject;
                // y = controller.transform.position.y;
                // z = controller.transform.position.z;
                // controller.transform.position = atm.transform.position;
                controller.gameObject.GetComponent<AtmController>().getQueueController(this.queueController);
                position += 9;
            }
        }
    }
}
