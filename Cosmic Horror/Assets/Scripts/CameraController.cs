using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject JournalHandle;
    public Transform DefaultPosition;
    public Transform TapperPosition;
    public float speed;
    private JournalController _journalControllerScriptHandle;

    private enum Positions
    {
        Default,
        Tapper,
        Binder
    };

    private Positions CurrentPosition;
    private int cooldown;
    private float actualSpeed;

    // Use this for initialization
    void Awake()
    {
        CurrentPosition = Positions.Default;
        transform.position = DefaultPosition.position;
        transform.rotation = DefaultPosition.rotation;
    }

    private void Start()
    {
        if (JournalHandle != null)
        {
            _journalControllerScriptHandle = JournalHandle.GetComponent<JournalController>();
        }
    }

    // Update is called once per frames
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CurrentPosition == Positions.Tapper && cooldown <= 0)
            {
                cooldown = 10;
                actualSpeed = speed * Time.deltaTime;
                transform.position = Vector3.Lerp(DefaultPosition.position, DefaultPosition.position, actualSpeed);
                transform.rotation = DefaultPosition.rotation;
                CurrentPosition = Positions.Default;
            }

            else if (CurrentPosition == Positions.Binder && cooldown <= 0)
            {
                CastRayFromBinder();

            }

            else if (CurrentPosition == Positions.Default && cooldown <= 0)
            {
                CastRayFromDefault();
            }
        }

        cooldown--;
    }

    void CastRayFromDefault()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "Binder")
            {
                CurrentPosition = Positions.Binder;
                if (_journalControllerScriptHandle != null)
                {
                    _journalControllerScriptHandle.ShowJournal();
                }
                return;
            }

            if (hit.collider.gameObject.name == "Tapper")
            {
                cooldown = 10;
                actualSpeed = 1 * Time.deltaTime;
                transform.position = Vector3.Lerp(TapperPosition.position, TapperPosition.position, actualSpeed);
                transform.rotation = TapperPosition.rotation;
                CurrentPosition = Positions.Tapper;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && CurrentPosition == Positions.Tapper && cooldown <= 0)
            {
                cooldown = 10;
                actualSpeed = speed * Time.deltaTime;
                transform.position = Vector3.Lerp(DefaultPosition.position, DefaultPosition.position, actualSpeed);
                transform.rotation = DefaultPosition.rotation;
                CurrentPosition = Positions.Default;
            }
        }
    }

    void CastRayFromBinder()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name == "ArrowLeft")
            {
                if (_journalControllerScriptHandle != null)
                {
                    _journalControllerScriptHandle.PrevPage(); 
                }
            }

            else if (hit.collider.gameObject.name == "ArrowRight")
            {
                if (_journalControllerScriptHandle != null)
                {
                    _journalControllerScriptHandle.NextPage();
                }

            }
            else
            {
                if (_journalControllerScriptHandle != null)
                {
                    _journalControllerScriptHandle.HideJournal();
                }

                CurrentPosition = Positions.Default;
            }
        }

        return;
    }
}
