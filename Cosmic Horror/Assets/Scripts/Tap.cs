using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour {

    #region Declarations

    private Renderer _renderer;
    public Boolean Tapped = false;
    public int Index;

    #endregion

    #region Default Unity Methods

    // Use this for initialization
    void Start ()
    {
        this._renderer = GetComponent<Renderer>();
        _renderer.material.color = Color.white;
    }
	
	// Update is called once per frame
	void Update ()
	{
	}

    #endregion

    #region Events

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "TapperCollider" && this.name != "LastTap")
        {
            if (this.name == "FirstTap")
            {
                TapSeriesController.StartMovement();
            }

            this.TapIt();
        }

        if (collider.gameObject.tag == "Ticker")
        {
            if (this.name == "LastTap")
            {
                TapSeriesController.EndMovement();
                return;
            }

            if (TapSeriesController.IsIndexEmpty(this.Index))
            {
                TapSeriesController.Add(this);
            }
        }
    }

    #endregion

    #region Public interface

    public void TapIt()
    {
        this._renderer.material.color = Color.black;
        this.Tapped = true;
    }

    public void UntapIt()
    {
        this._renderer.material.color = Color.white;
        this.Tapped = false;
    }

    #endregion


}
