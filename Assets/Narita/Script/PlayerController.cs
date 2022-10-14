using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    /// <summary>�������x</summary>
    float moveSpeed = 10f;
    /// <summary>���v</summary>
    float timer = 0f;
    /// <summary>����Ԃ��܂łɂ����鎞��</summary>//atai
    float timerlimit = 0.5f;
    Vector2 moveVelocity;
    /// <summary>�~�܂钼�O�̑��x����</summary>
    Vector2 lastMoveVelocity;
    /// <summary>���x��</summary>
    int level = 1;
    public VariableJoystick joyStick;
    Animator anim;
    Rigidbody2D rb;
    /// <summary>����Ԃ��W�I��ێ�����</summary>
    Returnpillow pillowEnemy;

    /// <summary>���Ԃ�����</summary>
    bool pillow = false;
    /// <summary>��l���q����</summary>
    bool adultState = false;

    /// <summary>���Ԃ�����</summary>
    public bool Pillow { get => pillow; set => pillow = value; }

    public int Level { get => level; set => level = value; }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        float h = joyStick.Horizontal;
        float v = joyStick.Vertical;

        moveVelocity = new Vector2(h, v).normalized * moveSpeed;
        rb.velocity = moveVelocity;

        lastMoveVelocity = moveVelocity;

        if (Input.GetButton("Fire1"))//�E������
        {
            if (pillowEnemy)//���Ԃ������ɂ�����
            {
                timer += Time.deltaTime;
            }
        }
    }

    void LateUpdate()
    {
        if (anim)
        {
            {//�����Ă����
                anim.SetFloat("float�̖��O", moveVelocity.x);
                anim.SetFloat("float�̖��O", moveVelocity.y);
                //��̏ꍇ�A�{Y
                //���̏ꍇ�A�[Y
                //�E��A�E���A�E�̏ꍇ�A�{X
                //����A�����A���̏ꍇ�A�[X
            }
            {//�~�܂��Ă����
                anim.SetFloat("", lastMoveVelocity.x);
                anim.SetFloat("", lastMoveVelocity.y);
                //��̏����ɉ����āA�v���C���[�������Ă��Ȃ����ƁB
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//�Q�Ă���G�̏������
    {
        if (collision.gameObject.CompareTag("Enemy��"))
        {
            pillowEnemy = collision.GetComponent<Returnpillow>();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        pillowEnemy = null;
    }
    //void ReturnPillowCount()
    //{
    //    timer += Time.deltaTime;
    //    if(timerlimit <= timer)
    //    {

    //    }
    //}
}
