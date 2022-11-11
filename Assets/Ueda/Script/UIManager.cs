using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    /// <summary>�^�����Ԃ�\���X���C�_�[</summary>
    [SerializeField] Slider _chargeSlider = null;
    /// <summary>���̏�ɕ\������X���C�_�[</summary>
    [SerializeField] GameObject _chargeSliderMini = null;
    /// <summary>�c�莞�Ԃ�\������e�L�X�g</summary>
    [SerializeField] TextMeshProUGUI _timerText = null;
    /// <summary>�N���A���ɕ\������UI</summary>
    [SerializeField] GameObject _clearUI = null;
    /// <summary>�N���A�^�C����\������e�L�X�g</summary>
    [SerializeField] TextMeshProUGUI _clearTimeText = null;
    /// <summary>�Q�[���I�[�o�[���ɕ\������e�L�X�g</summary>
    [SerializeField] GameObject _gameOverUI = null;

    PlayerController _player = null;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
        _clearUI.SetActive(false);
        _gameOverUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChargeSlider(float charge)
    {
        if(charge >= 1) charge = 1; 
        _chargeSlider.value = charge;
        _chargeSliderMini.GetComponent<Slider>().value = charge;

        if(_player.PillowEnemyObject != null) _chargeSliderMini.transform.position = _player.PillowEnemyObject.transform.position + new Vector3(0, 1, 0);
        _chargeSliderMini.SetActive(true);

        //�X���C�_�[�����^���ɂȂ�����v���C���[��bool��ς���
        if (_chargeSlider.value == _chargeSlider.maxValue)
        {
            _player.PillowEnemy.ReturnPillow = true;
            _chargeSlider.value = 0;
            _chargeSliderMini.SetActive(false);
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
    public void GameOver()
    {
        _gameOverUI.SetActive(true);
    }
}
