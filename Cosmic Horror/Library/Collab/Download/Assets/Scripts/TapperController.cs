using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapperController : MonoBehaviour
{
    #region Declarations

    private Animator _animator;
    private AudioSource _audioSource;

    #endregion

    #region Default Unity Methods

    // Use this for initialization
    void Start ()
    {
        this._animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	        this.Tap();
	    }
	}

    #endregion

    #region Public interface

    public void EndTap()
    {
        this._animator.SetBool("IsTapPressed", false);
    }

    #endregion

    #region Private methods

    private void Tap()
    {
        this._animator.SetBool("IsTapPressed", true);
    }

    #endregion

}
