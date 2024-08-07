using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f;
    [SerializeField]
    float jumpPower = 1f;
    Rigidbody2D rigid;

    Transform direction;
    bool isJumping = false;

    Vector3 rightScale = new Vector3(1, 1, 1);
    Vector3 leftScale = new Vector3(-1, 1, 1);

    Animator playerAnimator;


    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        direction = gameObject.GetComponentInChildren<Transform>();
        playerAnimator = GetComponentInChildren<Animator>();

    }


    void UpdateMove()
    {
        Vector3 temppos = Vector3.zero;

        if (Input.GetAxisRaw ("Horizontal") < 0)
        {
            Debug.Log("왼쪽으로 이동");
            direction.localScale = leftScale;
            playerAnimator.SetBool("is_Walking", true);
            temppos = Vector3.left;
        }
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            Debug.Log("오른쪽으로 이동");
            direction.localScale = rightScale;
            playerAnimator.SetBool("is_Walking", true);
            temppos = Vector3.right;
        }
        else
        {
            playerAnimator.SetBool("is_Walking", false);
        }

        transform.position += temppos * moveSpeed * Time.deltaTime;
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            isJumping = true;
        }

        if(!isJumping)
        {
            return;
        }

        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce (jumpVelocity, ForceMode2D.Impulse);
        isJumping=false;
        
    }



    bool m_ISAttackClick = false;

    protected void CallAnimtionEvetn(int p_val)
    {
        if (p_val == 0)
        {
            m_NextAttack = true;

            playerAnimator.SetBool("is_Attack", false);
            playerAnimator.SetBool("is_SecondAttack", false);
        }
        else
        {
            m_NextAttack = false;

            if (m_ISAttack02)
            {
                playerAnimator.SetBool("is_SecondAttack", true);

                //playerAnimator.SetBool("is_SecondAttack", false);
            }

            m_ISAttack02 = false;
            m_ISAttack01 = false;
        }

        Debug.Log($"함수호출 : {p_val}");
    }



    bool m_NextAttack = false;



    bool m_ISAttack02 = false;
    bool m_ISAttack01 = false;
    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            //m_ISAttackClick = true;

            //StartCoroutine()
            if(m_ISAttack01 == false)
            {
                playerAnimator.SetBool("is_SecondAttack", false);
                playerAnimator.SetBool("is_Attack", true);
                m_ISAttack01 = true;
            }
            

            if(m_NextAttack)
            {
                m_ISAttack02 = true;
            }

            
            //if(Input.GetKeyDown(KeyCode.X))
            //{
            //    playerAnimator.SetBool("is_SecondAttack", true);
            //}
            
        }
        //else if(CheckAnimationState() != "Attack_1" &&
        //    CheckAnimationState() != "Attack_2")
        //{
        //    playerAnimator.SetBool("is_Attack", false);
        //    playerAnimator.SetBool("is_SecondAttack", false);
        //}
    }


    void Update()
    {
        UpdateMove();
        Jump();
        Attack();
    }

    string CheckAnimationState()
    {
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Attack_1"))
            return "Attack_1";

        else if (stateInfo.IsName("Attack_2"))
            return "Attack_2";

        return "";
    }

    private void Awake()
    {
        GetComponentInChildren<AnimatorCallFN>()?.InitAction(CallAnimtionEvetn);
    }
}
