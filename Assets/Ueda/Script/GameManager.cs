using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField,Tooltip("�c�莞�Ԃ̏����l")] float _timeLimit = 300;
    
    [SerializeField,Tooltip("�c��̓G�i���j�̐�")] int _sleepingEnemy = 0;
    
    
    
    [SerializeField] UIManager _uIManager = default;
    PlayerController _player = default;
    

    public bool _isGame = false; 

    MapInstance _mapInstance = default;


    public int SleepingEnemy { get => _sleepingEnemy; set => _sleepingEnemy = value; }
    public PlayerController Player { get => _player; }

    /// <summary>�c�莞��</summary>
    float _time = 0;
    // Start is called before the first frame update
    void Awake()
    {
        //_mapInstance = 
        //_sleepingEnemy = _mapInstance.Entity.SleeperValue; �}�b�v�C���X�^���X���������Ă��Ȃ����ߕۗ�
        _player = FindObjectOfType<PlayerController>();
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
        
    }
    public void CheckSleepingEnemy() 
    {
        _sleepingEnemy--;
        if(_sleepingEnemy == 0)
        {
            Clear();
        }
    }
    private void Clear()
    {
        _uIManager.Clear(_timeLimit - _time);
        _isGame = false;
        //Scene�ړ��p�֐����g�p

    }
    public void GameOver()
    {
        _uIManager.GameOver();
        _isGame = false;
    }

    private void Timer()
    {
        _time += Time.deltaTime;
        _uIManager.TimerText(_timeLimit-_time);
        if (_time <= 0) GameOver();
    }
    
}
