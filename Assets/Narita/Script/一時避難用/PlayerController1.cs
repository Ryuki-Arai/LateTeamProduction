using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController1 : MonoBehaviour
{
    public VariableJoystick _joyStick;
    /// <summary>�������x�i�q���j</summary>
    [SerializeField, Header("�q����Ԃ̓����X�s�[�h")]
    float _childMoveSpeed = 10f;
    /// <summary>�������x�i��l�j</summary>
    [SerializeField, Header("��l��Ԃ̓����X�s�[�h")]
    float _adultMoveSpeed = 15f;
    /// <summary>�ړ����x�v�Z����</summary>
    Vector2 _moveVelocity;
    /// <summary>�Ō�Ɉړ����Ă�������</summary>
    Vector2 _lastMoveVelocity;
    /// <summary>�G�̈ʒu���</summary>
    Vector2 _enemyPos = default;
    /// <summary>��l���q����</summary>
    [SerializeField, Header("�v���C���[����l���q����")]
    bool _adultState = false;
    [SerializeField,Header("���̉��Ɏ����I�Ɉړ����Ă���Ƃ���true")]
    public bool _autoAnim = false;
    Rigidbody2D _rb;
    GameManager _gm;
    UIManager _ui;
    Animator _anim = null;
    /// <summary>�v���C���[�̏�Ԋm�F�A�O���Q�Ɨp</summary>
    public bool AdultState { get => _adultState; }
    /// <summary>����Ԃ��W�I��script</summary>

    //public Vector2 ReturnPillowPos { get => _returnPillowPos; }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _gm = FindObjectOfType<GameManager>();
        _ui = FindObjectOfType<UIManager>();
    }
    // Update is called once per frame
    void Update()
    {
        float _joyX = _joyStick.Horizontal;
        float _joyY = _joyStick.Vertical;
        //Debug.Log(_rb.velocity);
        ModeCheck(_joyX, _joyY);
        //�������Ōv�Z���ꂽ_moveVelocity����
        _rb.velocity = _moveVelocity;
        VelocitySave(_rb.velocity); 
    }
    private void LateUpdate()
    {
        if (!_anim)
            return;
        _anim.SetFloat("veloX", _rb.velocity.x);
        _anim.SetFloat("veloY", _rb.velocity.y);
        _anim.SetFloat("LastVeloX", _lastMoveVelocity.x);
        _anim.SetFloat("LastVeloY", _lastMoveVelocity.y);
        _anim.SetBool("adultState", _adultState);
        _anim.SetBool("autoMode", _autoAnim);
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
        if(velo.y != 0)
        {
            _lastMoveVelocity.y = velo.y;
        }
    }
    public void ModeChange(bool change)//��l���A�q��������Ƃ��ɌĂяo���֐�
    {
        _ui.CutIn(_adultState);
        _adultState = change;
    }

    /// <summary>���������ꍇ�Ă�,�A�j���[�V�����C�x���g��p�֐�</summary>
    public void PlayerFind()
    {
        _gm.GameOver();
    }
}
