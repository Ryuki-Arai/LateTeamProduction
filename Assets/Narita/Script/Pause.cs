using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField,Header("pause���ɏ����I�u�W�F�N�g")]
    List<GameObject> _objList;
    //[SerializeField, Header("pause�{�^���������ꂽ��True")]
    //bool _isPause = false;
    [SerializeField]
    SoundManager _sound = null;
    /// <summary>True�̎��A���Ԃ̉��Z���~�߂�</summary>
    //public bool IsPause { get => _isPause; }

    private void Start()
    {
        _objList = new List<GameObject>();
        _objList.Add(GameObject.FindGameObjectWithTag("Player"));
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("NakaiEnemy");
        foreach (var enemy in enemys)
        {
            _objList.Add(enemy);
        }
    }

    public void PauseAction()
    {
        if (IsGame.GameManager.Instance._isGame)
        {
            _sound.Paused();
            foreach (var obj in _objList)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            foreach (var obj in _objList)
            {
                obj.SetActive(true);
            }
        }
        IsGame.GameManager.Instance._isGame = !IsGame.GameManager.Instance._isGame;
    }
}
