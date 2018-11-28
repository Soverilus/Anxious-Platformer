using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStats : MonoBehaviour {
    HorizontalMovement myHM;
    VerticalMovement myVM;

    [Header("Misc Values")]
    public JumpEffect myJE;
    [HideInInspector]
    StatHandler mySH;
    GameController myGameController;
    public GameObject groundChecker;
    public PhysicsMaterial2D myPhysMaterial;
    public float friction;
    public Vector3 gravityMultiplier;
    public LayerMask whatIsFallDeath;
    public LayerMask whatIsEnemyDeath;
    public LayerMask whatIsTrapDeath;
    public float fallDeath;
    public float timeLeft;
    public float maxTime;
    public int whichTile;
    public Transform myEndGoalTransform;
    [HideInInspector]
    public float myEndGoal;
    Collider2D myCol;
    Vector3 originalGravity;
    Rigidbody2D myRB;
    [HideInInspector]
    public bool isDead = false;
    [Space(10)]

    [Header("Editor Values (Jump)")]
    public LayerMask whatIsGround;
    public float origJumpForce;
    [HideInInspector]
    public float jumpForce;
    public float jumpTime;
    public bool canJump;
    public float airControlMult;
    [Space(10)]

    [Header("Editor Values (Run)")]

    public float moveSpeed;
    [HideInInspector]
    public float myMoveSpeed;
    public bool canGoLeft;
    public bool canGoRight;
    [Space(10)]

    [Header("In-Game Values (Jump)")]
    public bool stoppedJumping;
    public bool isHoldingJump;
    public float jumpTimeCounter;
    public bool grounded;
    public float jumpSpecialTimer;
    [Space(10)]

    [Header("In-Game Values (Run)")]
    public Vector2 input;

    private void StartEarly() {
        myPhysMaterial = GetComponent<Collider2D>().sharedMaterial;
        originalGravity = Physics2D.gravity;
        myRB = GetComponent<Rigidbody2D>();
        myCol = GetComponent<Collider2D>();
        mySH = GameObject.FindGameObjectWithTag("StatHandler").GetComponent<StatHandler>();
        mySH.StatHandlerStart(maxTime);
        canGoLeft = mySH.canGoLeft;
        canGoRight = mySH.canGoRight;
        myMoveSpeed = mySH.myMoveSpeedMult * moveSpeed;
        jumpForce = mySH.jumpForceMult * origJumpForce;
        Physics.gravity = new Vector3(0f, mySH.gravityMultiplier * originalGravity.y, 0f);
        whichTile = mySH.whichTileRand;
        timeLeft = mySH.timeLeft;
    }

    private void Start() {
        StartEarly();
        SettleMiscValues();
        GetMovements();
        CreateStarts();
    }

    void SettleMiscValues() {
        myJE = GetComponent<JumpEffect>();
        myGameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        myGameController.whichTile = whichTile;
        myEndGoal = myEndGoalTransform.position.x;
    }

    void GetMovements() {
        GetVertical();
        GetHorizontal();
    }

    void GetVertical() {
        myVM = new VerticalMovement();
        myVM.player = gameObject;
        myVM.groundChecker = groundChecker;
        myVM.airControlMult = airControlMult;
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

    public void IWon() {
        myGameController.Victory();
    }

    void CheckVictoryDeathConditions() {
        timeLeft -= Time.deltaTime;
        if (transform.position.y <= fallDeath) {
            myGameController.Death("Fall");
        }
        if (myCol.IsTouchingLayers(whatIsEnemyDeath)) {
            myGameController.Death("Enemy");
        }
        if (myCol.IsTouchingLayers(whatIsTrapDeath)) {
            myGameController.Death("Trap");
        }
        if (timeLeft <= 0) {
            myGameController.Death("Time");
        }
    }
}