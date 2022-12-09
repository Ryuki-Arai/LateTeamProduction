using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Returnpillow : MonoBehaviour
{
    [SerializeField,Header("����Ԃ����̃v���C���[�̈ʒu")]
    GameObject[] _returnPillouPos = new GameObject[2];
    /// <summary>���Ԃ����s�������ǂ���</summary>
    [SerializeField, Header("����Ԃ��ꂽ���ǂ���")]
    bool _returnPillow;
    //Image image = null;
    SpriteRenderer image = null;
    /// <summary>�N���鎞��</summary>
    [SerializeField, Header("�N���鎞�ԁi��j")]
    float _getupTime = 0f;
    /// <summary>player����l���q�����ŕω�����</summary>
    [SerializeField, Header("�v���C���[�̏�Ԃŕω����鎞��")]
    float _timeInPlayerStats = 0f;
    /// <summary>�Ԃ�V�������ꍇ�g�p����</summary>
    [SerializeField, Header("�����̒��ɐԂ�V�������ꍇ�ω����鎞��")]
    float _timeInBaby = 0f;
    Collider2D[] collider = null;
    /// <summary>���v</summary>
    float _timer;
    Animator _anim = null;
    public bool ReturnPillow { get => _returnPillow; set => _returnPillow = value; }
    public GameObject[] ReturnPillouPos { get => _returnPillouPos;}

    // Start is called before the first frame update
    void Start()
    {

        _returnPillow = false;
        _anim = GetComponent<Animator>();
        //image = GetComponent<Image>();
        image = GetComponent<SpriteRenderer>();
        collider = GetComponents<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_returnPillow)
        {
            //�������蔻��͌�ŕύX�\��
            foreach (var col in collider)
            {
                col.enabled = false;
            }
            //���ς���F�͌�ŕύX�\��
            if (!image)
            {
                Debug.LogError("image������܂���");
            }
            else
            {
                image.color = Color.black;
            }
        }
    }
    private void LateUpdate()
    {
        if (_anim)
            _anim.SetBool("bool�̖��O", _returnPillow);
    }

    private void OnTriggerStay2D(Collider2D collision)//�v���C���[�������蔻��̒��ɂƂǂ܂�����
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _timer += Time.deltaTime;
            if (_getupTime <= _timer && _returnPillow)//�������Ԃ𒴂��� + ����Ԃ���Ă��Ȃ������� + �v���C���[���܂��͈͓��ɂ�����
            {
                //�����������A�Q�[���I�[�o�[�̊֐�������
                GetUp(player);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _timer -= _timer;
    }
    /// <summary>�N���鎞�Ԃ����߂�֐�</summary>
    /// <param name="time"></param>
    public void GetUpTime(float time)
    {
        _getupTime = time;
    }

    private void GetUp(PlayerController player)
    {
        player.PlayerFind();
    }
}