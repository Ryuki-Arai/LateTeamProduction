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
    [SerializeField, Header("���̉��ɍs�����Ƃ��Ă��鎞�i�X�y�[�X���͎��j�̃X�s�[�h")]
    float _autoMoveSpeed = 3f;
    [SerializeField, Header("�덷�̋��e�͈�"), Tooltip("�덷�̋��e�͈�")]
    float _toleranceDis = 0.3f;
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
    [SerializeField, Tooltip("����Ԃ����߂̃{�^��")]
    GameObject _returnPillowButton = null;
    [SerializeField, Header("�v���C���[����l���q����"), Tooltip("��l�̎�True")]
    bool _adultState = false;
    [SerializeField, Header("����Ԃ����Ƃ��Ă��鎞True"), Tooltip("����Ԃ���ʒu�ɂ��ăX�y�[�X�L�[�������Ă���Ƃ�True")]
    bool _returnPillowInPos = false;
    [SerializeField, Header("���̉��Ɏ����I�Ɉړ����Ă���Ƃ���true"), Tooltip("���̉��Ɏ����I�Ɉړ����Ă���Ƃ���true")]
    bool _autoAnim = false;
    [SerializeField, Header("�v���C���[�������Ă��鎞True"), Tooltip("�v���C���[��velocity��0�ł͂Ȃ��ꍇTrue")]
    bool _playerMove = false;
    [Tooltip("�E�����߂��Ƃ�True")]
    bool _closePos = false;
    [SerializeField, Header("�Q�Ă���G�̓����蔻����ɂ��āA�{�^���������ꂽ�Ƃ�True")]
    bool _returnPillowAction = false;
    [SerializeField, Tooltip("�X���C�_�[�ɒl��n�����߂Ɏg�p")]
    UIManager _ui = null;
    [SerializeField, Tooltip("�G�͈͓̔��ɓ������Ƃ��A�o���Ƃ��Ɏg�p")]
    SoundManager _sound = null;
    [SerializeField, Tooltip("�v���C���[�̓����蔻��")]
    CapsuleCollider2D _collider = null;
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
    public bool ReturnPillowAction { get => _returnPillowAction; set => _returnPillowAction = value; }

    private void Awake()
    {
        IsGame.GameManager.Instance.PlayerSet(this);
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<Animator>();
        _returnPillowButton.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        float _joyX = _joyStick.Horizontal;
        float _joyY = _joyStick.Vertical;
        _collider.isTrigger = _returnPillowInPos == true ? true : false;
        if(_pillowEnemy)
        {
            _returnPillowButton.SetActive(true);
        }
        else
        {
            _returnPillowButton.SetActive(false);
        }
        if (!_autoAnim)
        {
            ModeCheck(_joyX, _joyY);
            _rb.velocity = _moveVelocity;
            VelocitySave(_rb.velocity);
            _sound.GaugeStop();
        }
        //if (Input.GetButton("Jump"))//�X�y�[�X������
        //if (Input.GetMouseButton(0))
        if(_returnPillowAction || Input.GetButton("Jump"))
        {
            if (_pillowEnemy)//���Ԃ������ɂ�����
            {
                TranslatePlayerPos();
            }
        }
        else
        {
            _returnPillowInPos = false;
            _autoAnim = false;
        }
        //if (Input.GetButtonDown("Jump"))//�����œ������߂ɋ����v�Z���s��,�X�y�[�X�L�[��� || 
        if (_returnPillowAction || Input.GetButtonDown("Jump"))
        {
            if (_pillowEnemy)//���Ԃ������ɂ�����
            {
                if (_returnPillowPos == default)
                {
                    PlayerAndEnemyDis();
                }
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
            _playerAnim.SetBool("playerMove", _playerMove);
            _playerAnim.SetBool("adultState", _adultState);
            _playerAnim.SetBool("closePos", _closePos);
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
        if (collision.gameObject.CompareTag("ReturnPillow"))
        {
            InformationReset();
        }
        _returnCountTime = 0;
        _ui.ChargeSlider(_returnCountTime);
        _sound.KillSleeping();
    }

    private void ModeCheck(float h, float v)
    {
        if (!_autoAnim)
        {
            _moveVelocity = !_adultState ?
                       new Vector2(h, v).normalized * _childMoveSpeed : new Vector2(h, v).normalized * _adultMoveSpeed;
        }
    }
    private void VelocitySave(Vector2 velo)
    {
        _playerMove = velo == Vector2.zero ? false : true;
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
        _autoAnim = false;
        _returnPillowAction = false;
        _returnPillowInPos = false;
        _pillowEnemyObject = null;
        _pillowEnemy = null;
        _returnPillowPos = default;
        _returnCountTime = 0;
        _ui.ChargeSlider(_returnCountTime);
    }
    private void PlayerAndEnemyDis()//�����v�Z
    {
        if (!_pillowEnemyObject)
            return;
        if (Vector2.Distance(transform.position, _pillowEnemy.ReturnPillouPosLeft.position)
        >= Vector2.Distance(transform.position, _pillowEnemy.ReturnPillouPosRight.position))
        {
            _returnPillowPos = _pillowEnemy.ReturnPillouPosRight.position;
            _closePos = false;
        }
        else
        {
            _returnPillowPos = _pillowEnemy.ReturnPillouPosLeft.position;
            _closePos = true;
        }

    }
    private void TranslatePlayerPos()
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
                    transform.Translate(Vector2.left * Time.deltaTime * _autoMoveSpeed);
                }
                else if (transform.position.x < _returnPillowPos.x)
                {
                    transform.Translate(Vector2.right * Time.deltaTime * _autoMoveSpeed);
                }
            }
            else
            {
                if (transform.position.y > _returnPillowPos.y)
                {
                    transform.Translate(Vector2.down * Time.deltaTime * _autoMoveSpeed);
                }
                else if (transform.position.y < _returnPillowPos.y)
                {
                    transform.Translate(Vector2.up * Time.deltaTime * _autoMoveSpeed);
                }
            }
        }
        else if (_returnPillowDisToPlayer < _toleranceDis)
        {
            _returnPillowInPos = true;
            _returnCountTime += Time.deltaTime;
            _ui.ChargeSlider(_returnCountTime);
        }
    }
    ///// <summary>���������ꍇ�Ă�,�A�j���[�V�����C�x���g��p�֐�</summary>
    //public void PlayerFind()
    //{
    //    IsGame.GameManager.Instance.GameOver();
    //}
}
