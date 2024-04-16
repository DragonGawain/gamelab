using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // public ParticleSystem crystalParticles1; // Reference to the Particle System component
    // public ParticleSystem crystalParticles2; // Reference to the Particle System component

    public AudioSource musicAudioSource; // Reference to the AudioSource component for playing music

    ParticleSystem lightParticles = null;
    ParticleSystem darkParticles = null;
    DCore core;

    private void Awake()
    {
        core = GetComponent<DCore>();
        if (core != null)
        {
            core.OnHealthChanged += DeathCheck; // Subscribe to the OnHealthChanged event
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightPlayer")) // Check if the collider belongs to the player
        {
            lightParticles = other.gameObject.GetComponentInChildren<ParticleSystem>();
            lightParticles.Play();

            musicAudioSource.Play(); // Play the music
        }
        if (other.CompareTag("DarkPlayer")) // Check if the collider belongs to the player
        {
            darkParticles = other.gameObject.GetComponentInChildren<ParticleSystem>();
            darkParticles.Play();

            musicAudioSource.Play(); // Play the music
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightPlayer")) // Check if the collider belongs to the player
        {
            other.gameObject
                .GetComponentInChildren<ParticleSystem>()
                .Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            lightParticles = null;
            musicAudioSource.Stop(); // Play the music
        }
        if (other.CompareTag("DarkPlayer")) // Check if the collider belongs to the player
        {
            other.gameObject
                .GetComponentInChildren<ParticleSystem>()
                .Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            darkParticles = null;
            musicAudioSource.Stop(); // Play the music
        }
    }

    void DeathCheck(float coreHP)
    {
        if (coreHP <= 0)
        {
            if (lightParticles != null)
                lightParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            if (darkParticles != null)
                darkParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
