using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public VariableJoystick _joyStick;
    [SerializeField, Header("�q����Ԃ̓����X�s�[�h"), Tooltip("�������x�i�q���j")]
    float _childMoveSpeed = 10f;
    [SerializeField, Header("��l��Ԃ̓����X�s�[�h"), Tooltip("�������x�i��l�j")]
    float _adultMoveSpeed = 15f;
    [SerializeField, Header("�덷�̋��e�͈�"), Tooltip("�덷�̋��e�͈�")]
    float _toleranceDis = 0.5f;
    [Tooltip("����Ԃ����̃J�E���g�p�^�C�}�[")]
    float _returnCountTime = 0f;
    ///// <summary>�G����ǂꂾ�����ꂽ�������疍��Ԃ����̊Ԋu</summary>
    //float _returnPillowDisToEnemy = 0.5f;
    [Tooltip("�v���C���[��_returnPillowPos�̋���")]
    float _returnPillowDisToPlayer;
    [Tooltip("�ړ����x�v�Z����")]
    Vector2 _moveVelocity;
    [Tooltip("�����Ȃ��Ȃ������̍Ō�ɐi��ł�������")]
    Vector2 _lastMoveVelocity;
    [Tooltip("�v���C���[������Ԃ���ʒu���")]
    Vector2 _returnPillowPos = default;
    [Tooltip("�Q�Ă���G��script���")]
    Returnpillow _pillowEnemy = null;
    [Tooltip("�Q�Ă���G���̂���")]
    GameObject _pillowEnemyObject = null;
    [SerializeField, Header("�v���C���[����l���q����"), Tooltip("��l�̎�True")]
    bool _adultState = false;
    [SerializeField, Header("����Ԃ����Ƃ��Ă��鎞True"), Tooltip("����Ԃ���ʒu�ɂ��ăX�y�[�X�L�[�������Ă���Ƃ�True")]
    public bool _returnPillowInPos = false;
    [SerializeField, Header("���̉��Ɏ����I�Ɉړ����Ă���Ƃ���true"), Tooltip("���̉��Ɏ����I�Ɉړ����Ă���Ƃ���true")]
    bool _autoAnim = false;
    [SerializeField, Tooltip("�X���C�_�[�ɒl��n�����߂Ɏg�p")]
    UIManager _ui = null;
    [SerializeField, Tooltip("�G�͈͓̔��ɓ������Ƃ��A�o���Ƃ��Ɏg�p")]
    SoundManager _sound = null;
    Rigidbody2D _rb;
    Animator _playerAnim = null;
    [Tooltip("�v���C���[�̏�Ԋm�F�A�O���Q�Ɨp")]
    public bool AdultState { get => _adultState; }
    [Tooltip("�Q�Ă���G��script���A�O���Q�Ɨp")]
    public Returnpillow PillowEnemy { get => _pillowEnemy; set => _pillowEnemy = value; }
    [Tooltip("�Q�Ă���G���̂��́A�O���Q�Ɨp")]
    public GameObject PillowEnemyObject { get => _pillowEnemyObject; }
    [Tooltip("����Ԃ���ʒu�ɂ��ăX�y�[�X�L�[�������Ă���Ƃ�True, �O���Q�Ɨp")]
    public Vector2 ReturnPillowPos { get => _returnPillowPos; }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        float _joyX = _joyStick.Horizontal;
        float _joyY = _joyStick.Vertical;
        ModeCheck(_joyX, _joyY);
        if (!_autoAnim)
        {
            _rb.velocity = _moveVelocity;
        }
        VelocitySave(_rb.velocity);
        if (Input.GetButton("Jump"))//�X�y�[�X������
        {
            if (_pillowEnemy)//���Ԃ������ɂ�����
            {
                if (!_adultState)
                    TranslatePlayerPos(_childMoveSpeed);
                else
                    TranslatePlayerPos(_adultMoveSpeed);
            }
        }
        else
        {
            _returnPillowInPos = false;
        }
        if (Input.GetButtonDown("Jump"))//�����œ������߂ɋ����v�Z���s��,�X�y�[�X�L�[���
        {
            if (_pillowEnemy)//���Ԃ������ɂ�����
            {
                PlayerAndEnemyDis();
            }
        }
        if (!_playerAnim)
        {
            return;
        }
        else
        {
            _playerAnim.SetFloat("veloX", _rb.velocity.x);
            _playerAnim.SetFloat("veloY", _rb.velocity.y);
            _playerAnim.SetFloat("LastVeloX", _lastMoveVelocity.x);
            _playerAnim.SetFloat("LastVeloY", _lastMoveVelocity.y);
            _playerAnim.SetBool("adultState", _adultState);
            _playerAnim.SetBool("returnPillowInPos", _returnPillowInPos);
            _playerAnim.SetBool("autoMode", _autoAnim);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)//�Q�Ă���G�̏������
    {
        if (collision.TryGetComponent<Returnpillow>(out Returnpillow enemy))
        {
            _sound.SleepingVoice();
            _pillowEnemyObject = collision.gameObject;
            _pillowEnemy = enemy;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        InformationReset();
        _sound.KillSleeping();
    }

    private void ModeCheck(float h, float v)
    {
        if (h != 0 && v != 0)
        {
            _autoAnim = false;
        }
        _moveVelocity = !_adultState ?
           new Vector2(h, v).normalized * _childMoveSpeed : new Vector2(h, v).normalized * _adultMoveSpeed;
    }
    private void VelocitySave(Vector2 velo)
    {
        if (velo.x != 0)
        {
            _lastMoveVelocity.x = velo.x;
        }
        if (velo.y != 0)
        {
            _lastMoveVelocity.y = velo.y;
        }
    }
    public void ModeChange(bool change)//��l���A�q��������Ƃ��ɌĂяo���֐�
    {
        _adultState = change;
    }
    public void InformationReset()//�擾�����f�[�^�S�����A�X���C�_�[�̏�����
    {
        _pillowEnemyObject = null;
        _pillowEnemy = null;
        _returnCountTime = 0;
        _ui.ChargeSlider(_returnCountTime);
    }
    private void PlayerAndEnemyDis()//�����v�Z
    {
        if (!_pillowEnemyObject)
            return;
        _returnPillowPos = Vector2.Distance(transform.position, _pillowEnemy.ReturnPillouPos[0].position)
        >= Vector2.Distance(transform.position, _pillowEnemy.ReturnPillouPos[1].position) ?
        _pillowEnemy.ReturnPillouPos[1].position : _pillowEnemy.ReturnPillouPos[0].position;
    }
    private void TranslatePlayerPos(float speed)
    {
        if (!_pillowEnemyObject)
            return;
        _autoAnim = true;
        _returnPillowDisToPlayer = Vector2.Distance(transform.position, _returnPillowPos);
        if (_returnPillowDisToPlayer > _toleranceDis)//�덷�͈�
        {
            if (Mathf.Abs(transform.position.x - _returnPillowPos.x) > _toleranceDis)
            {
                if (transform.position.x > _returnPillowPos.x)
                {
                    transform.Translate(Vector2.left * Time.deltaTime * speed);
                    _rb.velocity = Vector2.left * Time.deltaTime * speed;
                }
                else if (transform.position.x < _returnPillowPos.x)
                {
                    transform.Translate(Vector2.right * Time.deltaTime * speed);
                    _rb.velocity = Vector2.right * Time.deltaTime * speed;
                }
            }
            else
            {
                if (transform.position.y > _returnPillowPos.y)
                {
                    transform.Translate(Vector2.down * Time.deltaTime * speed);
                    _rb.velocity = Vector2.down * Time.deltaTime * speed;
                }
                else if (transform.position.y < _returnPillowPos.y)
                {
                    transform.Translate(Vector2.up * Time.deltaTime * speed);
                    _rb.velocity = Vector2.up * Time.deltaTime * speed;
                }
            }
        }
        else
            _returnPillowInPos = true;
        _returnCountTime += Time.deltaTime;
        _ui.ChargeSlider(_returnCountTime);
    }
    /// <summary>���������ꍇ�Ă�,�A�j���[�V�����C�x���g��p�֐�</summary>
    public void PlayerFind()
    {
    }
}
