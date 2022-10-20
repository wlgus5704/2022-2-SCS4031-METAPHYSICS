﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using static Struct;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    //public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();

    private UserPrivileges  m_UserPrivilege = UserPrivileges.None;
    private string          m_CurrentUserName = null;
    //임시
    private Dictionary<String, UserData> m_UserDataDict = new Dictionary<String, UserData>();

    public void Init()
    {
        //초기 실험용 데이터
        UserData UD = new UserData();
        UD.UserNum = "2017110413";
        UD.Password = "794613";
        UD.UserName = "유재헌";
        m_UserDataDict.Add("2017110413", UD);
        // StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    public Struct.UserData FindUserData(String UserNumber)
    {
        UserData UD;

        m_UserDataDict.TryGetValue(UserNumber, out UD);

        return UD;
    }

    public bool IsOverlappedUser(String UserNumber)
    {
        return m_UserDataDict.ContainsKey(UserNumber);
    }

    public bool AddUserData(UserData Data)
    {
        if (Data.UserNum.Length == 0 || Data.Password.Length == 0 || Data.UserName.Length == 0)
            return false;

        m_UserDataDict.Add(Data.UserNum, Data);

        return true;
    }

    public void SetCurrentUser(string UserName)
    {
        m_CurrentUserName = UserName;
    }

    public string GetCurrentUser()
    {
        return m_CurrentUserName;
    }

    public void SetCurrentPrivilege(UserPrivileges UserPrivilege)
    {
        m_UserPrivilege = UserPrivilege;
    }

    public UserPrivileges GetCurrentPrivilege()
    {
        return m_UserPrivilege;
    }
}
