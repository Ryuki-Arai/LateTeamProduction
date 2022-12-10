using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class HangingScroll : MonoBehaviour
{
    [SerializeField, Tooltip("�|�����̉摜�B�v�f0���q���A�v�f1�����Ƃ�")]
    Sprite[] _scrollImages = new Sprite[2];

    [Tooltip("�v���C���[���i�[����ϐ�")]
    PlayerController _playerController = null;
    [Tooltip("�I�u�W�F�N�g��Renderer")]
    SpriteRenderer _scrollRenderer = null;
    [Tooltip("�I�u�W�F�N�g��Collider")]
    Collider2D[] _scrollCollider = null;


    private void Start()
    {
        Init();
    }
    public void Init()
    {
        _scrollRenderer = GetComponentInChildren<SpriteRenderer>();
        _scrollCollider = GetComponents<Collider2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _playerController = player;
            StartCoroutine(PlayerInCollider());
            Debug.Log("In");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _playerController = null;
            Debug.Log("Out");
        }
    }

    IEnumerator PlayerInCollider()
    {
        while(_playerController != null)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                bool isAdultMode = !_playerController.AdultState;
                if (isAdultMode)
                {
                    _scrollRenderer.sprite = _scrollImages[0];
                }
                else
                {
                    _scrollRenderer.sprite = _scrollImages[1];
                }
                _playerController.ModeChange(isAdultMode);
                yield break;
            }
            yield return null;
        }
    }
}
