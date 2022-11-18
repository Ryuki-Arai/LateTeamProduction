using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    PlayerController _player;
    Animator _anim;
    /// <summary>�~�܂钼�O�̑��x����</summary>
    Vector2 _lastMovejoyStick;
    private void Start()
    {
        _player = GetComponent<PlayerController>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_player.JoyX != 0 && _player.JoyY != 0)
        {
            _lastMovejoyStick.x = _player.JoyX;
            _lastMovejoyStick.y = _player.JoyY;
        }
    }
    /// <summary>ModeChange�֐��Ŏg�p</summary>
    public void ModeChangeAnim()
    {
        if (!_anim)
        {
            return;
        }
        if (!_player.AdultState)
        {
            _anim.Play("");//�q���p�ҋ@�A�j���[�V����
        }
        else
        {
            _anim.Play("");//��l�p�ҋ@�A�j���[�V����
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
        if (!_player.AdultState)
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
                {
                    _anim.Play("");
                }
                else
                {
                    _anim.Play("");
                }
            }
            else if (transform.position.x < _player.ReturnPillowPos.x)
            {
                transform.Translate(Vector2.right * Time.deltaTime * speed);
                if (!_player.AdultState)
                {
                    _anim.Play("");
                }
                else
                {
                    _anim.Play("");
                }
            }
        }
        else
        {
            if (transform.position.y > _player.ReturnPillowPos.y)
            {
                transform.Translate(Vector2.down * Time.deltaTime * speed);
                if (!_player.AdultState)
                {
                    _anim.Play("");
                }
                else
                {
                    _anim.Play("");
                }

            }
            else if (transform.position.y < _player.ReturnPillowPos.y)
            {
                transform.Translate(Vector2.up * Time.deltaTime * speed);
                if (!_player.AdultState)
                {
                    _anim.Play("");
                }
                else
                {
                    _anim.Play("");
                }
            }
        }
    }
    /// <summary>����Ԃ��ۂɉE���Ȃ̂������Ȃ̂�</summary>
    /// <param name="pos"></param>
    public void RightorLeft(Vector2 pos)
    {
        if (!_anim)
        {
            return;
        }
        if (pos.x > 0)//�E
        {
            if (!_player.AdultState)
            {
                _anim.Play("");//�q��
            }
            else
            {
                _anim.Play("");
            }
        }
        else//��
        {
            if (!_player.AdultState)
            {
                _anim.Play("");//�q��
            }
            else
            {
                _anim.Play("");
            }
        }
    }
}
