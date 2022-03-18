using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SubmitController : MonoBehaviour {
	private bool submited = false;
	public InputField[] inputs; //lambda, mu, servers, limit
	public Text[] outputs;
	public GameObject rowTemplate;
	public GameObject table;

	public GameObject atmContainer;
	public GameObject atmTemplate;

	public GameObject spawn;
	public GameObject queueController;
	public void Submit() {

		if (submited) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		} else {
			GameObject buttonText = this.gameObject.transform.Find("Text").gameObject;
			buttonText.GetComponent<Text>().text = "Reset";
		int servers = 1;
		int limit = 0;
		float lambda, mu = 0;

		if (!inputs[2].text.Equals("") && int.Parse(inputs[2].text) > 1) {
			servers = int.Parse (inputs[2].text);
		}
		if (!inputs[3].text.Equals("") && int.Parse(inputs[3].text) > 0) {
			limit = int.Parse (inputs[3].text);
		}
		if (inputs [0].text.Equals ("") || inputs [1].text.Equals ("")) {
			Debug.Log (0);
		} else {
			lambda = float.Parse (inputs[0].text);
			mu = float.Parse (inputs[1].text);


			Colas cola = new Colas (lambda, mu, servers, limit);
			cola.getOutputs (outputs);
			cola.getTable (table);
			cola.getTemplate (rowTemplate);
			cola.Queue ();

			lambda = float.Parse(outputs[6].text);
			lambda = lambda / 60;
			mu = mu / 60;

			Montecarlo mc = new Montecarlo (mu);
			Simulation simul = new Simulation(servers - 1, lambda, mu);
			//
			simul.getTemplate(atmTemplate, atmContainer, queueController);
			//
			simul.generateServers();
			simul.setMuDatabase(mc.distroExp());
			mc.Update (lambda);
			simul.setLambdaDatabase(this.spawn, mc.distroExp());

			cola = null;
			mc = null;
			simul = null;
		}
			//
			this.submited = true; 
		}


	}
}