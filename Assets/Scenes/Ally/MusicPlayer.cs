using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    public ParticleSystem crystalParticles; // Reference to the Particle System component
    public AudioSource musicAudioSource; // Reference to the AudioSource component for playing music

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider belongs to the player
        {
            crystalParticles.Play(); // Play the particle system

            musicAudioSource.Play(); // Play the music

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider belongs to the player
        {
            crystalParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // Stop the particle system immediately and clear existing particles
            musicAudioSource.Stop(); // Play the music

        }
    }
}
