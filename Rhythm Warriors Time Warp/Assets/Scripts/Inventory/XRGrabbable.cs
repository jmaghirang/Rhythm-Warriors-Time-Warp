using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// some code from:
// https://github.com/fbsamples/oculus-networked-physics-sample/blob/main/Networked%20Physics/Assets/Oculus/VR/Scripts/Util/OVRGrabbable.cs
public class XRGrabbable : XRBaseInteractable
{
    /*
    [SerializeField]
    protected bool m_allowOffhandGrab = true;
    [SerializeField]
    protected bool m_snapPosition = false;
    [SerializeField]
    protected bool m_snapOrientation = false;
    [SerializeField]
    protected Transform m_snapOffset;
    [SerializeField]
    protected Collider[] m_grabPoints = null;

    protected bool m_grabbedKinematic = false;
    protected Collider m_grabbedCollider = null;
    protected XRBaseInteractor m_grabbedBy = null;

    public bool allowOffhandGrab
    {
        get { return m_allowOffhandGrab; }
    }

    public bool isGrabbed
    {
        get { return m_grabbedBy != null; }
    }

    public bool snapPosition
    {
        get { return m_snapPosition; }
    }

    public bool snapOrientation
    {
        get { return m_snapOrientation; }
    }

    public Transform snapOffset
    {
        get { return m_snapOffset; }
    }

    public XRBaseInteractor grabbedBy
    {
        get { return m_grabbedBy; }
    }

    public Transform grabbedTransform
    {
        get { return m_grabbedCollider.transform; }
    }

    public Rigidbody grabbedRigidbody
    {
        get { return m_grabbedCollider.attachedRigidbody; }
    }

    public Collider[] grabPoints
    {
        get { return m_grabPoints; }
    }

    protected override void Awake()
    {
        base.Awake();
        if (m_grabPoints.Length == 0)
        {
            Collider collider = this.GetComponent<Collider>();
            if (collider == null)
            {
                throw new ArgumentException("Grabbables cannot have zero grab points and no collider -- please add a grab point or collider.");
            }
            m_grabPoints = new Collider[1] { collider };
        }
    }

    protected virtual void Start()
    {
        m_grabbedKinematic = GetComponent<Rigidbody>().isKinematic;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (m_grabbedBy != null)
        {
            m_grabbedBy.ForceDeselect();
        }
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        m_grabbedBy = args.interactor;
        m_grabbedCollider = args.interactor.GetComponent<Collider>();
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = m_grabbedKinematic;
        rb.velocity = args.interactor.attachTransform.position - transform.position;
        rb.angularVelocity = args.interactor.attachTransform.rotation.eulerAngles - transform.rotation.eulerAngles;
        m_grabbedBy = null;
        m_grabbedCollider = null;
    }
    */

    // different code:
    /*
    [SerializeField] protected bool m_allowOffhandGrab = true;
    [SerializeField] protected bool m_snapPosition = false;
    [SerializeField] protected bool m_snapOrientation = false;
    [SerializeField] protected Transform m_snapOffset;

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);
        GrabBegin(interactor);
    }

    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);
        GrabEnd(interactor.velocity, interactor.angularVelocity);
    }

    virtual protected void GrabBegin(XRBaseInteractor interactor)
    {
        m_grabbedBy = interactor;
        m_grabbedCollider = interactor.GetComponent<Collider>();
        GetComponent<Rigidbody>().isKinematic = true;

        // added for inventory
        if (gameObject.GetComponent<ItemInventory>() == null) return;
        if (gameObject.GetComponent<ItemInventory>().inSlot)
        {
            gameObject.GetComponentInParent<Slot>().ItemInSlot = null;
            gameObject.transform.parent = null;
            gameObject.GetComponent<Item>().inSlot = false;
            gameObject.GetComponent<Item>().currentSlot.ResetColor();
            gameObject.GetComponent<Item>().currentSlot = null;
        }
    }

    virtual protected void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = m_grabbedKinematic;
        rb.velocity = linearVelocity;
        rb.angularVelocity = angularVelocity;
        m_grabbedBy = null;
        m_grabbedCollider = null;
    }
    */
}