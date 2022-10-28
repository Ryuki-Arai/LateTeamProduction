using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Returnpillow : MonoBehaviour
{
    /// <summary>���Ԃ����s�������ǂ���</summary>
    [SerializeField,Header("����Ԃ��ꂽ���ǂ���")]
    bool returnPillow;
    //Image image = null;
    SpriteRenderer image = null;
    /// <summary>�N���鎞��</summary>
    [SerializeField,Header("�N���鎞�ԁi��j")]
    float getupTime = 0f;
    /// <summary>player����l���q�����ŕω�����</summary>
    [SerializeField,Header("�v���C���[�̏�Ԃŕω����鎞��")]
    float _timeInPlayerStats = 0f;
    /// <summary>�Ԃ�V�������ꍇ�g�p����</summary>
    [SerializeField, Header("�����̒��ɐԂ�V�������ꍇ�ω����鎞��")]
    float _timeInBaby = 0f;
    /// <summary>���v</summary>
    float timer;
    Animator anim = null;
    public bool ReturnPillow { get => returnPillow; set => returnPillow = value; }
    // Start is called before the first frame update
    void Start()
    {

        returnPillow = false;
        anim = GetComponent<Animator>();
        //image = GetComponent<Image>();
        image = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (returnPillow)
        {
            //�������蔻��͌�ŕύX�\��
            GetComponent<CircleCollider2D>().enabled = false;
            //���ς���F�͌�ŕύX�\��
            if (!image)
            {
                Debug.LogError("image������܂���");
            }
            else
            {
                image.color = Color.black;
            }
        }
    }
    private void LateUpdate()
    {
        if (anim)
            anim.SetBool("bool�̖��O", returnPillow);
    }

    private void OnTriggerStay2D(Collider2D collision)//�v���C���[�������蔻��̒��ɂƂǂ܂�����
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().Pillow = true;
            timer += Time.deltaTime;
            //���l�̕��͌�ŕύX�\��

            if (getupTime <= timer && returnPillow)//�������Ԃ𒴂����@�{�@����Ԃ���Ă��Ȃ�������
            {

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        timer -= timer;
    }

    private void GetUpTime()
    {

    }
}