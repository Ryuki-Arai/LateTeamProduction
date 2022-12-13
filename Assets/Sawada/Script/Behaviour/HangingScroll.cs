using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
public class HangingScroll : MonoBehaviour
{
    [SerializeField, Tooltip("�|�����̉摜�B�v�f0���q���A�v�f1����l")]
    Sprite[] _scrollImages = new Sprite[2];

    [Tooltip("�v���C���[���i�[����ϐ�")]
    PlayerController _playerController = null;
    [Tooltip("�I�u�W�F�N�g��Renderer")]
    SpriteRenderer _scrollRenderer = null;

    

    //��X�C��
    ScrollText _scrollText = null;
    GameManager _gameManager = null;
    UIManager _uiManager = null;
    float _waitTime = 1f;


    public void Init(GameManager gameManager)
    {
        _scrollRenderer = GetComponentInChildren<SpriteRenderer>();
        _scrollText = GetComponentInChildren<ScrollText>();
        _scrollText.TextActivate(_playerController);
        _gameManager = gameManager;
        _uiManager = FindObjectOfType<UIManager>();//��X�C��
    }

    IEnumerator PlayerInCollider()
    {
        while(_playerController)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                bool isAdultMode = !_playerController.AdultState;
                _uiManager.CutIn(!isAdultMode);
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
            _scrollText.TextActivate(_playerController);
            StartCoroutine(PlayerInCollider());
            Debug.Log("In");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _gameManager.Player.gameObject)
        {
            _playerController = null;
            _scrollText.TextActivate(_playerController);
            Debug.Log("Out");
        }
    }
}
