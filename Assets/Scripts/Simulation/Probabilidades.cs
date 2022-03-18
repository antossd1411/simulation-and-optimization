using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probabilidades {
	private float value = 0;
	private float inferior = 0;
	private float superior = 0;

	public Probabilidades(float i, float inf, float sup) {
		this.value = i;
		this.inferior = inf;
		this.superior = sup;
	}

	public float getValue() {
		return this.value;
	}
	public float getInferior() {
		return this.inferior;
	}
	public float getSuperior() {
		return this.superior;
	}
}
