using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IsGame
{
    public class GameManager
    {
        static GameManager _instance = new GameManager();

        PlayerController _player = default;

        [Tooltip("�c�莞�Ԃ̏����l")] float _timeLimit = 300;

        [Tooltip("�c��̓G�i���j�̐�")] int _sleepingEnemy = 9;



        UIManager _uIManager = default;


        public bool _isGame = false;

        MapInstance _mapInstance = default;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("�C���X�^���X������܂���B");
                }
                return _instance;
            }
        }

        public void PlayerSet(PlayerController player)
        {
            _player = player;
        }

        public void UIManagerSet(UIManager uIManager)
        {
            _uIManager = uIManager;
        }

        public int SleepingEnemy { get => _sleepingEnemy; set => _sleepingEnemy = value; }
        public PlayerController Player { get => _player;}


        /// <summary>�c�莞��</summary>
        float _time = 0;
        // Start is called before the first frame update
        void GameStart()
        {
            _isGame = true;
        }
        
        public void CheckSleepingEnemy()
        {
            _sleepingEnemy--;
            if (_sleepingEnemy == 0)
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

        public void Timer()
        {
            _time += Time.deltaTime;
            _uIManager.TimerText(_timeLimit - _time);
            if (_time >= _timeLimit) GameOver();
        }

    }
}

