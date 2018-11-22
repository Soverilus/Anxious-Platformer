using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStats : MonoBehaviour {
    HorizontalMovement myHM;
    VerticalMovement myVM;

    [Header("Misc Values")]
    GameController myGameController;
    public GameObject groundChecker;
    public PhysicsMaterial2D myPhysMaterial;
    public float friction;
    public Vector3 gravityMultiplier;
    public LayerMask whatIsFlag;
    public LayerMask whatIsFallDeath;
    public LayerMask whatIsEnemyDeath;
    public LayerMask whatIsTrapDeath;
    Collider2D myCol;
    Vector3 originalGravity;
    Rigidbody2D myRB;
    public float timeLeft;
    public float maxTime;
    public int whichTile;
    [Space(10)]

    [Header("Editor Values (Jump)")]
    public LayerMask whatIsGround;
    public float jumpForce;
    public float jumpTime;
    public bool canJump;
    [Space(10)]

    [Header("Editor Values (Run)")]
    public float myMoveSpeed;
    public bool canGoLeft;
    public bool canGoRight;
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
        originalGravity = Physics2D.gravity;
        myRB = GetComponent<Rigidbody2D>();
        myCol = GetComponent<Collider2D>();
        timeLeft = maxTime;
        myGameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        myGameController.whichTile = whichTile;
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
        CheckVictoryDeathConditions();
    }

    void CreateStarts() {
        myHM.NewStart();
        myVM.NewStart();
    }

    void CreateUpdates() {
        myHM.NewUpdate();
        myVM.NewUpdate();
    }

    void CheckVictoryDeathConditions() {
        if (myCol.IsTouchingLayers(whatIsFlag)){
            myGameController.Victory();
        }
        if (myCol.IsTouchingLayers(whatIsFallDeath)) {
            myGameController.Death(0);
        }
        if (myCol.IsTouchingLayers(whatIsEnemyDeath)) {
            myGameController.Death(1);
        }
        if (myCol.IsTouchingLayers(whatIsTrapDeath)) {
            myGameController.Death(2);
        }
        if (timeLeft <= 0) {
            myGameController.Death(3);
        }
    }
}