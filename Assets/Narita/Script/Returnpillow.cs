using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Returnpillow : MonoBehaviour, IRevers
{
    [SerializeField, Header("����Ԃ����̃v���C���[�̈ʒu"), Tooltip("����Ԃ����̃v���C���[�̈ʒu���")]
    Transform _returnPillouPosLeft;
    [SerializeField, Header("����Ԃ����̃v���C���[�̈ʒu"), Tooltip("����Ԃ����̃v���C���[�̈ʒu���")]
    Transform _returnPillouPosRight;
    [SerializeField, Header("����Ԃ��ꂽ���ǂ���"), Tooltip("����Ԃ��ꂽ���ǂ���")]
    bool _returnPillow = false;
    [SerializeField, Header("�N�������ǂ���"), Tooltip("�N�������ǂ���")]
    bool _getUp = false;
    [SerializeField, Header("Player���N�����Ă���ꍇTrue")]
    bool _playerIntrusion = false;
    [SerializeField, Header("�N���鎞�ԁi��j"), Tooltip("�N���鎞�ԁi�֐����Œl�����߂�j")]
    float _getupTime = 5f;
    [SerializeField,Header("player�������Ă���{��܂ł̎���")]
    float _resultDelayTime = 4f;
    [Tooltip("reaction��\������b����border�A_getupCountTimer�̔���")]
    float _reactionTime = 2.5f;
    //
    [SerializeField, Header("�v���C���[�̏�Ԃŕω����鎞��"), Tooltip("�v���C���[����l�̎��l��ω�������")]
    float _timeInPlayerStats = 0f;
    /// <summary>�Ԃ�V�������ꍇ�g�p����</summary>
    [SerializeField, Header("�����̒��ɐԂ�V�������ꍇ�ω����鎞��")]
    float _timeInBaby = 0f;
    //,�u//�v�ň͂��Ă���ϐ��͂���script�����ׂ��Ȃ̂��킩��Ȃ����A�b�ɏオ���Ă���̂ɂǂ��ɂ����݂��Ȃ����ߒ�`���Ă���B
    [Tooltip("�N����܂ł̎��Ԃ��J�E���g����^�C�}�[")]
    float _getupCountTimer;
    [SerializeField, Tooltip("reaction����ꏊ�ɐݒu���Ă���Gameobject")]
    GameObject _reactionObject = null; 
    [SerializeField, Tooltip("�������̉摜")]
    Sprite _reaction = null;
    [SerializeField,Tooltip("���g��animator")]
    Animator _returnPillowAnim = null;
    [Tooltip("�����蔻��̔z��")]
    Collider2D[] collider = null;
    SleepingEnemyAnimControle _sleepHumanController = null;
    [SerializeField, Tooltip("player���������Ƃ��Ɏg�p")]
    SoundManager _sound = null;
    [Tooltip("����Ԃ��ꂽ���ǂ����A�O���Q�Ɨp")]
    public bool ReturnPillow { get => _returnPillow; set => _returnPillow = value; }
    [Tooltip("����Ԃ����̃v���C���[�̈ʒu���A�O���Q�Ɨp")]
    public Transform ReturnPillouPosLeft { get => _returnPillouPosLeft; }
    public Transform ReturnPillouPosRight { get => _returnPillouPosRight; }
    // Start is called before the first frame update
    void Start()
    {
        _returnPillow = false;
        _reactionObject.SetActive(false);
        _sleepHumanController = GetComponent<SleepingEnemyAnimControle>();
        collider = GetComponents<Collider2D>();
    }


    void Update()
    {
        if(_playerIntrusion)
        {
            _getupCountTimer += Time.deltaTime;
            //�J�E���g��reaction���鎞�Ԃ�border�𒴂��Ă��āA�N���鎞�Ԃ�菬����������
            if (_getupCountTimer > _reactionTime && _getupCountTimer < _getupTime)
            {
                _reactionObject.SetActive(true);
            }
            if (_getupTime < _getupCountTimer)//�������Ԃ𒴂��� + ����Ԃ���Ă��Ȃ������� 
            {
                _sound.Discoverd();
                _sleepHumanController.Awaken();
                _getUp = true;
                _sleepHumanController.Discover();
            }
            if(_getupTime + _resultDelayTime < _getupCountTimer )
            {
                GetUp();
            }
        }
    }
    // Update is called once per frame

    private void OnTriggerStay2D(Collider2D collision)//�v���C���[�������蔻��̒��ɂƂǂ܂�����
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _playerIntrusion = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _getupCountTimer = 0;
        _playerIntrusion = false;
        _reactionObject.SetActive(false);
    }
    /// <summary>�N���鎞�Ԃ����߂�A�v���C���[�̏�Ԃ��ω������Ƃ��ɌĂԊ֐�</summary>
    /// <param name="time"></param>
    public void GetUpTimeAndTimeInPlayerStats(float getuptime)
    {
        _getupTime = getuptime;
    }

    public void GetUp()//�A�j���[�V�����C�x���g�p
    {
        IsGame.GameManager.Instance.GameOver();
    }

    public void ObjectRevers()
    {
        _returnPillow = true;
        if (_returnPillowAnim)
            _returnPillowAnim.SetBool("returnPillowPlay", _returnPillow);
        foreach (var col in collider)
        {
            col.enabled = false;
        }
    }
}