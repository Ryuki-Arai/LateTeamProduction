using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    /// <summary>タメ時間を表すスライダー</summary>
    [SerializeField] Slider _chargeSlider = null;
    /// <summary>枕の上に表示するスライダー</summary>
    [SerializeField] GameObject _chargeSliderMini = null;
    /// <summary>残り時間を表示するテキスト</summary>
    [SerializeField] TextMeshProUGUI _timerText = null;
    /// <summary>クリア時に表示するUI</summary>
    [SerializeField] GameObject _clearUI = null;
    /// <summary>クリアタイムを表示するテキスト</summary>
    [SerializeField] TextMeshProUGUI _clearTimeText = null;
    /// <summary>ゲームオーバー時に表示するテキスト</summary>
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

        //スライダーが満タンになったらプレイヤーのboolを変える
        if (_chargeSlider.value == _chargeSlider.maxValue)
        {
            _player.InformationReset();
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
