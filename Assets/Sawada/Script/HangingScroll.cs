using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[RequireComponent(typeof(CircleCollider2D))]
public class HangingScroll : MonoBehaviour
{
    [SerializeField, Tooltip("�|�����̉摜�B�v�f0���q���A�v�f1����l")]
    Sprite[] _scrollImages = new Sprite[2];

    [Tooltip("�v���C���[���i�[����ϐ�")]
    PlayerController _playerController = null;
    [Tooltip("�I�u�W�F�N�g��Renderer")]
    SpriteRenderer _scrollRenderer = null;
    [Tooltip("�v���C���[�̑�l�q���̔����BoolReactiveProperty�ɕϊ����ĕۑ����Ă���(���̂����͐������Ȃ��̂ŒǁX�C������)")]
    BoolReactiveProperty _adultState = null;

    GameManager _gm;


    public void Init(GameManager gameManager)
    {
        _scrollRenderer = GetComponentInChildren<SpriteRenderer>();
        _gm = gameManager;
        _playerController = _gm.Player;
        _adultState = new BoolReactiveProperty(_playerController.AdultState);
        RendererChange();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _gm.Player.gameObject)
        {
            StartCoroutine(PlayerInCollider(_playerController));
            Debug.Log("In");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _gm.Player.gameObject)
        {
            Debug.Log("Out");
        }
    }

    IEnumerator PlayerInCollider(PlayerController player)
    {
        while(true)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                bool isAdultMode = !player.AdultState;
                player.ModeChange(isAdultMode);
                yield break;
            }
            yield return null;
        }
    }

    void RendererChange()
    {
        _adultState
            .Subscribe(x =>
            {
                if (x)
                {
                    _scrollRenderer.sprite = _scrollImages[0];
                }
                else
                {
                    _scrollRenderer.sprite = _scrollImages[1];
                }
            });
    }
}
