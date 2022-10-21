using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Returnpillow: MonoBehaviour
{
    //[SerializeField]
    //Slider _slider = null;
    /// <summary>���Ԃ����s�������ǂ���</summary>
    bool _returnPillow;
    /// <summary>�N���鎞�ԁi��j</summary>
    float _getupTime;
    /// <summary>player����l���q�����ŕω�����</summary>
    float _timeInPlayerStats;
    /// <summary>�Ԃ�V�������ꍇ�g�p����</summary>
    float _timeInBaby;
    /// <summary>���v</summary>
    float _timer;
    Animator _anim = null;
    Image _image = null;
    public bool ReturnPillow { get => _returnPillow; set => _returnPillow = value; }
    // Start is called before the first frame update
    void Start()
    {
        _returnPillow = false;
        _anim = GetComponent<Animator>();
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_returnPillow)
        {
            //�������蔻��͌�ŕύX�\��
            GetComponent<BoxCollider2D>().enabled = false;
            //���ς���F�͌�ŕύX�\��
            _image.color = Color.black;
        }
    }
    private void LateUpdate()
    {
        _anim.SetBool("bool�̖��O", _returnPillow);
    }

    private void OnTriggerStay2D(Collider2D collision)//�v���C���[�������蔻��̒��ɂƂǂ܂�����
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().Pillow = true;
            _timer += Time.deltaTime;
            //���l�̕��͌�ŕύX�\��
            _getupTime = Random.Range(2, 5);
            if (_getupTime <= _timer && _returnPillow)//�������Ԃ𒴂����@�{�@����Ԃ���Ă��Ȃ�������
            {
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _timer -= _timer;
    }
}
