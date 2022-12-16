using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Returnpillow : MonoBehaviour
{
    [SerializeField, Header("����Ԃ����̃v���C���[�̈ʒu"), Tooltip("����Ԃ����̃v���C���[�̈ʒu���")]
    Transform[] _returnPillouPos = new Transform[2];
    [SerializeField, Header("����Ԃ��ꂽ���ǂ���"), Tooltip("����Ԃ��ꂽ���ǂ���")]
    bool _returnPillow = false;
    [SerializeField, Header("�N���鎞�ԁi��j"), Tooltip("�N���鎞�ԁi�֐����Œl�����߂�j")]
    float _getupTime = 0f;
    //
    [SerializeField, Header("�v���C���[�̏�Ԃŕω����鎞��"), Tooltip("�v���C���[����l�̎��l��ω�������")]
    float _timeInPlayerStats = 0f;
    /// <summary>�Ԃ�V�������ꍇ�g�p����</summary>
    [SerializeField, Header("�����̒��ɐԂ�V�������ꍇ�ω����鎞��")]
    float _timeInBaby = 0f;
    //,�u//�v�ň͂��Ă���ϐ��͂���script�����ׂ��Ȃ̂��킩��Ȃ����A�b�ɏオ���Ă���̂ɂǂ��ɂ����݂��Ȃ����ߒ�`���Ă���B
    [Tooltip("�����蔻��̔z��")]
    Collider2D[] collider = null;
    [Tooltip("�N����܂ł̎��Ԃ��J�E���g����^�C�}�[")]
    float _getupCountTimer;
    Animator _anim = null;
    [Tooltip("����Ԃ��ꂽ���ǂ����A�O���Q�Ɨp")]
    public bool ReturnPillow { get => _returnPillow; set => _returnPillow = value; }
    [Tooltip("����Ԃ����̃v���C���[�̈ʒu���A�O���Q�Ɨp")]
    public Transform[] ReturnPillouPos { get => _returnPillouPos;}

    // Start is called before the first frame update
    void Start()
    {;
        _returnPillow = false;
        _anim = GetComponent<Animator>();
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
        }
    }
    private void LateUpdate()
    {
        if (_anim)
            _anim.SetBool("returnPillowPlay", _returnPillow);
    }

    private void OnTriggerStay2D(Collider2D collision)//�v���C���[�������蔻��̒��ɂƂǂ܂�����
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _getupCountTimer += Time.deltaTime;
            if (_getupTime<= _getupCountTimer && _returnPillow)//�������Ԃ𒴂��� + ����Ԃ���Ă��Ȃ������� + �v���C���[���܂��͈͓��ɂ�����
            {
                //�����������A�Q�[���I�[�o�[�̊֐�������
                GetUp(player);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _getupCountTimer -= _getupCountTimer;
    }
    /// <summary>�N���鎞�Ԃ����߂�A�v���C���[�̏�Ԃ��ω������Ƃ��ɌĂԊ֐�</summary>
    /// <param name="time"></param>
    public void GetUpTimeAndTimeInPlayerStats(float getuptime)
    {
        _getupTime = getuptime;
    }

    private void GetUp(PlayerController player)
    {
        player.PlayerFind();
    }
}