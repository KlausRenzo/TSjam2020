﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public string movementAnimation, idleAnimation;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Play(string animation)
    {
        animator.Play(animation);
    }
}
