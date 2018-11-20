using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStats : MonoBehaviour {
    //[TextArea]
    //[SerializeField]
    //private string MyStringText;
    //[Space(1)]

    [Header("Editor Values (Jump)")]
    public LayerMask whatIsGround;
    public float jumpForce;
    public float jumpTime;
    [Space(1)]

    [Header("Editor Values (Run)")]

    [Space(1)]

    [Header("In-Game Values (Jump)")]
    public bool stoppedJumping;
    public bool isHoldingJump;
    public float jumpTimeCounter;
    public bool grounded;
}