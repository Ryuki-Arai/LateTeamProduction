using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField, Tooltip("�^�����Ԃ�\���X���C�_�[")] Slider _chargeSlider = null;
    
    [SerializeField,Tooltip("�c�莞�Ԃ�\������e�L�X�g")] TextMeshProUGUI _timerText = null;
    
    [SerializeField,Tooltip("�N���A���ɕ\������UI")] GameObject _clearUI = null;

    [SerializeField, Tooltip("�Q�[���I�[�o�[���ɕ\������UI")] GameObject _gameOverUI = null;

    [SerializeField,Tooltip("�N���A�^�C����\������e�L�X�g")] TextMeshProUGUI _clearTimeText = null;
    
    [SerializeField,Tooltip("�J�b�g�C���p�̃A�j���[�^�[")] Animator _cutIn = null;
    bool _isRange = false;
    PlayerController _player = null;
    // Start is called before the first frame update
    
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _clearUI.SetActive(false);
        _gameOverUI.SetActive(false);
    }
    
    public void ChargeSlider(float charge , Animator chargeAnim) // �X���C�_�[�ƂЂ�����Ԃ��Ώۂ̃A�j���[�^�[�𐧌�
    {
        
        if(_player._returnPillowInPos)
        {
            chargeAnim.speed = 1;
        }
        else
        {
            chargeAnim.speed = 0;
        }
        if (charge == 0)
        {
            _isRange = true;
            return;
        }
        if(charge >= 1) charge = 1; 
        if(_isRange)
        {
            chargeAnim.Play("",0,0);
            _isRange = false;
        }
        _chargeSlider.value = charge;

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
        _clearTimeText.text = clearTime.ToString("F0")+" �b";
        _clearUI.SetActive(true);
    }
    public void GameOver() 
    {
        _gameOverUI.SetActive(true);
    }
    public void CutIn(bool before)
    {
        _cutIn.SetBool("isChild",before);
    }
}
