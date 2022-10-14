using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Returnpillow : MonoBehaviour
{
    /// <summary>���Ԃ����s�������ǂ���</summary>
    bool returnPillow;
    Animator anim = null;
    Image image = null;
    [SerializeField]
    Slider slider = null;
    /// <summary>�����_���Ȑ��l</summary>
    float getupTime;
    /// <summary>���v</summary>
    float timer;
    public bool ReturnPillow { get => returnPillow; set => returnPillow = value; }
    // Start is called before the first frame update
    void Start()
    {
        if (!slider)
        {
            Debug.Log("slider���Z�b�g���Ă�������");
        }
        returnPillow = false;
        anim = GetComponent<Animator>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (returnPillow)
        {
            //�������蔻��͌�ŕύX�\��
            GetComponent<BoxCollider2D>().enabled = false;
            //���ς���F�͌�ŕύX�\��
            image.color = Color.black;
        }
    }
    private void LateUpdate()
    {
        anim.SetBool("bool�̖��O", returnPillow);
    }

    private void OnTriggerStay2D(Collider2D collision)//�v���C���[�������蔻��̒��ɂƂǂ܂�����
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().Pillow = true;
            timer += Time.deltaTime;
            //���l�̕��͌�ŕύX�\��
            getupTime = Random.Range(2, 5);
            if (getupTime <= timer && returnPillow)//�������Ԃ𒴂����@�{�@����Ԃ���Ă��Ȃ�������
            {

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        timer -= timer;
    }
}
