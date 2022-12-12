using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField,Tooltip("�c�莞�Ԃ̏����l")] float _timeLimit = 300;
    
    [SerializeField,Tooltip("�c��̓G�i���j�̐�")] int _sleepingEnemy = 0;
    
    [SerializeField,Tooltip("�Q�[���I�[�o�[���ɕ\������UI")] GameObject _gameOverUI = null;
    
    [SerializeField] UIManager _uIManager = default;
    PlayerController _player = default;
    public int SleepingEnemy { get => _sleepingEnemy; set => _sleepingEnemy = value; }
    public PlayerController Player { get => _player; }

    public bool _isGame = false; 

    MapInstance _mapInstance = default;


    /// <summary>�c�莞��</summary>
    float _time = 0;
    // Start is called before the first frame update
    void Start()
    {
        //_mapInstance = 
        //_sleepingEnemy = _mapInstance.Entity.SleeperValue;
        _player = FindObjectOfType<PlayerController>();
        _gameOverUI.SetActive(false);
        GameStart();
    }

    void GameStart()
    {
        _isGame = true;
    }


    // Update is called once per frame
    void Update()
    {
        Timer();
        if(_sleepingEnemy == 0)
        {
            Clear();
        }
    }

    private void Clear()
    {
        _uIManager.Clear(_timeLimit - _time);
        //Scene�ړ��p�֐����g�p

    }

    private void Timer()
    {
        _time += Time.deltaTime;
        _uIManager.TimerText(_timeLimit-_time);
    }
    public void GameOver()
    {
        _gameOverUI.SetActive(true);
        _isGame = false;
    }
}
