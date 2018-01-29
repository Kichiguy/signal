using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalController : MonoBehaviour {

    #region Declarations

    public List<GameObject> Pages;
    private int pageIndex;

    #endregion

    // Use this for initialization
    void Start ()
    {
        this.pageIndex = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextPage()
    {
        this.HideAllPages();
        if (pageIndex < Pages.Count-1)
        {
            pageIndex += 1;
        } 
        this.ShowPage(pageIndex);
    }

    public void PrevPage()
    {
        this.HideAllPages();
        if (pageIndex > 0)
        {
            pageIndex -= 1;
        }
        this.ShowPage(pageIndex);
    }

    public void ShowJournal()
    {
        this.gameObject.SetActive(true);
    }

    public void HideJournal()
    {
        this.gameObject.SetActive(false);
    }

    private void HideAllPages()
    {
        foreach (var page in Pages)
        {
            page.SetActive(false);
        }
    }

    private void ShowPage(int index)
    {
        Pages[index].SetActive(true);
    }
}
