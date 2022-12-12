using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
/// <summary>�p�j����G�̓����𐧌䂷��script</summary>
public class NakaiEnemy : MonoBehaviour//�ӂ�����񂷂̂̓A�j���[�V�������ŃR���C�_�[�̌�����ύX����Ηǂ��B
{
    [SerializeField,Tooltip("�i�J�C�̓�������")]
    float _moveSpeed = 5f;
    [SerializeField,Header("�ڕW�Ƃ̋����̗]�T"),Tooltip("�ڕW�Ƃ̋����̗]�T")]
    float _pointDis = 0.5f;
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
        if (_points != null)//�|�C���g���󂯎���Ă���
        {
            if (!_playerFind)//�v���C���[�������Ă��Ȃ�
            {
                if (!_levelBorder)//���n������������K�v���Ȃ��X�e�[�W�̃��x������Ȃ������ꍇ
                {
                    GotoPoint(_points);
                }
                else if(_levelBorder && !_lookAround)//���n������������K�v������X�e�[�W�̃��x�������A�����Ă��鎞
                {
                    GotoPoint(_points);
                }
            }
        }
    }
    private void LateUpdate()
    {
        if (!_anim)
            return;
        _anim.SetFloat("lastVeloX", Mathf.Abs(_lastMoveVelocity.x));//�̂��ɖ��O�����߂�
        _anim.SetFloat("lastVeloY", Mathf.Abs(_lastMoveVelocity.y));
        _anim.SetBool("levelBorder", _levelBorder);
        _anim.SetBool("lookAround", _lookAround);
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
    /// <summary>�n���ꂽpoint���ɐi��</summary>
    /// <param name="pointsArray"></param>
    void GotoPoint(Transform[] pointsArray)
    {
        //�������g�ƃ|�C���g�̋��������߂�
        float distance = Vector2.Distance(transform.position, pointsArray[pointsArray.Length % _pointArrayNumber].position);
        if (distance >= _pointDis)//�������Ȃ��Ȃ�A���B����܂�
        {
            _dir = (pointsArray[Mathf.Abs(pointsArray.Length % _pointArrayNumber)].position - transform.position).normalized * _moveSpeed;
            //�������߂�
            transform.Translate(_dir * Time.deltaTime);//���̑���
            _rb.velocity = _dir;
        }
        else//���B������
        {
            //GameManager�̔���p�ϐ��ŋt�ɉ��悤�ɂȂ����ꍇ��arrayNumber--;�ɂ���B
            _pointArrayNumber++;
        }
    }
    public void LookAroundIsActive()//�A�j���[�V�����C�x���g�p
    {
        _lookAround = !_lookAround;
    }
    private void VelocitySave(Vector2 velo)
    {
        if (velo != Vector2.zero)
            _lastMoveVelocity = velo;
    }
}
