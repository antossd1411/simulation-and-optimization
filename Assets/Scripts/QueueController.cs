using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QueueController : MonoBehaviour
{
    private Vector3 lastPlace;
    public GameObject serversContainer;
    public GameObject spawn;

    private Vector3 freeServer;
    private int bussyServers = 0;
    public void updatePlayerPosition(GameObject player) {
        player.gameObject.GetComponent<NavMeshAgent>().SetDestination(this.lastPlace);
        this.updatePlace(6);
    }


    public void substractQueue(Vector3 newPos) {
        this.bussyServers--;
        this.freeServer = newPos;
    }
    private void updatePlace(int space) {
        this.lastPlace += Vector3.left * space;
   }
    // Start is called before the first frame update
    void Start()
    {
        this.lastPlace = this.gameObject.transform.position;
        // Get gameobject
        GameObject atm = serversContainer.transform.GetChild(0).gameObject;
        this.freeServer = atm.transform.Find("AtmController").gameObject.transform.position;
    }

    private void updateFreeServer() {
        bool found = false;
        int x = 0;
        if (this.bussyServers < serversContainer.transform.childCount) {
            do {
                GameObject atm = serversContainer.transform.GetChild(x).gameObject;
                GameObject controller = atm.transform.Find("AtmController").gameObject;
                if (controller.transform.position == this.freeServer) {
                    controller.GetComponent<AtmController>().isBussy = true;
                }
                if (!controller.GetComponent<AtmController>().isBussy) {
                    this.freeServer = controller.transform.position;
                    found = true;
                }
                x++;
            } while (!found);
        }
    }

    private void OnTriggerEnter(Collider player) {
        if (this.bussyServers < serversContainer.transform.childCount) {
            player.gameObject.GetComponent<NavMeshAgent>().SetDestination(this.freeServer);
        }
    }

    private void OnTriggerStay(Collider player) {
        if (this.bussyServers < serversContainer.transform.childCount) {
            player.gameObject.GetComponent<NavMeshAgent>().SetDestination(this.freeServer);
        }
    }

    private void OnTriggerExit(Collider player) {
        player.gameObject.transform.SetParent(null, true);
        Vector3 lastPos = this.gameObject.transform.position;
        Vector3 currentPos = lastPos;
        for (int i = 0; i < this.spawn.transform.childCount; i++) {
            GameObject child = this.spawn.transform.GetChild(i).gameObject;
            currentPos = child.GetComponent<NavMeshAgent>().destination;
            child.GetComponent<NavMeshAgent>().SetDestination(lastPos);
            lastPos = currentPos;
        }
        this.bussyServers++;
        this.updatePlace(-6);
        this.updateFreeServer();
    }
}
