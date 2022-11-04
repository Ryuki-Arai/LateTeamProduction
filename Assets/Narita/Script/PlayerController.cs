using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    /// <summary>�������x�i�q���j</summary>
    [SerializeField,Header("�q����Ԃ̓����X�s�[�h")]
    float _childMoveSpeed = 10f;
    /// <summary>�������x�i��l�j</summary>
    [SerializeField, Header("��l��Ԃ̓����X�s�[�h")]
    float _adultMoveSpeed = 15f;
    /// <summary>���v</summary>
    float _timer = 0f;
    /// <summary>����Ԃ��܂łɂ����鎞��</summary>//atai
    [SerializeField,Header("����Ԃ��܂łɂ����鎞��")]
    float _timerlimit = 0.5f;
    Vector2 _moveVelocity;
    /// <summary>�~�܂钼�O�̑��x����</summary>
    Vector2 _lastMoveVelocity;
    /// <summary>���x��</summary>
    int level = 1;
    public VariableJoystick _joyStick;
    Animator _anim;
    Rigidbody2D _rb;
    /// <summary>����Ԃ��W�I��script��ێ�����</summary>
    Returnpillow _pillowEnemy;
    /// <summary>����Ԃ��W�I��gameobject��ێ�����</summary>
    GameObject pillowEnemy;
    UIManager _ui;
    /// <summary>���Ԃ�����</summary>
    bool _pillow = false;
    /// <summary>��l���q����</summary>
    [SerializeField,Header("�v���C���[����l���q����")]
    bool _adultState = false;

    /// <summary>���Ԃ�����</summary>
    public bool Pillow { get => _pillow; set => _pillow = value; }
    public int Level { get => level; set => level = value; }
    public float Timerlimit { get => _timerlimit; set => _timerlimit = value; }
    public Returnpillow PillowEnemy { get => _pillowEnemy; set => _pillowEnemy = value; }
    public GameObject PillowEnemyObject { get => pillowEnemy; set => pillowEnemy = value; }
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _ui = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        float h = _joyStick.Horizontal;
        float v = _joyStick.Vertical;

        if (!_adultState)
        {
            ChildMode(h, v);
        }
        else
        {
            AdultMode(h, v);
        }

        _rb.velocity = _moveVelocity;
        _lastMoveVelocity = _moveVelocity;

        if (Input.GetButton("Jump"))//�E������
        {
            if (_pillowEnemy)//���Ԃ������ɂ�����
            {
                _timer += Time.deltaTime;
                _ui.ChargeSlider(_timer);
            }
        }
        else if(Input.GetButtonUp("Jump"))
        {
            _timer = 0;
            _ui.ChargeSlider(_timer);
        }
    }

    void LateUpdate()
    {
        if (_anim)
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
        //if (collision.gameObject.CompareTag("ReturnPillow"))
        //{
        //    pillowEnemy = collision.GetComponent<Returnpillow>();
        //}

        if(collision.gameObject.TryGetComponent<Returnpillow>(out var returnpillow))
        {
            pillowEnemy = collision.gameObject;
            _pillowEnemy = returnpillow;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _pillowEnemy = null;
        _timer = 0;
        _ui.ChargeSlider(_timer);
    }

    private void ChildMode(float h, float v)
    {
        _moveVelocity = new Vector2(h, v).normalized * _childMoveSpeed;
    }

    private void AdultMode(float h, float v)
    {
        _moveVelocity = new Vector2(h, v).normalized * _adultMoveSpeed;
    }
}
