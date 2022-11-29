using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NakaiEnemy : MonoBehaviour
{
    [SerializeField]
    float _moveSpeed = 5f;
    Rigidbody2D _rb = null;
    /// <summary>�z��ԍ�</summary>
    int arrayNumber = 0;
    [SerializeField]
    bool playerFind = false;
    Transform[] points = null;
    Vector2 _dir;
    Animator _anim = null;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (points != null)//�|�C���g���󂯎���Ă���
        {
            if (!playerFind)//�v���C���[�������Ă��Ȃ�
            {
                GotoPoint(points);
            }
        }
    }
    private void LateUpdate()
    {
        if (!_anim)
            return;
        _anim.SetFloat("", _dir.x);//�̂��ɖ��O�����߂�
        _anim.SetFloat("", _dir.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerFind = true;
            Debug.Log("�v���C���[�������܂���");
            collision.GetComponent<PlayerController>().PlayerFind();
        }
    }
    /// <summary>�n�����͏��ԂɋC��t���邱��</summary>
    /// <param name="pointsArray"></param>
    public void GetPoints(Transform[] pointsArray)
    {
        points = new Transform[pointsArray.Length];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = pointsArray[i];
        }
    }
    /// <summary>�n���ꂽpoint���ɐi��</summary>
    /// <param name="pointsArray"></param>
    void GotoPoint(Transform[] pointsArray)
    {
        //�������g�ƃ|�C���g�̋��������߂�
        float distance = Vector2.Distance(transform.position, pointsArray[arrayNumber % pointsArray.Length].position);
        if (distance >= 0.5f)//�������Ȃ��Ȃ�A���B����܂�,0.5f�̓}�W�b�N�i���o�[�ɂȂ��Ă��邽�߂̂��ύX
        {
            //�������߂�
            _dir = (pointsArray[arrayNumber].position - transform.position).normalized * _moveSpeed;
            transform.Translate(_dir * Time.deltaTime);//���̑���
        }
        else//���B������
        {
            arrayNumber++;
        }
    }
 
}
