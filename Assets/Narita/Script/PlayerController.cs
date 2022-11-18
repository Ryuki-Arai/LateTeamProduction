using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public VariableJoystick _joyStick;
    /// <summary>�������x�i�q���j</summary>
    [SerializeField, Header("�q����Ԃ̓����X�s�[�h")]
    float _childMoveSpeed = 10f;
    /// <summary>�������x�i��l�j</summary>
    [SerializeField, Header("��l��Ԃ̓����X�s�[�h")]
    float _adultMoveSpeed = 15f;
    /// <summary>����Ԃ��܂łɂ����鎞��</summary>
    [SerializeField, Header("�덷�̋��e�͈�")]
    float toleranceDis = 0.2f;
    /// <summary>���v</summary>
    float _timer = 0f;
    /// <summary>�G����ǂꂾ�����ꂽ�������疍��Ԃ����̊Ԋu</summary>
    float _returnPillowDisToEnemy = 1f;
    /// <summary>�v���C���[��_returnPillowPos�̋���</summary>
    float _returnPillowDisToPlayer;
    /// <summary>_joyStick��X���̒l</summary>
    float _joyX;
    /// <summary>_joyStick��Y���̒l</summary>
    float _joyY;
    /// <summary>�ړ����x�v�Z����</summary>
    Vector2 _moveVelocity;
    /// <summary>�G�̈ʒu���</summary>
    Vector2 _enemyPos = default;
    /// <summary>�v���C���[������Ԃ���ʒu</summary>
    Vector2 _returnPillowPos = default;
    /// <summary>����Ԃ��W�I��script��ێ�����</summary>
    Returnpillow _pillowEnemy = null;
    /// <summary>����Ԃ��W�I��gameobject��ێ�����</summary>
    GameObject _pillowEnemyObject = null;
    /// <summary>��l���q����</summary>
    [SerializeField, Header("�v���C���[����l���q����")]
    bool _adultState = false;
    Rigidbody2D _rb;
    UIManager _ui;
    GameManager _gm;
    PlayerAnimController _animController;

    /// <summary>_joyStick��X���̒l,�O���Q�Ɨp</summary>
    public float JoyX { get => _joyX; }
    /// <summary>_joyStick��Y���̒l,�O���Q�Ɨp</summary>
    public float JoyY { get => _joyY; }
    /// <summary>�v���C���[�̏�Ԋm�F�A�O���Q�Ɨp</summary>
    public bool AdultState { get => _adultState; }
    /// <summary>����Ԃ��W�I��script</summary>
    public Returnpillow PillowEnemy { get => _pillowEnemy; set => _pillowEnemy = value; }
    /// <summary>����Ԃ��W�I��gameobject</summary>
    public GameObject PillowEnemyObject { get => _pillowEnemyObject; set => _pillowEnemyObject = value; }
    public Vector2 ReturnPillowPos { get => _returnPillowPos; }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ui = FindObjectOfType<UIManager>();
        _animController = GetComponent<PlayerAnimController>();
    }
    // Update is called once per frame
    void Update()
    {
        float _joyX = _joyStick.Horizontal;
        float _joyY = _joyStick.Vertical;
        ModeCheck(_joyX, _joyY);
        //�������Ōv�Z���ꂽ_moveVelocity����
        _rb.velocity = _moveVelocity;

        if (Input.GetButton("Jump"))//�X�y�[�X������
        {
            if (_pillowEnemy)//���Ԃ������ɂ�����
            {
                if (!_adultState)
                {
                    TranslatePlayerPos(_childMoveSpeed);
                }
                else
                {
                    TranslatePlayerPos(_adultMoveSpeed);
                }
                _timer += Time.deltaTime;
                _ui.ChargeSlider(_timer);
            }
        }
        if (Input.GetButtonDown("Jump"))//�����œ������߂ɋ����v�Z���s��,�X�y�[�X�L�[���
        {
            PlayerAndEnemyDis();
        }
    }

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

    private void ModeCheck(float h, float v)
    {
        if (!_adultState)
        {
            _moveVelocity = new Vector2(h, v).normalized * _childMoveSpeed;
        }
        else
        {
            _moveVelocity = new Vector2(h, v).normalized * _adultMoveSpeed;
        }
        _animController.MoveAnim(h, v);
    }
    public void ModeChange(bool change)//��l���A�q��������Ƃ��ɌĂяo���֐�
    {
        _animController.ModeChangeAnim();
        _adultState = change;
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
        if (_enemyPos != default)
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
        else
        {
            Debug.Log("�G�̈ʒu�����擾�o���Ă��܂���");
        }
    }
    private void TranslatePlayerPos(float speed)
    {
        if (_pillowEnemyObject)//Enemy���������Ă�����
        {
            _returnPillowDisToPlayer = Vector2.Distance(transform.position, _returnPillowPos);
            if (_returnPillowDisToPlayer > toleranceDis)//�덷�͈�
            {
                _animController.TranslatePlayerPosAnimPlay(speed, toleranceDis);
            }
            else
            {
                _animController.RightorLeft(_returnPillowPos);
                _timer += Time.deltaTime;
                _ui.ChargeSlider(_timer);
            }
        }
    }
    /// <summary>���������ꍇ�Ă�,�A�j���[�V�����C�x���g��p�֐�</summary>
    public void PlayerFind()
    {
        _gm.GameOver();
    }
}
