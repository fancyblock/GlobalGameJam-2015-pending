﻿using UnityEngine;
using System.Collections;

public class UIMgr : MonoBehaviour 
{
    static protected UIMgr m_instance;

    /// <summary>
    /// initial 
    /// </summary>
	void Awake() 
	{
        m_instance = this;
    }

    /// <summary>
    /// return singleton of this class
    /// </summary>
    static public UIMgr SharedInstance
    {
        get
        {
            return m_instance;
        }
    }

    /// <summary>
    /// function part 
    /// </summary>

    public Empire m_empire;

    // UI stuff 
    public UILabel m_txtAge;
    public UILabel m_txtMoney;
    public UILabel m_txtProvinceInfo;
    public UILabel m_txtNextMoney;
    public UIProgressBar m_yearProgressBar;
    public UIButton m_btnPayTribute;
    public UIButton m_btnNextYear;

    public GameObject m_dialogMask;
    // action dlgs 
    public ProvinceDlg m_provinceDlg;
    public LandDlg m_landDlg;
    public TributeDlg m_tributeDlg;
    public EventDlg m_eventDlg;

    /// <summary>
    /// start 
    /// </summary>
    void Start()
    {
        m_landDlg.m_callback = onDlgClosed;
        m_provinceDlg.m_callback = onDlgClosed;
        m_tributeDlg.m_callback = onDlgClosed;
        m_eventDlg .m_callback = onDlgClosed;
    }

    /// <summary>
    /// refresh UI
    /// </summary>
    public void RefreshUI()
    {
        int income = 0;
        foreach (Province p in m_empire.m_provinces)
        {
            income += p.GetIncome();
        }

        m_txtAge.text = "公元" + m_empire.m_age + "年";
        m_txtMoney.text = "国库：" + m_empire.m_money + "金币";
        m_txtProvinceInfo.text = "行省数：" + m_empire.m_provinces.Count;
        m_txtNextMoney.text = "明年收入：" + income;

        if (m_empire.m_status == GameEnums.GAME_STATUS_READY)
        {
            //TODO 
        }
        else if (m_empire.m_status == GameEnums.GAME_STATUS_RUNNING)
        {
            //TODO 
        }
        else if (m_empire.m_status == GameEnums.GAME_STATUS_WAITTING)
        {
            //TODO 
        }
    }

    /// <summary>
    /// refresh progress 
    /// </summary>
    public void RefreshProgress()
    {
        float progress = m_empire.m_timer / m_empire.m_yearTime;

        if (progress > 1.0f)
        {
            progress = 1.0f;
        }

        m_yearProgressBar.value = progress;
    }

    /// <summary>
    /// show province dlg 
    /// </summary>
    /// <param name="province"></param>
    public void ShowProvinceDlg( Province province )
    {
        m_dialogMask.SetActive(true);
        m_empire.Pause();

        if( province.m_conquered )
        {
            m_provinceDlg.Show(province);
        }
        else
        {
            m_landDlg.Show(province);
        }
    }

    /// <summary>
    /// show event dialog 
    /// </summary>
    /// <param name="evt"></param>
    public void ShowEventDlg( TheEvent evt )
    {
        m_dialogMask.SetActive(true);
        m_empire.Pause();

        m_eventDlg.Show(evt);
    }

    /// <summary>
    /// show tribute dialog 
    /// </summary>
    public void ShowTributeDlg()
    {
        Debug.Log("[UIMgr]: PayTribute");

        m_dialogMask.SetActive(true);
        m_empire.Pause();

        m_tributeDlg.Show();
    }

    /// <summary>
    /// callback when dialog closed 
    /// </summary>
    protected void onDlgClosed()
    {
        Debug.Log("[UIMgr]: onDlgClosed");

        m_eventDlg.gameObject.SetActive(false);
        m_landDlg.gameObject.SetActive(false);
        m_provinceDlg.gameObject.SetActive(false);
        m_tributeDlg.gameObject.SetActive(false);

        m_dialogMask.SetActive(false);
        m_empire.Resume();
    }

}
