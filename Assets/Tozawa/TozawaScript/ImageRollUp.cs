using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageRollUp : MonoBehaviour
{
    [SerializeField,Header("�؂�ւ������摜�������ɓ���Ă�������")]
    Sprite[] _asobikata;
    [SerializeField]
    Image _thisImage;
    int _indexNum = 0;
    public void ChangeImage()
    {
        _indexNum++;
        _thisImage.sprite = _asobikata[_indexNum % _asobikata.Length];
    }
}
