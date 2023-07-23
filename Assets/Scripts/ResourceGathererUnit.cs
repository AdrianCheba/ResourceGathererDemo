using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;

public class ResourceGathererUnit : MonoBehaviour, IUnit
{

    public enum State
    {
        Idle,
        Moving,
    }

    private const float speed = 7f;

    private Vector3 targetPosition;
    private float stopDistance;
    private Action onArrivedAtPosition;
    public State state;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject resours;

    private void Start()
    {
        animator = GetComponent<Animator>();
        resours.SetActive(false);
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Moving:
                HandleMovement();
                break;
        }
    }

    private void HandleMovement()
    {
        if (Vector3.Distance(transform.position, targetPosition) > stopDistance)
        {
            Vector3 moveDir = (targetPosition - transform.position).normalized;

            Vector3 movRotation = Vector3.RotateTowards(transform.forward, moveDir, 180f, 0.0f);

            transform.rotation = Quaternion.LookRotation(movRotation);

            transform.position = transform.position + moveDir * speed * Time.deltaTime;
        }
        else
        {
            if (onArrivedAtPosition != null)
            {
                Action tmpAction = onArrivedAtPosition;
                onArrivedAtPosition = null;
                state = State.Idle;
                tmpAction();
            }
        }
    }

    public bool IsIdle()
    {
        return state == State.Idle;
    }

    public void MoveTo(Vector3 position, float stopDistance, Action onArrivedAtPosition)
    {
        targetPosition = position;
        this.stopDistance = stopDistance;
        this.onArrivedAtPosition = onArrivedAtPosition;
        state = State.Moving;
    }

    public void PlayAnimation(string animName) 
    {
        switch (animName)
        {
            case "IsWalking":
                animator.SetTrigger("IsWalking");
                animator.ResetTrigger("IsCarrying");
                animator.ResetTrigger("IsIdle");
                resours.SetActive(false);
                break;
            case "IsCarrying":
                animator.SetTrigger("IsCarrying");
                animator.ResetTrigger("IsWalking");
                animator.ResetTrigger("IsIdle");
                resours.SetActive(true);
                break;
            case "IsIdle":
                animator.SetTrigger("IsIdle");
                animator.ResetTrigger("IsCarrying");
                animator.ResetTrigger("IsWalking");
                if(resours)
                resours.SetActive(false);
                break;

        }
    }

}

