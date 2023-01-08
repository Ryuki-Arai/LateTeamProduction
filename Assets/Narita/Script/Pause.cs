using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField]
    GameObject[] _moveObject;
    [SerializeField, Header("pause�{�^���������ꂽ��True")]
    bool _isPause = false;
    
    public void PauseAction()
    {
        if (!_isPause)
        {
            foreach (var obj in _moveObject)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            foreach (var obj in _moveObject)
            {
                obj.SetActive(true);
            }
        }
        _isPause = !_isPause;
    }
}
