using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Montecarlo {
	private float value = 0;
	public Montecarlo(float value) {
		this.value = value;
	}

	public void Update(float value)	{
		this.value = value;
	}

	public List<Probabilidades> distroExp() {
		float inferior = 0;
		int i = 1;
		float pn = 0;
		List<Probabilidades> list = new List<Probabilidades>();
		do {
			pn = this.Exp(i);
			Probabilidades prob = new Probabilidades(i, inferior, pn);
			list.Add(prob);
			inferior = pn;
			i++;
		} while(1 - pn > 0.0001);
		Probabilidades lastProb = new Probabilidades (i, inferior, 1);
		list.Add (lastProb);
		return list;
	}

	private float Exp(float n) {
		return 1 - (1 / Mathf.Exp(n * this.value));
	}
}
