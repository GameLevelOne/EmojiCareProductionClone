using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour {
	public ParticleSystem particlesLeft;
	public ParticleSystem particlesRight;
	public ParticleSystem particleConfetti;
	public ParticleSystem particleBoom;

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

	public void ShowParticleConfetti()
	{
		particleConfetti.Clear();
		particleConfetti.Play();
	}

	public void ShowParticleStarBoom()
	{
		particleBoom.Clear();
		particleBoom.Play();
	}

	public void StopParticleConfettiAndStarBoom()
	{
		particleConfetti.Stop();
		particleBoom.Stop();
	}
}
