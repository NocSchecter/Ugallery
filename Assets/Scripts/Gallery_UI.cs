using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gallery_UI : MonoBehaviour
{
    [SerializeField]
    public GameObject _buttonPrefab;

    [HideInInspector]
    public TextMeshProUGUI _buttonText;

    public Image _buttonIcon;

    [HideInInspector]
    public SVGImage _iconState;

    [HideInInspector]
    public SVGImage _hoverSelect;

    [HideInInspector]
    public SVGImage _hoverSelectPlay;

    [HideInInspector]
    public Button _delete;

    [HideInInspector]
    public TextMeshProUGUI _tagAdd;
    [HideInInspector]
    public Button _addElement;

    public RectTransform _contentGallery;

    [HideInInspector]
    public List<GameObject> _buttonFileList = new List<GameObject>();

    [HideInInspector]
    public List<SVGImage> _iconPlay = new List<SVGImage>();

    [HideInInspector]
    public List<SVGImage> _listHoverSelect = new List<SVGImage>();
    [HideInInspector]
    public List<SVGImage> _listHoverSelectPlay = new List<SVGImage>();
}
