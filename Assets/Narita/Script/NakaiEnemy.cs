using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
/// <summary>�p�j����G�̓����𐧌䂷��script</summary>
public class NakaiEnemy : MonoBehaviour//�ӂ�����񂷂̂̓A�j���[�V�������ŃR���C�_�[�̌�����ύX����Ηǂ��B
{
    [Tooltip("�ǂɓ���������")]
    int _number = 0;
    [Tooltip("_number�̍ő�l")]
    private int _maxNumber = 4;
    [Tooltip("��]����p�x")]
    float _rotateZ = 90f;
    [SerializeField,Tooltip("�i�J�C�̓�������")]
    float _moveSpeed = 5f;
    [SerializeField,Header("�ڕW�Ƃ̋����̗]�T"),Tooltip("�ڕW�Ƃ̋����̗]�T")]
    float _pointDis = 0.5f;
    [Tooltip("�����蔻��̃Y��")]
    float _MisalignmentPos = 0.64f;
    [SerializeField, Tooltip("player�������邽�߂̓����蔻��")]
    GameObject _atari = null;
    [Tooltip("�i�J�C�̓������ς�鎞�̃X�e�[�W�̃��x��")]
    int _stageLevelBorder = 0;
    [Tooltip("�󂯎����point�̗v�f�ԍ�")]
    int _pointArrayNumber = 0;
    [SerializeField, Tooltip("player���������Ƃ�True�ATrue�̎��ɂ̓i�J�C�͓����Ȃ�")]
    bool _playerFind = false;
    [SerializeField,Tooltip("�󂯎�����X�e�[�W�̃��x����_stageLevelBorder�ȏ�Ȃ�True")]
    bool _levelBorder = false;
    [Tooltip("�A�j���[�V�����C�x���g�p,�p�j�A�j���[�V���������������true")]
    bool _lookAround = false;
    [Tooltip("�O������󂯎��A�p�j����ʒu���")]
    Transform[] _points = null;
    [Tooltip("�ړ������ւ̑��x�v�Z����")]
    Vector2 _dir = default;
    [Tooltip("�����Ȃ��Ȃ������̍Ō�ɐi��ł�������")]
    Vector2 _lastMoveVelocity = default;
    Animator _anim = null;
    Rigidbody2D _rb = null;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        VelocitySave(_rb.velocity);
            if (!_playerFind)//�v���C���[�������Ă��Ȃ�
            {
            switch (_number % _maxNumber)//0%4 = 0;1%4 = 1;...
            {
                case 0:
                    {
                        _rb.velocity = Vector2.up * _moveSpeed;
                        break;
                    }
                case 1:
                    {
                        _rb.velocity = Vector2.left * _moveSpeed;
                        break;
                    }
                case 2:
                    {
                        _rb.velocity = Vector2.down * _moveSpeed;
                        break;
                    }
                case 3:
                    {
                        _rb.velocity = Vector2.right * _moveSpeed;
                        break;
                    }
            }
        }
    }
    /// <summary>�n�����͏��ԂɋC��t���邱��</summary>
    /// <param name="pointsArray"></param>
    public void GetPoints(Transform[] pointsArray)
    {
        _points = new Transform[pointsArray.Length];
        for (int i = 0; i < _points.Length; i++)
        {
            _points[i] = pointsArray[i];
        }
    }
    public void GetPlayerLevel(int level)
    {
        _levelBorder = _stageLevelBorder <= level ? true : false; 
    }
 
    public void LookAroundIsActive()//�A�j���[�V�����C�x���g�p
    {
        _lookAround = !_lookAround;
    }
    private void VelocitySave(Vector2 velo)
    {
        if (velo != Vector2.zero)
            _lastMoveVelocity = velo;

        if (!_anim)
            return;
        _anim.SetFloat("lastVeloX", _lastMoveVelocity.x);//�̂��ɖ��O�����߂�
        _anim.SetFloat("lastVeloY", _lastMoveVelocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("rotate");
        //Debug.Log(_lastMoveVelocity);
        _atari.transform.Rotate(0.0f, 0.0f, _rotateZ);
        _number++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerFind = true;
            Debug.Log("�v���C���[�������܂���");
            collision.GetComponent<PlayerController>().PlayerFind();
        }
    }
    void AtariPos(int num)
    {
        if(num == 1)
        {
            _atari.transform.localPosition = new Vector3(_atari.transform.localPosition.x, _MisalignmentPos, _atari.transform.localPosition.z);
        }
        if(num == 3)
        {
            _atari.transform.localPosition = new Vector3(_atari.transform.localPosition.x, _MisalignmentPos*-1, _atari.transform.localPosition.z);
        }
    }
}
