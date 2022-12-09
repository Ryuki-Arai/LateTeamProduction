using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    /// <summary>�^�����Ԃ�\���X���C�_�[</summary>
    [SerializeField] Slider _chargeSlider = null;
    /// <summary>�^���̊����x������\���I�u�W�F�N�g</summary>
    [SerializeField] GameObject _returnSign = null;
    /// <summary>�c�莞�Ԃ�\������e�L�X�g</summary>
    [SerializeField] TextMeshProUGUI _timerText = null;
    /// <summary>�N���A���ɕ\������UI</summary>
    [SerializeField] GameObject _clearUI = null;
    /// <summary>�N���A�^�C����\������e�L�X�g</summary>
    [SerializeField] TextMeshProUGUI _clearTimeText = null;
    /// <summary>�J�b�g�C���p�̃A�j���[�^�[</summary>
    [SerializeField] Animator _cutIn = null;
    bool _isRange = false;
    PlayerController _player = null;
    Animator _returnSignAnim = null;
    // Start is called before the first frame update
    private void Awake()
    {
        _returnSign.SetActive(false);
    }
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _returnSignAnim = _returnSign.GetComponent<Animator>();
        _clearUI.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChargeSlider(float charge)
    {
        
        if(_player._returnPillowInPos)
        {
            _returnSignAnim.speed = 1;
        }
        else
        {
            _returnSignAnim.speed = 0;
        }
        if (charge == 0)
        {
            _returnSign.SetActive(false);
            _isRange = true;
            return;
        }
        if(charge >= 1) charge = 1; 
        if(_isRange)
        {
            _returnSignAnim.Play("",0,0);
            _isRange = false;
        }
        _chargeSlider.value = charge;
        _returnSign.GetComponent<Slider>().value = charge;

        if(_player.PillowEnemyObject != null) _returnSign.transform.position = _player.PillowEnemyObject.transform.position + new Vector3(0, 1, 0);
        _returnSign.SetActive(true);

        //�X���C�_�[�����^���ɂȂ�����v���C���[��bool��ς���
        if (charge == 1)
        {
            
            _player.PillowEnemy.ReturnPillow = true;
            _chargeSlider.value = 0;
            
            _player.InformationReset();
            _isRange = true;
        }
    }
    public void TimerText(float time)
    {
        _timerText.text = time.ToString("F0");
    }
    public void Clear(float clearTime)
    {
        _clearTimeText.text = clearTime.ToString("F0");
        _clearUI.SetActive(true);
    }
    public void CutIn(bool before)
    {
        _cutIn.SetBool("isChild",before);
    }
}
