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
    [SerializeField, Header("�덷�̋��e�͈�")]
    float _toleranceDis = 0.2f;
    /// <summary>���v</summary>
    float _timer = 0f;
    /// <summary>�G����ǂꂾ�����ꂽ�������疍��Ԃ����̊Ԋu</summary>
    float _returnPillowDisToEnemy = 0.5f;
    /// <summary>�v���C���[��_returnPillowPos�̋���</summary>
    float _returnPillowDisToPlayer;
    /// <summary>�ړ����x�v�Z����</summary>
    Vector2 _moveVelocity;
    /// <summary>�Ō�Ɉړ����Ă�������</summary>
    Vector2 _lastMoveVelocity;
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
    [SerializeField, Header("����Ԃ���ꏊ�ɂ��邩�ǂ���")]
    bool _returnPillowInPos = false;
    bool _autoAnim = false;
    Rigidbody2D _rb;
    UIManager _ui;
    GameManager _gm;
    Animator _anim = null;
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
        _anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        float _joyX = _joyStick.Horizontal;
        float _joyY = _joyStick.Vertical;
        Debug.Log(_rb.velocity);
        ModeCheck(_joyX, _joyY);
        //�������Ōv�Z���ꂽ_moveVelocity����
        _rb.velocity = _moveVelocity;
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
            PlayerAndEnemyDis();
        }
    }
    private void LateUpdate()
    {
        if (!_anim)
            return;
        _anim.SetFloat("veloX", _rb.velocity.x);
        _anim.SetFloat("veloY", _rb.velocity.y);
        _anim.SetFloat("LastVeloX", _lastMoveVelocity.x);
        _anim.SetFloat("LastVeloY", _lastMoveVelocity.y);
        _anim.SetBool("_adultState", _adultState);
        _anim.SetBool("_returnPillowInPos", _returnPillowInPos);
        _anim.SetBool("_autoMode", _autoAnim);
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
        if (h != 0 && v != 0)
        {
            _autoAnim = false;
        }
        _moveVelocity = !_adultState ?
            new Vector2(h, v).normalized * _childMoveSpeed : new Vector2(h, v).normalized * _adultMoveSpeed;
    }
    private void VelocitySave(Vector2 velo)
    {
        if (velo != Vector2.zero)
            _lastMoveVelocity = velo;
    }
    public void ModeChange(bool change)//��l���A�q��������Ƃ��ɌĂяo���֐�
    {
        _adultState = change;
    }
    public void InformationReset()//�擾�����f�[�^�S�����A�X���C�_�[�̏�����
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
            _returnPillowPos = Vector2.Distance(transform.position, new Vector2(_enemyPos.x - _returnPillowDisToEnemy, _enemyPos.y))
            >= Vector2.Distance(transform.position, new Vector2(_enemyPos.x + _returnPillowDisToEnemy, _enemyPos.y)) ?
            new Vector2(_enemyPos.x + _returnPillowDisToEnemy, _enemyPos.y) : new Vector2(_enemyPos.x - _returnPillowDisToEnemy, _enemyPos.y);
        else
            Debug.Log("�G�̈ʒu�����擾�o���Ă��܂���");
    }
    private void TranslatePlayerPos(float speed)
    {
        if (_pillowEnemyObject)//Enemy���������Ă�����
        {
            _autoAnim = true;
            _returnPillowDisToPlayer = Vector2.Distance(transform.position, _returnPillowPos);
            if (_returnPillowDisToPlayer > _toleranceDis)//�덷�͈�
            {
                if (Mathf.Abs(transform.position.x - _returnPillowPos.x) > _toleranceDis)
                {
                    if (transform.position.x > _returnPillowPos.x)
                    {
                        transform.Translate(Vector2.left * Time.deltaTime * speed);
                        _rb.velocity = Vector2.left;
                    }
                    else if (transform.position.x < _returnPillowPos.x)
                    {
                        transform.Translate(Vector2.right * Time.deltaTime * speed);
                        _rb.velocity = Vector2.right;
                    }
                }
                else
                {
                    if (transform.position.y > _returnPillowPos.y)
                    {
                        transform.Translate(Vector2.down * Time.deltaTime * speed);
                        _rb.velocity = Vector2.down;
                    }
                    else if (transform.position.y < _returnPillowPos.y)
                    {
                        transform.Translate(Vector2.up * Time.deltaTime * speed);
                        _rb.velocity = Vector2.up;
                    }
                }
            }
            else
                _returnPillowInPos = true;
                _timer += Time.deltaTime;
                _ui.ChargeSlider(_timer);
        }
    }
    /// <summary>���������ꍇ�Ă�,�A�j���[�V�����C�x���g��p�֐�</summary>
    public void PlayerFind()
    {
        _gm.GameOver();
    }
}
