using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
//�|�����̃N���X(�N�����邱�ƂŎq���Ƒ�l���؂�ւ��)
public class HangingScroll : MonoBehaviour
{
    [SerializeField, Tooltip("�|�����̉摜�B�v�f0���q���A�v�f1����l")]
    Sprite[] _scrollImages = new Sprite[2];
    [SerializeField, Tooltip("")]
    ScrollText _scroolText = null; 

    [Tooltip("�v���C���[���i�[����ϐ�")]
    PlayerController _playerController = null;
    [Tooltip("�I�u�W�F�N�g��Renderer")]
    SpriteRenderer _scrollRenderer = null;

    //��X�C��
    //GameManager _gameManager = null;
    UIManager _uiManager = null;
    SoundManager _soundManager = null;
    float _waitTime = 1f;


    public void Init()
    {
        _scrollRenderer = GetComponentInChildren<SpriteRenderer>();
        //_gameManager = gameManager;
        _scroolText.TextActivate(_playerController);
        //_playerController = _gameManager.Player;

        //��X�C��
        _uiManager = FindObjectOfType<UIManager>();
        _soundManager = FindObjectOfType<SoundManager>();
    }

    IEnumerator PlayerInCollider()
    {
        while(_playerController)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                bool isAdultMode = !_playerController.AdultState;
                _uiManager.CutIn(!isAdultMode);
                _soundManager.Kakejikued();
                yield return new WaitForSeconds(_waitTime);
                _playerController.ModeChange(isAdultMode);
                if (isAdultMode)
                {
                    _scrollRenderer.sprite = _scrollImages[0];
                }
                else
                {
                    _scrollRenderer.sprite = _scrollImages[1];
                }
                yield break;
            }
            yield return null;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _playerController = player;
            _scroolText.TextActivate(_playerController);
            StartCoroutine(PlayerInCollider());
            Debug.Log("In");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject == _gameManager.Player.gameObject)
        //{
        //    _playerController = null;
        //    _scroolText.TextActivate(_playerController);
        //    Debug.Log("Out");
        //}
    }
}
