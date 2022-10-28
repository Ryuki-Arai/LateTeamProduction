using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    /// <summary>�������x�i�q���j</summary>
    [SerializeField,Header("�q����Ԃ̓����X�s�[�h")]
    float childMoveSpeed = 10f;
    /// <summary>�������x�i��l�j</summary>
    [SerializeField, Header("��l��Ԃ̓����X�s�[�h")]
    float adultMoveSpeed = 15f;
    /// <summary>���v</summary>
    float timer = 0f;
    /// <summary>����Ԃ��܂łɂ����鎞��</summary>//atai
    [SerializeField,Header("����Ԃ��܂łɂ����鎞��")]
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
    UIManager ui;
    /// <summary>���Ԃ�����</summary>
    bool pillow = false;
    /// <summary>��l���q����</summary>
    [SerializeField,Header("�v���C���[����l���q����")]
    bool adultState = false;

    /// <summary>���Ԃ�����</summary>
    public bool Pillow { get => pillow; set => pillow = value; }
    public int Level { get => level; set => level = value; }
    public float Timerlimit { get => timerlimit; set => timerlimit = value; }
    public Returnpillow PillowEnemy { get => pillowEnemy; set => pillowEnemy = value; }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ui = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        float h = joyStick.Horizontal;
        float v = joyStick.Vertical;

        if (!adultState)
        {
            ChildMode(h, v);
        }
        else
        {
            AdultMode(h, v);
        }

        rb.velocity = moveVelocity;
        lastMoveVelocity = moveVelocity;

        if (Input.GetButton("Jump"))//�E������
        {
            if (pillowEnemy)//���Ԃ������ɂ�����
            {
                timer += Time.deltaTime;
                ui.ChargeSlider(timer);
            }
        }
        else if(Input.GetButtonUp("Jump"))
        {
            timer = 0;
            ui.ChargeSlider(timer);
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
        if (collision.gameObject.CompareTag("ReturnPillow"))
        {
            pillowEnemy = collision.GetComponent<Returnpillow>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        pillowEnemy = null;
        timer = 0;
        ui.ChargeSlider(timer);
    }

    private void ChildMode(float h, float v)
    {
        moveVelocity = new Vector2(h, v).normalized * childMoveSpeed;
    }

    private void AdultMode(float h, float v)
    {
        moveVelocity = new Vector2(h, v).normalized * adultMoveSpeed;
    }
}
