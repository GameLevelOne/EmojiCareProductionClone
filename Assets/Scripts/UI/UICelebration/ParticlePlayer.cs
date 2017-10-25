using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour {
	public ParticleSystem particles;

	public void ShowParticles(){
		particles.Emit(10);
	}
}
