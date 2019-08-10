﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FastAndSlowEffect : MonoBehaviour
{
    [Range(0.1f,2.0f)] public float timeScale = 1f;
    private float minTimeScale = 0.1f;
    private float maxTimeScale = 2.0f;

    private float alreadyCalled = 0;

    [Header("Fill in Applicable")]
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private MovingPlatform movingPlatform;
    [SerializeField] private speedingPlatform speedingPlatform;
    [SerializeField] private Fan fan;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Rotate rotations;
    [SerializeField] private AudioSource audio;

    private Vector3 DefaultVelocity;

    private float DefaultNavMeshSpeed;

    private float DefaultWaitToGoBack;
    private float DefaultplatformSpeed;

    private float DefaultSpeedPlatform;

    private float DefaultFanSpeed;

    private float DefaultAudioVolume;

    private void Start()
    {
        if (rigidbody)
        {
            DefaultVelocity = rigidbody.velocity;
        }

        if (navMeshAgent)
        {
            DefaultNavMeshSpeed = navMeshAgent.speed;
        }

        if (speedingPlatform)
        {
            DefaultSpeedPlatform = speedingPlatform.speed;
        }

        if (movingPlatform)
        {
            DefaultWaitToGoBack = movingPlatform.waitToGoBack;
            DefaultplatformSpeed = movingPlatform.platformSpeed;
        }

        if (fan)
        {
            DefaultFanSpeed = fan.speed;
        }

        if (particles)
        {
            DefaultParticleSpeed = particles.main.startSpeed.constant;
        }

        if (rotations)
        {
            DefaultRotateSpeed = rotations.rotSpeed;
        }

        if (audio)
        {
            DefaultAudioVolume = audio.volume;
        }
    }

    public void NewTimeScale(float pTimeChange)
    {
        //Protects from calling it twice on the same frame
        if (Time.time == alreadyCalled)
            return;
        else
            alreadyCalled = Time.time;



        timeScale = Mathf.Clamp(timeScale + pTimeChange, minTimeScale, maxTimeScale);

        if (rigidbody)
        {
            rigidbody.velocity = DefaultVelocity * timeScale;
        }

        if (navMeshAgent)
        {
            navMeshAgent.speed = DefaultNavMeshSpeed * timeScale;
        }

        if (speedingPlatform)
        {
            speedingPlatform.speed = DefaultSpeedPlatform * timeScale;
        }

        if (movingPlatform)
        {
            movingPlatform.waitToGoBack = DefaultWaitToGoBack / timeScale;
            movingPlatform.platformSpeed = DefaultplatformSpeed * timeScale;
        }

        if (fan)
        {
            fan.speed = DefaultFanSpeed * (timeScale + 0.1f);
            fan.changeSize((int)timeScale);
        }

        if (particles)
        {
            particles.startSpeed = DefaultParticleSpeed * (timeScale + 0.1f);
        }

        if (rotations)
        {
            rotations.rotSpeed = DefaultRotateSpeed * (timeScale + 0.1f);
        }

        if (audio)
        {
            audio.volume = DefaultAudioVolume * (timeScale / 2f + 1f);
            DefaultAudioVolume = audio.volume;
        }
    }
}
