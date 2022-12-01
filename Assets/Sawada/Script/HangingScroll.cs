using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class HangingScroll : MonoBehaviour
{
    [SerializeField, Tooltip("�|�����̉摜�B�v�f0���q���A�v�f1�����Ƃ�")]
    Sprite[] _scrollImages = new Sprite[2];
    [Tooltip("�I�u�W�F�N�g��Renderer")]
    SpriteRenderer _scrollRenderer = null;
    [Tooltip("�I�u�W�F�N�g��Collider")]
    Collider2D[] _scrollCollider = null;

    public void Init()
    {
        _scrollRenderer = GetComponent<SpriteRenderer>();
        _scrollCollider = GetComponentsInChildren<Collider2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player) && Input.GetButtonDown("Fire2"))
        {
            bool isAdultMode = !player.AdultState;
            if (isAdultMode)
            {
                _scrollRenderer.sprite = _scrollImages[0];
            }
            else
            {
                _scrollRenderer.sprite = _scrollImages[1];
            }
            player.ModeChange(isAdultMode);
        }
    }
    public void IsActive(bool active)
    {
        _scrollRenderer.enabled = active;
        foreach(Collider2D collider in _scrollCollider)
        {
            collider.enabled = active;
        }
    }
}
