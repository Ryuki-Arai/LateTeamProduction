using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _objList;
    [SerializeField, Header("pause�{�^���������ꂽ��True")]
    bool _isPause = false;

    /// <summary>True�̎��A���Ԃ̉��Z���~�߂�</summary>
    public bool IsPause { get => _isPause; }

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
        if (!_isPause)
        {
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
        _isPause = !_isPause;
    }
}
