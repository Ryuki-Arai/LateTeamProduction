using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{//�z��⃊�X�g�ɂ��Ă��Ȃ��̂͊e���A�g�����₷�����邽��
    [SerializeField, Header("�q����Ԃ�animationstate�̖��O{�ҋ@�����}")]
    string _childStandbyUp = "";
    [SerializeField, Header("�q����Ԃ�animationstate�̖��O{�ҋ@������}")]
    string _childStandbyDown = "";
    [SerializeField, Header("�q����Ԃ�animationstate�̖��O{�ҋ@�E����}")]
    string _childStandbyRight = "";
    [SerializeField, Header("�q����Ԃ�animationstate�̖��O{�ҋ@������}")]
    string _childStandbyLeft = "";
    [SerializeField, Header("�q����Ԃ�animationstate�̖��O{�s�������}")]
    string _childActionUp = "";
    [SerializeField, Header("�q����Ԃ�animationstate�̖��O{�s��������}")]
    string _childActionDown = "";
    [SerializeField, Header("�q����Ԃ�animationstate�̖��O{�s���E����}")]
    string _childActionRight = "";
    [SerializeField, Header("�q����Ԃ�animationstate�̖��O{�s��������}")]
    string _childActionLeft = "";
    [SerializeField, Header("�q����Ԃ�animationstate�̖��O{�J�b�g�C���p}")]
    string _childCutIn = "";

    [SerializeField, Header("��l��Ԃ�animationstate�̖��O{�ҋ@�����}")]
    string _adultStandbyUp = "";
    [SerializeField, Header("��l��Ԃ�animationstate�̖��O{�ҋ@������}")]
    string _adultStandbyDown = "";
    [SerializeField, Header("��l��Ԃ�animationstate�̖��O{�ҋ@�E����}")]
    string _adultStandbyRight = "";
    [SerializeField, Header("��l��Ԃ�animationstate�̖��O{�ҋ@������}")]
    string _adultStandbyLeft = "";
    [SerializeField, Header("��l��Ԃ�animationstate�̖��O{�s�������}")]
    string _adultActionUp = "";
    [SerializeField, Header("��l��Ԃ�animationstate�̖��O{�s��������}")]
    string _adultActionDown = "";
    [SerializeField, Header("��l��Ԃ�animationstate�̖��O{�s���E����}")]
    string _adultActionRight = "";
    [SerializeField, Header("��l��Ԃ�animationstate�̖��O{�s��������}")]
    string _adultActionLeft = "";
    [SerializeField, Header("�q����Ԃ�animationstate�̖��O{�J�b�g�C���p}")]
    string _adultCutIn = "";
    PlayerController _player;
    Animator _anim;
    /// <summary>�~�܂钼�O�̑��x����</summary>
    Vector2 _lastMovejoyStick;
    private void Start()
    {
        _player = GetComponent<PlayerController>();
        _anim = GetComponent<Animator>();
    }

    //private void Update()
    //{
    //    if (_player.JoyX != 0 && _player.JoyY != 0)
    //    {
    //        _lastMovejoyStick.x = _player.JoyX;
    //        _lastMovejoyStick.y = _player.JoyY;
    //    }
    //}
    /// <summary>ModeChange()�֐��Ŏg�p</summary>
    public void ModeChangeAnim()//Down�̕����̑ҋ@��Ԃɂ���
    {
        if (!_anim)
        {
            return;
        }
        if (!_player.AdultState)//�q����������
        {
            _anim.Play(_adultStandbyDown);
            _anim.Play(_adultCutIn);
        }
        else
        {
            _anim.Play(_childStandbyDown);
            _anim.Play(_childCutIn);
        }

    }
    /// <summary>AnimPlay�֐��Ŏg�p</summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void MoveAnim(float x, float y)
    {
        if (!_anim)
        {
            return;
        }
        if (x != 0 && y != 0)//�����Ă��鎞
        {
            if (-0.5 < y && y < 0.5)//���E
            {
                if (0.5 < x && x < 1)//�E
                {
                    if (!_player.AdultState)
                        _anim.Play(_childActionRight);
                    else
                        _anim.Play(_adultActionRight);
                }
                else if (-1 < x && x < -0.5)//��
                {
                    if (!_player.AdultState)
                        _anim.Play(_childActionLeft);
                    else
                        _anim.Play(_adultActionLeft);
                }
            }
            if (-0.5 < x && x < 0.5)//�㉺���ʂ̏���
            {
                if (0.5 < y && y < 1)//��
                {
                    if (!_player.AdultState)
                        _anim.Play(_childActionUp);
                    else
                        _anim.Play(_adultActionUp);
                }
                else if (-1 < y && y < -0.5)//��
                {
                    if (!_player.AdultState)
                        _anim.Play(_childActionDown);
                    else
                        _anim.Play(_adultActionDown);
                }
            }
        }
        else//�~�܂��Ă��鎞
        {
            if (-0.5 < _lastMovejoyStick.y && _lastMovejoyStick.y < 0.5)//���E
            {
                if (0.5 < _lastMovejoyStick.x && _lastMovejoyStick.x < 1)//�E
                {
                    if (!_player.AdultState)
                        _anim.Play(_childStandbyRight);
                    else
                        _anim.Play(_adultStandbyRight);
                }
                else if (-1 < _lastMovejoyStick.x && _lastMovejoyStick.x < -0.5)//��
                {
                    if (!_player.AdultState)
                        _anim.Play(_childStandbyLeft);
                    else
                        _anim.Play(_adultStandbyLeft);
                }
            }
            if (-0.5 < _lastMovejoyStick.x && _lastMovejoyStick.x < 0.5)//�㉺
            {
                if (0.5 < _lastMovejoyStick.y && _lastMovejoyStick.y < 1)//��
                {
                    if (!_player.AdultState)
                        _anim.Play(_childStandbyUp);
                    else
                        _anim.Play(_adultStandbyUp);
                }
                else if (-1 < _lastMovejoyStick.y && _lastMovejoyStick.y < -0.5)//��
                {
                    if (!_player.AdultState)
                        _anim.Play(_childStandbyDown);
                    else
                        _anim.Play(_adultStandbyDown);
                }
            }
        }
    }

    /// <summary>�v���C���[��Enemy�̍��E�ǂ��炩�Ɉړ������ăA�j���[�V�����Đ�</summary>
    public void TranslatePlayerPosAnimPlay(float speed, float toleranceDis)
    {
        if (!_anim)
        {
            return;
        }
        if (Mathf.Abs(transform.position.x - _player.ReturnPillowPos.x) > toleranceDis)
        {
            if (transform.position.x > _player.ReturnPillowPos.x)
            {
                transform.Translate(Vector2.left * Time.deltaTime * speed);
                if (!_player.AdultState)
                    _anim.Play(_childActionLeft);
                else
                    _anim.Play(_adultActionLeft);
            }
            else if (transform.position.x < _player.ReturnPillowPos.x)
            {
                transform.Translate(Vector2.right * Time.deltaTime * speed);
                if (!_player.AdultState)
                    _anim.Play(_childActionRight);
                else
                    _anim.Play(_adultActionRight);
            }
        }
        else
        {
            if (transform.position.y > _player.ReturnPillowPos.y)
            {
                transform.Translate(Vector2.down * Time.deltaTime * speed);
                if (!_player.AdultState)
                    _anim.Play(_childActionDown);
                else
                    _anim.Play(_adultActionDown);
            }
            else if (transform.position.y < _player.ReturnPillowPos.y)
            {
                transform.Translate(Vector2.up * Time.deltaTime * speed);
                if (!_player.AdultState)
                    _anim.Play(_childActionUp);
                else
                    _anim.Play(_adultActionUp);
            }
        }
    }
    /// <summary>����Ԃ��ۂɉE���Ȃ̂������Ȃ̂�</summary>
    /// <param name="pos">�G�̈ʒu</param>
    public void RightorLeft(Vector2 pos)
    {
        if (!_anim)
        {
            return;
        }
        if (pos.x > transform.position.x)//�E
        {
            if (!_player.AdultState)
                _anim.Play(_childStandbyRight);//�q��
            else
                _anim.Play(_adultStandbyRight);
        }
        else//��
        {
            if (!_player.AdultState)
                _anim.Play(_childStandbyLeft);//�q��
            else
                _anim.Play(_adultStandbyLeft);
        }
    }
}
