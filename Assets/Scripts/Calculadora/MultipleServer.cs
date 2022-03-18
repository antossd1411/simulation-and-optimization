using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleServer {

	private float p0, ls, lq, ws, wq, efectiva, perdida = 0;
	private int servers, limit = 0;

	public MultipleServer(int servers, int limit) {
		this.servers = servers;
		this.limit = limit;
	}


	//NoLimit

	private float Factorial(int n) {
		if (n == 0) {
			return 1;
		} else {
			return n * this.Factorial (n - 1);
		}
	}

	public float P0(float ro) {
		float p0Acum = 0;
		for (int i = 0; i < this.servers; i++) {
			p0Acum += Mathf.Pow (ro, i) / this.Factorial (i);
		}
		p0Acum += Mathf.Pow(ro,this.servers) / (this.Factorial(this.servers) * (1 - ro / this.servers));
		return this.p0 = 1 / p0Acum;
	}
	private float Pn(int n, float ro, int servers) {
		float pn = this.p0 * Mathf.Pow (ro, n) / this.Factorial (n);
		if (n > servers) {
			pn = this.p0 * Mathf.Pow(ro, n) / (Mathf.Pow(servers, n - servers) * this.Factorial(servers)) ;
		}
		return pn;
	}
	public float Lq(int servers, float ro) {
		return this.lq = this.Pn (servers, ro, servers) * (servers * ro) / Mathf.Pow (servers - ro, 2);
	}

	public float Ls(float ro) {
		return this.ls = this.lq + ro;
	}
	public float Wq(float lambda) {
		return this.wq = this.lq / lambda;
	}
	public float Ws(float mu) {
		return this.ws = this.wq + 1 / mu;
	}

	public void setTable(GameObject table, GameObject template, float ro) {
		float pn = 0;
		float acum = 0;
		int i = 0;
		do {
			pn = this.Pn(i, ro, this.servers);
			acum += pn;
			GameObject row = GameObject.Instantiate(template, table.transform, false);
			row.transform.GetChild(0).GetComponent<Text>().text = "P(" + i + ") = " + pn.ToString("F5");
			row.transform.GetChild(1).GetComponent<Text>().text = acum.ToString("F5");
			i++;
		} while(pn >= 0.0001);
	}

	//Limit
	public float P0Limit(float ro) {
		float p0Acum = 0;
		for (int i = 0; i < this.servers; i++) {
			p0Acum += Mathf.Pow (ro, i) / this.Factorial (i);
		}
		if ((ro / this.servers) == 1) {
			p0Acum += Mathf.Pow (ro, this.servers) * (this.limit - this.servers + 1) / this.Factorial (this.servers);
		} else {
			p0Acum += Mathf.Pow (ro, this.servers) / (this.Factorial(this.servers) * (1 - ro / this.servers)) * (1 - (Mathf.Pow (ro / this.servers, this.limit - this.servers + 1)));
		}
		return this.p0 = 1 / p0Acum;
	}

	private float PnLimit(float ro, int n) {
		float pn = this.p0 * Mathf.Pow (ro, n) / this.Factorial (n);
		if (n > this.servers) {
			pn = this.p0 * Mathf.Pow (ro, n) / (this.Factorial(this.servers) * Mathf.Pow(this.servers, n - this.servers));
		}
		return pn;
	}

	public float LqLimit(float ro) {
		float lq = 0;
		for (int i = this.servers + 1; i <= this.limit; i++) {
			lq += (i - this.servers) * this.PnLimit (ro, i);
		}
		return this.lq = lq;
	}
	public float Efectiva(float lambda, float ro) {
		return this.efectiva = lambda * (1 - this.PnLimit(ro, this.limit));
	}
	public float Perdida(float lambda) {
		return this.perdida = lambda - this.efectiva;
	}
	public float LsLimit(float mu) {
		return this.ls = this.lq + this.efectiva / mu;
	}
	public float WsLimit() {
		return this.ws = this.ls / this.efectiva;
	}
	public float WqLimit() {
		return this.wq = this.lq / this.efectiva;
	}

	public void setTableLimit(GameObject table, GameObject template, float ro) {
		float pn = 0;
		float acum = 0;
		for (int i = 0; i <= this.limit; i++) {
			pn = this.PnLimit(ro, i);
			acum += pn;
			GameObject row = GameObject.Instantiate(template, table.transform, false);
			row.transform.GetChild(0).GetComponent<Text>().text = "P(" + i + ") = " + pn.ToString("F5");
			row.transform.GetChild(1).GetComponent<Text>().text = acum.ToString("F5");
		}
	}
}