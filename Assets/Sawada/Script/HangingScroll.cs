using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class HangingScroll : MonoBehaviour
{
    [SerializeField,Tooltip("�|�����̉摜�B�v�f0���q���A�v�f1�����Ƃ�")]
    Sprite[] _scrollImages = new Sprite[2];
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player) && Input.GetButtonDown("Diside"))
        {
            bool playerMode = !player.AdultState;
            player.ModeChange(!player.AdultState);
            if
        }
    }
}
