using System;
using System.Linq;
using UnityEngine;
using UniRx;
using IsGame;


public class HouseOnSolt : HouseBase
{
    [Tooltip("���艖�̃I�u�W�F�N�g")]
    GameObject _soltObject;

    public override void Initialize<T>(T house)
    {
        base.Initialize(house);
        _houseType = HouseType.Solt;
        _soltObject = house.ObjectsOfHouse;
    }
    public override void PlayerEntryHouseMotion(PlayerController player)
    {
        base.PlayerEntryHouseMotion(player);
        _soltObject.SetActive(!player.AdultState);
    }
    public override void PlayerInHouseMotion(PlayerController player)
    {
        base.PlayerInHouseMotion(player);
        if (!player.AdultState)
        {
            GameManager.Instance.GameOver();
        }
    }
}
