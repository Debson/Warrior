﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WalkMovement : MonoBehaviour
{
    public static int playerPoints = 0;

    [SerializeField]
    private float knockbackStrength;

    [SerializeField]
    public float knockBackLength;

    [SerializeField]
    float walkSpeed = 50;

    [HideInInspector]
    public float knockbackTimeCount;

    [HideInInspector]
    public bool knockFromRight;

    [HideInInspector]
    public float desiredWalkDirection;

    Rigidbody2D myBody;
    CrouchMovement crouchMovement;
    SpriteRenderer spriteRenderer;
    Color startColor;

    protected void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        crouchMovement = GetComponent<CrouchMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void Start()
    {
       startColor = spriteRenderer.color;
    }

    protected void FixedUpdate()
    {

            float desiredXVelocity;
            if (crouchMovement.isCrouching)
            {
                desiredXVelocity = desiredWalkDirection * walkSpeed * 0.4f * Time.fixedDeltaTime;
            }
            else
            {
                desiredXVelocity = desiredWalkDirection * walkSpeed * Time.fixedDeltaTime;
            }

            // TODO it in cooroutine
            if (knockbackTimeCount <= 0)
            {
                myBody.velocity = new Vector2(desiredXVelocity, myBody.velocity.y);
                spriteRenderer.color = startColor;
            }
            else
            {

                if (knockFromRight)
                {
                    myBody.velocity = new Vector2(-knockbackStrength, knockbackStrength / 3);
                    spriteRenderer.color = new Color(1, 0, 0);

                }
                if (!knockFromRight)
                {
                    myBody.velocity = new Vector2(knockbackStrength, knockbackStrength / 3);
                    spriteRenderer.color = new Color(1, 0, 0);
                }
                knockbackTimeCount -= Time.deltaTime;
            }
        }
}
