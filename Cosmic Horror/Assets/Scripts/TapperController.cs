using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapperController : MonoBehaviour
{
    #region Declarations

    private Animator _animator;
    public AudioSource _audioSource;
    private int cooldown;

    #endregion

    #region Default Unity Methods

    // Use this for initialization
    void Start ()
    {
        this._animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    if (Input.GetKeyDown(KeyCode.Space) && cooldown <= 0)
	    {
	        this.Tap();
            _audioSource.Play();
            cooldown = 15;
	    }
        cooldown--;
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
