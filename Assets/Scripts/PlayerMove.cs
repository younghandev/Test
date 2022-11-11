using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;

    Rigidbody playerRigidbody;
    Animator playerAnimator;
    float hAxis, vAxis;
    Vector3 moveDirection;

    float attackDelay;
    [SerializeField] float rate = 0.5f;

    bool doAttack;
    bool isAttackReady;
    [SerializeField] bool isAim; // �� Ȯ�ο� SerializeField, Ȯ���ϰ� ������ ��

    // Callback Methods
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        GetInput();
        Aim();
        Attack();
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    // Public Methods


    // Private Methods
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        isAim = Input.GetButton("Fire2");
        doAttack = Input.GetButtonDown("Fire1");
    }

    void Move()
    {
        moveDirection = new Vector3(hAxis, 0, vAxis).normalized;
        //Vector3 tempDirection = new Vector3(0, playerRigidbody.velocity.y, 0);

        //playerRigidbody.velocity = moveDirection * moveSpeed;
        playerRigidbody.velocity = new Vector3(moveDirection.x * moveSpeed, playerRigidbody.velocity.y, moveDirection.z * moveSpeed);

        playerAnimator.SetBool("isWalk", moveDirection != Vector3.zero);
    }

    void Rotate()
    {
        if (moveDirection == Vector3.zero)
            return;

        transform.rotation = Quaternion.LookRotation(moveDirection);
    }

    // ���� ������ �� �����ؾ� ��
    void Attack()
    {
        attackDelay += Time.deltaTime;
        isAttackReady = rate < attackDelay;

        if (isAim && doAttack && isAttackReady)
        {
            // �߻�

            playerAnimator.SetTrigger("doAttack");

            attackDelay = 0;
        }
    }

    void Aim()
    {
        if (isAim)
        {
            // ī�޶� ���� ����
        }
    }
}
