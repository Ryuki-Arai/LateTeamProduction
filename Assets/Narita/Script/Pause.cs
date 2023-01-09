using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField]
    GameObject[] _moveObject;
    [SerializeField, Header("pauseボタンが押されたらTrue")]
    bool _isPause = false;

    /// <summary>Trueの時、時間の加算を止める</summary>
    public bool IsPause { get => _isPause; }
    
  
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
