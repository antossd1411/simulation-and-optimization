using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colas {
	private float lambda, mu, ro = 0;
	private int servers, limit = 0;

	private Text[] outputs;
	private GameObject table;
	private GameObject rowTemplate;

	public Colas(float lambda, float mu, int servers, int limit) {
		this.lambda = lambda;
		this.mu = mu;
		this.ro = lambda / mu;
		this.servers = servers;
		this.limit = limit;
	}

	public void getOutputs(Text[] outputs) {
		this.outputs = outputs; //Ro, P0, Ls, Lq, Ws, Wq, Efec, Perd
		for (int i = 0; i < this.outputs.Length; i++) {
			this.outputs [i].text = "";
		}
	}

	public void getTemplate(GameObject template) {
		this.rowTemplate = template;
	}

	public void getTable(GameObject table) {
		this.table = table;
		if (this.table.transform.childCount > 0) {
			for (int i = 0; i < this.table.transform.childCount; i++) {
				GameObject.Destroy (this.table.transform.GetChild(i).gameObject);
			}
		}
	}


	public void Queue() {
		if (this.servers > 1) {
			MultipleServer cola = new MultipleServer (this.servers, this.limit);
			if (this.limit > 0) {
				this.outputs [0].text = this.ro.ToString ("F4");
				this.outputs [1].text = cola.P0Limit(this.ro).ToString ("F4");
				this.outputs [3].text = cola.LqLimit(this.ro).ToString ("F4");
				this.outputs [6].text = cola.Efectiva(this.lambda, this.ro).ToString ("F4");
				this.outputs [7].text = cola.Perdida(this.lambda).ToString ("F4");
				this.outputs [2].text = cola.LsLimit(this.mu).ToString ("F4");
				this.outputs [4].text = cola.WsLimit().ToString ("F4");
				this.outputs [5].text = cola.WqLimit().ToString ("F4");

				cola.setTableLimit (this.table, this.rowTemplate, this.ro);

			} else {
				this.outputs [0].text = this.ro.ToString ("F4");
				this.outputs [1].text = cola.P0(this.ro).ToString ("F4");
				this.outputs [3].text = cola.Lq(this.servers, this.ro).ToString ("F4");
				this.outputs [2].text = cola.Ls(this.ro).ToString ("F4");
				this.outputs [5].text = cola.Wq(this.lambda).ToString ("F4");
				this.outputs [4].text = cola.Ws(this.mu).ToString ("F4");
				this.outputs [6].text = this.lambda.ToString ();
				this.outputs [7].text = "0";

				cola.setTable (this.table, this.rowTemplate, this.ro);
			}
			cola = null;
		} else {
			OneServer cola = new OneServer ();
			if (this.limit > 0) {
				this.outputs[0].text = this.ro.ToString("F4");
				this.outputs[1].text = cola.P0Limit(this.limit, this.ro).ToString("F4");
				this.outputs[2].text = cola.LsLimit(this.limit, this.ro).ToString("F4");
				this.outputs[6].text = cola.Efectiva(this.lambda, this.ro, this.limit).ToString("F4");
				this.outputs[7].text = cola.Perdida(this.lambda).ToString("F4");
				this.outputs[3].text = cola.LqLimit(this.mu).ToString("F4");
				this.outputs[4].text = cola.WsLimit().ToString("F4");
				this.outputs[5].text = cola.WqLimit().ToString("F4");

				cola.setTableLimit (this.ro, this.limit, this.table, this.rowTemplate);

			} else {
				this.outputs [0].text = this.ro.ToString ("F4");
				this.outputs [1].text = cola.P0(this.ro).ToString ("F4");
				this.outputs [2].text = cola.Ls(this.ro).ToString ("F4");
				this.outputs [3].text = cola.Lq(this.ro).ToString ("F4");
				this.outputs [4].text = cola.Ws(this.lambda).ToString ("F4");
				this.outputs [5].text = cola.Wq(this.lambda).ToString ("F4");
				this.outputs [6].text = this.lambda.ToString ();
				this.outputs [7].text = "0";

				cola.setTable(this.ro, this.table, this.rowTemplate);
			}
			cola = null;
		}
	}
}