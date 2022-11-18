using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HouseBase : MonoBehaviour
{
    //[Tooltip("掛け軸が存在するか")]
    //bool _
    [Tooltip("GameManagerを格納する変数")]
    GameManager _gameManager = null;
    [Tooltip("家の中にいる枕の数")]
    protected Returnpillow[] _returnPillows = new Returnpillow[3];
    [Tooltip("家の種類")]
    protected HouseType _type = HouseType.None;

    public HouseType Type => _type;

    public virtual void Init() { }
    public virtual void PlayerEntryHouseMotion(PlayerController player) { }
    public virtual void PlayerInHouseMotion(PlayerController player) { }
    public virtual void PlayerExitHouseMotion(PlayerController player) { }

    public virtual void OnEnable()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _returnPillows = GetComponentsInChildren<Returnpillow>();
        //_returnPillows.Select(x => x.SetGetUpTime(_getUpTime));
        Init();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            PlayerEntryHouseMotion(player);
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {

            PlayerInHouseMotion(player);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {

            PlayerExitHouseMotion(player);
        }
    }
}
public enum HouseType
{
    None = 0,
    Baby = 1,
    Solt = 2,
    DevilArrow = 3
}

