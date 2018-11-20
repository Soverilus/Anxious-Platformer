using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStats : MonoBehaviour {
    HorizontalMovement myHM;
    VerticalMovement myVM;

    [Header("Misc Values")]
    public GameObject groundChecker;
    public PhysicsMaterial2D myPhysMaterial;
    public float friction;
    [Space(10)]

    [Header("Editor Values (Jump)")]
    public LayerMask whatIsGround;
    public float jumpForce;
    public float jumpTime;
    [Space(10)]

    [Header("Editor Values (Run)")]
    public float myMoveSpeed;
    [Space(10)]

    [Header("In-Game Values (Jump)")]
    public bool stoppedJumping;
    public bool isHoldingJump;
    public float jumpTimeCounter;
    public bool grounded;
    [Space(10)]

    [Header("In-Game Values (Run)")]
    public Vector2 input;

    private void Start() {
        SettleMiscValues();

        GetMovements();
        CreateStarts();
    }

    void SettleMiscValues() {
        myPhysMaterial = GetComponent<Collider2D>().sharedMaterial;
    }

    void GetMovements() {
        GetVertical();
        GetHorizontal();
    }

    void GetVertical() {
        myVM = new VerticalMovement();
        myVM.player = gameObject;
        myVM.groundChecker = groundChecker;
    }

    void GetHorizontal() {
        myHM = new HorizontalMovement();
        myHM.player = gameObject;
    }

    private void Update() {
        CreateUpdates();
    }

    void CreateStarts() {
        myHM.NewStart();
        myVM.NewStart();
    }

    void CreateUpdates() {
        myHM.NewUpdate();
        myVM.NewUpdate();
    }
}