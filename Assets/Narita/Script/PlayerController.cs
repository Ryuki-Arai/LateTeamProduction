using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    /// <summary>�������x</summary>
    float _moveSpeed = 10f;
    /// <summary>���v</summary>
    float _timer = 0f;
    /// <summary>����Ԃ��܂łɂ����鎞��</summary>//atai
    float _timerlimit = 0.5f;
    Vector2 _moveVelocity;
    /// <summary>�~�܂钼�O�̑��x����</summary>
    Vector2 _lastMoveVelocity;
    /// <summary>���x��</summary>
    int _level = 1;
    public VariableJoystick _joyStick;
    Animator _anim;
    Rigidbody2D _rb;
    /// <summary>����Ԃ��W�I��ێ�����</summary>
    Returnpillow _pillowEnemy;

    /// <summary>���Ԃ�����</summary>
    bool _pillow = false;
    /// <summary>��l���q����</summary>
    bool _adultState = false;

    /// <summary>���Ԃ�����</summary>
    public bool Pillow { get => _pillow; set => _pillow = value; }

    public int Level { get => _level; set => _level = value; }

    public float Timer { get => _timer;}
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        float h = _joyStick.Horizontal;
        float v = _joyStick.Vertical;

        _moveVelocity = new Vector2(h, v).normalized * _moveSpeed;
        _rb.velocity = _moveVelocity;

        _lastMoveVelocity = _moveVelocity;

        if(Input.GetButton("Fire1"))//�E������
        {
            if(_pillowEnemy)//���Ԃ������ɂ�����
            {
                _timer += Time.deltaTime;
            }
        }
    }

    void LateUpdate()
    {
        if(_anim)
        {
            {//�����Ă����
                _anim.SetFloat("float�̖��O", _moveVelocity.x);
                _anim.SetFloat("float�̖��O", _moveVelocity.y);
                //��̏ꍇ�A�{Y
                //���̏ꍇ�A�[Y
                //�E��A�E���A�E�̏ꍇ�A�{X
                //����A�����A���̏ꍇ�A�[X
            }
            {//�~�܂��Ă����
                _anim.SetFloat("", _lastMoveVelocity.x);
                _anim.SetFloat("", _lastMoveVelocity.y);
                //��̏����ɉ����āA�v���C���[�������Ă��Ȃ����ƁB
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//�Q�Ă���G�̏������
    {
        if(collision.gameObject.CompareTag("Enemy��"))
        {
            _pillowEnemy = collision.GetComponent<Returnpillow>();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _pillowEnemy = null;
    }
    //void ReturnPillowCount()
    //{
    //    timer += Time.deltaTime;
    //    if(timerlimit <= timer)
    //    {
           
    //    }
    //}
}
