﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Boo.Lang.Environments;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class TapSeriesController : MonoBehaviour
{
    #region Declarations
    public static List<Tap> TapSeries;
    private static Animator _animator;

    #endregion

    #region Default Unity Methods

    // Use this for initialization
    void Start ()
    {
        _animator = GetComponent<Animator>();
        TapSeries = new List<Tap>();
    }
	
	// Update is called once per frame
	void Update ()
	{
		
	}

    #endregion

    #region Public interface

    public static Boolean IsIndexEmpty(int index)
    {
        if (TapSeries == null)
        {
            return true;
        }

        try
        {
            if (TapSeries[index] == null)
            {
                return true;
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            return true;
        }
        return false;
    }

    public static void Add(Tap tap)
    {
       TapSeries.Add(tap);   
    }

    public static void SetBoolAtIndex(int index, Boolean state)
    {
        if (TapSeries[index] != null)
        {
            TapSeries[index].Tapped = state;
        }
    }

    public static void StartMovement()
    {
        _animator.SetBool("moveTapSeries", true);
    }

    public static void EndMovement()
    {
        if (TapSeries == null)
        {
            return;
        }
        _animator.SetBool("moveTapSeries", false);
        foreach (var tap in TapSeries)
        {
            Debug.Log(tap.Index + ": " + tap.Tapped);
            tap.UntapIt();
        }

       
    }

    #endregion
}
