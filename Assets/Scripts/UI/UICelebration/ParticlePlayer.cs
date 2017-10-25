using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour {
	public ParticleSystem particlesLeft;
	public ParticleSystem particlesRight;

	public void ShowParticles(){
		particlesLeft.Clear();
		particlesRight.Clear();

		particlesLeft.Play();
		particlesRight.Play();
	}

	public void StopParticles(){
		particlesLeft.Stop();
		particlesRight.Stop();
	}
}
