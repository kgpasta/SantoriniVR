using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class StraightTaperedPointerRenderer : MonoBehaviour {

    private const float POINTER_LENGTH = 0.25f;
    private const float POINTER_WIDTH = 0.0032f;
    private const string PIX_GREEN_HEX = "00D355";
    private const string WHITE = "FFFFFF";

    private Color m_LineColor;
    public Color LineColor
    {
        get { return m_LineColor; }
        set {
            m_LineColor = value;
            if (m_PointerLineRenderer != null)
            {
                UpdateLineColorGradient();
            }
        }
    }

    private bool m_Visibilty = true;
    public bool Visibilty
    {
        get { return m_Visibilty; }
        set {
            m_Visibilty = value;
            if (m_PointerLineRenderer != null)
            {
                m_PointerLineRenderer.enabled = value;
            }
        }
    }

    private LineRenderer m_PointerLineRenderer = null;

    protected void Awake()
    {
        VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
    }

    private void OnEnable()
    {
        CreateTaperingLineRenderer();
    }

    void Update ()
    {
        UpdatePointerPosition();
	}

    protected void OnDestroy()
    {
        VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
    }

    private void CreateTaperingLineRenderer()
    {
        // Add LineRenderer Component
        m_PointerLineRenderer = gameObject.AddComponent<LineRenderer>();
        m_PointerLineRenderer.enabled = false;

        // Set Material and Color Gradient
        m_PointerLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        ColorUtility.TryParseHtmlString(WHITE, out m_LineColor);
        UpdateLineColorGradient();

        // Set Length and Width of Pointer
        m_PointerLineRenderer.startWidth = POINTER_WIDTH;
        m_PointerLineRenderer.endWidth = POINTER_WIDTH;

        // Turn on renderer
        m_PointerLineRenderer.enabled = true;
    }

    private void UpdateLineColorGradient()
    {
        float alphaOpaque = 1.0f;
        float alphaTransparent = 0f;

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(m_LineColor, 0.0f), new GradientColorKey(m_LineColor, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alphaOpaque, 0.0f), new GradientAlphaKey(alphaTransparent, 1.0f) }
            );

        m_PointerLineRenderer.colorGradient = gradient;
    }

    private void UpdatePointerPosition()
    {
        Vector3 parentPosition = transform.parent.position;

        Vector3[] positions = new Vector3[2];
        positions[0] = parentPosition;
        positions[1] = CalculateLineRendererEndPosition();

        m_PointerLineRenderer.positionCount = positions.Length;
        m_PointerLineRenderer.SetPositions(positions);
    }

    private Vector3 CalculateLineRendererEndPosition()
    {
        Vector3 parentPosition = transform.parent.position;
        Quaternion parentRotation = transform.parent.rotation;

        Quaternion quat = new Quaternion(0f, 0f, POINTER_LENGTH, 0f);
        quat = parentRotation * quat * Quaternion.Inverse(parentRotation);

        return parentPosition + new Vector3(quat.x, quat.y, quat.z);
    }
}
