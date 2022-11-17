using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    /// <summary>�������x�i�q���j</summary>
    [SerializeField, Header("�q����Ԃ̓����X�s�[�h")]
    float _childMoveSpeed = 10f;
    /// <summary>�������x�i��l�j</summary>
    [SerializeField, Header("��l��Ԃ̓����X�s�[�h")]
    float _adultMoveSpeed = 15f;
    /// <summary>���v</summary>
    float _timer = 0f;
    /// <summary>����Ԃ��܂łɂ����鎞��</summary>//atai
    [SerializeField, Header("����Ԃ��܂łɂ����鎞��")]
    float _timerlimit = 0.5f;
    [SerializeField, Header("�덷�̋��e�͈�")]
    float toleranceDis = 0.2f;
    Vector2 _moveVelocity;
    /// <summary>�~�܂钼�O�̑��x����</summary>
    Vector2 _lastMovejoyStick;
    /// <summary>���x��</summary>
    int level = 1;
    public VariableJoystick _joyStick;
    Animator _anim;
    Rigidbody2D _rb;
    /// <summary>����Ԃ��W�I��script��ێ�����</summary>
    Returnpillow _pillowEnemy = null;
    /// <summary>����Ԃ��W�I��gameobject��ێ�����</summary>
    GameObject _pillowEnemyObject = null;
    /// <summary>�G�̈ʒu���</summary>
    Vector2 _enemyPos = default;
    /// <summary>�v���C���[������Ԃ���ʒu</summary>
    Vector2 _returnPillowPos = default;
    /// <summary>�G����ǂꂾ�����ꂽ�������疍��Ԃ����̊Ԋu</summary>
    float _returnPillowDisToEnemy = 1f;
    /// <summary>�v���C���[��_returnPillowPos�̋���</summary>
    float _returnPillowDisToPlayer;
    UIManager _ui;
    /// <summary>���Ԃ�����</summary>
    bool _pillow = false;
    /// <summary>��l���q����</summary>
    [SerializeField, Header("�v���C���[����l���q����")]
    bool _adultState = false;
    /// <summary>�v���C���[�̏�Ԋm�F�A�O���Q�Ɨp</summary>
    public bool AdultState { get => _adultState;}
    /// <summary>���Ԃ�����</summary>
    public bool Pillow { get => _pillow; set => _pillow = value; }
    public int Level { get => level; set => level = value; }
    public float Timerlimit { get => _timerlimit; set => _timerlimit = value; }
    public Returnpillow PillowEnemy { get => _pillowEnemy; set => _pillowEnemy = value; }
    public GameObject PillowEnemyObject { get => _pillowEnemyObject; set => _pillowEnemyObject = value; }

    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _ui = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_pillowEnemy);
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
        if (h != 0 && v != 0)
        {
            _lastMovejoyStick.x = h;
            _lastMovejoyStick.y = v;
        }

        if (Input.GetButton("Jump"))//�X�y�[�X������
        {
            if (_pillowEnemy)//���Ԃ������ɂ�����
            {
                TranslatePlayerPos();
                _timer += Time.deltaTime;
                _ui.ChargeSlider(_timer);
            }
        }
        if (Input.GetButtonDown("Jump"))//�����œ������߂ɋ����v�Z���s��,�X�y�[�X�L�[���
        {
            PlayerAndEnemyDis();
        }
        //else if (Input.GetButtonUp("Jump"))
        //{
        //    _timer = 0;
        //    _ui.ChargeSlider(_timer);
        //}
    }


    //void LateUpdate()
    //{
    //    if (_anim)
    //    {
    //        {//�����Ă����
    //            _anim.SetFloat("float�̖��O", _moveVelocity.x);
    //            _anim.SetFloat("float�̖��O", _moveVelocity.y);
    //            //��̏ꍇ�A�{Y
    //            //���̏ꍇ�A�[Y
    //            //�E��A�E���A�E�̏ꍇ�A�{X
    //            //����A�����A���̏ꍇ�A�[X
    //        }
    //        {//�~�܂��Ă����
    //            _anim.SetFloat("", _lastMoveVelocity.x);
    //            _anim.SetFloat("", _lastMoveVelocity.y);
    //            //��̏����ɉ����āA�v���C���[�������Ă��Ȃ����ƁB
    //        }
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)//�Q�Ă���G�̏������
    {
        if (collision.gameObject.CompareTag("ReturnPillow"))
        {
            _pillowEnemyObject = collision.gameObject;
            _pillowEnemy = _pillowEnemyObject.GetComponent<Returnpillow>();
            _enemyPos = _pillowEnemyObject.transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        InformationReset();
    }

    private void ChildMode(float h, float v)
    {
        _moveVelocity = new Vector2(h, v).normalized * _childMoveSpeed;
        AnimPlay(h, v);
    }

    private void AdultMode(float h, float v)
    {
        _moveVelocity = new Vector2(h, v).normalized * _adultMoveSpeed;
        AnimPlay(h, v);
    }

    public void ModeChange(bool change)//��l���A�q��������Ƃ��ɌĂяo���֐�
    {

        if (!_adultState)
        {
            _anim.Play("");
        }
        else
        {
            _anim.Play("");
        }
        _adultState = change;
    }
    void AnimPlay(float x, float y)//+x���E�A+y����
    {
        if (!_anim)
        {
            return;
        }
        if (!_adultState)
        {
            if (x != 0 && y != 0)
            {
                if (-0.5 < y && y < 0.5)//���E
                {
                    if (0.5 < x && x < 1)//�E
                    {
                        _anim.Play("ChildRight");
                    }
                    else if (-1 < x && x < -0.5)//��
                    {
                        _anim.Play("ChildLeft");
                    }
                }
                if (-0.5 < x && x < 0.5)//�㉺
                {
                    if (0.5 < y && y < 1)//��
                    {
                        _anim.Play("ChildUp");
                    }
                    else if (-1 < y && y < -0.5)//��
                    {
                        _anim.Play("ChildDown");
                    }
                }
            }
            else
            {
                if (-0.5 < _lastMovejoyStick.y && _lastMovejoyStick.y < 0.5)//���E
                {
                    if (0.5 < _lastMovejoyStick.x && _lastMovejoyStick.x < 1)//�E
                    {
                        _anim.Play("Player-Idle-right");
                    }
                    else if (-1 < _lastMovejoyStick.x && _lastMovejoyStick.x < -0.5)//��
                    {
                        _anim.Play("Player-Idle-left");
                    }
                }
                if (-0.5 < _lastMovejoyStick.x && _lastMovejoyStick.x < 0.5)//�㉺
                {
                    if (0.5 < _lastMovejoyStick.y && _lastMovejoyStick.y < 1)//��
                    {
                        _anim.Play("Player-Idle-Up");
                    }
                    else if (-1 < _lastMovejoyStick.y && _lastMovejoyStick.y < -0.5)//��
                    {
                        _anim.Play("Player-Idle-down");
                    }
                }
            }
        }
        else
        {
            if (x != 0 && y != 0)
            {
                if (-0.5 < y && y < 0.5)//���E
                {
                    if (0.5 < x && x < 1)//�E
                    {
                        _anim.Play("");
                    }
                    else if (-1 < x && x < -0.5)//��
                    {
                        _anim.Play("");
                    }
                }
                if (-0.5 < x && x < 0.5)//�㉺
                {
                    if (0.5 < y && y < 1)//��
                    {
                        _anim.Play("");
                    }
                    else if (-1 < y && y < -0.5)//��
                    {
                        _anim.Play("");
                    }
                }
            }
            else
            {
                if (-0.5 < _lastMovejoyStick.y && _lastMovejoyStick.y < 0.5)//���E
                {
                    if (0.5 < _lastMovejoyStick.x && _lastMovejoyStick.x < 1)//�E
                    {
                        _anim.Play("");
                    }
                    else if (-1 < _lastMovejoyStick.x && _lastMovejoyStick.x < -0.5)//��
                    {
                        _anim.Play("");
                    }
                }
                if (-0.5 < _lastMovejoyStick.x && _lastMovejoyStick.x < 0.5)//�㉺
                {
                    if (0.5 < _lastMovejoyStick.y && _lastMovejoyStick.y < 1)//��
                    {
                        _anim.Play("");
                    }
                    else if (-1 < _lastMovejoyStick.y && _lastMovejoyStick.y < -0.5)//��
                    {
                        _anim.Play("");
                    }
                }
            }
        }
    }
    public void InformationReset()//�S�����p
    {
        _pillowEnemyObject = null;
        _pillowEnemy = null;
        _enemyPos = default;
        _timer = 0;
        _ui.ChargeSlider(_timer);
    }

    private void PlayerAndEnemyDis()//�����v�Z
    {
        if (Vector2.Distance(transform.position, new Vector2(_enemyPos.x - _returnPillowDisToEnemy, _enemyPos.y))
        >= Vector2.Distance(transform.position, new Vector2(_enemyPos.x + _returnPillowDisToEnemy, _enemyPos.y)))
        {
            _returnPillowPos = new Vector2(_enemyPos.x + _returnPillowDisToEnemy, _enemyPos.y);
        }
        else
        {
            _returnPillowPos = new Vector2(_enemyPos.x - _returnPillowDisToEnemy, _enemyPos.y);
        }
    }
    private void TranslatePlayerPos()
    {
        if (_pillowEnemyObject)//Enemy���������Ă�����
        {
            _returnPillowDisToPlayer = Vector2.Distance(transform.position, _returnPillowPos);
            if (_returnPillowDisToPlayer > toleranceDis)//�덷�͈�
            {
                if (Mathf.Abs(transform.position.x - _returnPillowPos.x) > toleranceDis)
                {
                    if (transform.position.x > _returnPillowPos.x)
                    {
                        transform.Translate(Vector2.left * Time.deltaTime * _childMoveSpeed);
                    }
                    else if (transform.position.x < _returnPillowPos.x)
                    {
                        transform.Translate(Vector2.right * Time.deltaTime * _childMoveSpeed);
                    }
                }
                else
                {
                    if (transform.position.y > _returnPillowPos.y)
                    {
                        transform.Translate(Vector2.down * Time.deltaTime * _childMoveSpeed);
                    }
                    else if (transform.position.y < _returnPillowPos.y)
                    {
                        transform.Translate(Vector2.up * Time.deltaTime * _childMoveSpeed);
                    }
                }
            }
        }
    }
}
