using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneServer {

	private float p0, ls, lq, ws, wq, efectiva, perdida = 0;
	//NoLimit
	public float P0(float ro) {
		return this.p0 = 1 - ro;
	}

	public float Ls(float ro) {
		return this.ls = ro / this.p0;
	}

	public float Lq(float ro) {
		return this.lq = this.ls - ro;
	}

	public float Ws(float lambda) {
		return this.ws = this.ls / lambda;
	}

	public float Wq(float lambda) {
		return this.wq = this.lq / lambda;
	}

	public void setTable(float ro, GameObject table, GameObject template) {
		float acum = 0;
		float pn = 0;
		int i = 0;
		do {
			pn = Mathf.Pow(ro, i) * this.p0;
			acum += pn;
			GameObject row = GameObject.Instantiate(template, table.transform, false);
			row.transform.GetChild(0).GetComponent<Text>().text = "P(" + i + ") = " + pn.ToString("F5");
			row.transform.GetChild(1).GetComponent<Text>().text = acum.ToString("F5");
			i++;
		} while (pn >= 0.0001);
	}

	//Limit
	public float P0Limit(int limit, float ro) {
		this.p0 = 1 / (limit + 1);
		if (ro != 0) {
			this.p0 = (1 - ro) / (1 - Mathf.Pow(ro, limit + 1));
		}
		return this.p0;
	}

	private float PnLimit(int n, int limit, float ro) {
		float pn = 1 / (limit + 1);
		if (ro != 1) {
			pn = this.p0 * Mathf.Pow (ro, n);
		}
		return pn;
	}

	public float LsLimit(int limit, float ro) {
		this.ls = limit / 2;
		if (ro != 1) {
			this.ls = 0;
			for (int i = 0; i <= limit; i++) {
				this.ls += i * this.PnLimit(i, limit, ro);
			}
		}
		return this.ls;
	}

	public float Efectiva(float lambda, float ro, int limit) {
		return this.efectiva = lambda * (1 - this.PnLimit(limit, limit, ro));
	}

	public float Perdida(float lambda) {
		return this.perdida = lambda - this.efectiva;
	}

	public float LqLimit(float mu) {
		return this.lq = this.ls - (this.efectiva / mu);
	}

	public float WsLimit() {
		return this.ws = this.ls / this.efectiva;
	}
	public float WqLimit() {
		return this.wq = this.lq / this.efectiva;
	}

	public void setTableLimit(float ro, int limit, GameObject table, GameObject template) {
		float pn = 0;
		float acum = 0;

		for (int i = 0; i <= limit; i++) {
			pn = this.PnLimit (i, limit, ro);
			acum += pn;
			GameObject row = GameObject.Instantiate(template, table.transform, false);
			row.transform.GetChild(0).GetComponent<Text>().text = "P(" + i + ") = " + pn.ToString("F5");
			row.transform.GetChild(1).GetComponent<Text>().text = acum.ToString("F5");
		}
	}
}
