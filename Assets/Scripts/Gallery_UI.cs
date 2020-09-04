using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/******************************
*  Author       :  Erick
*  Version      :  0.1
*  Date         :  020/08/2020
*******************************/

public class Gallery_UI : MonoBehaviour
{
    #region INITIALIZATION VARIABLES

    //Prefab button components
    [SerializeField]
    public GameObject _buttonPrefab;

    [HideInInspector]
    public TextMeshProUGUI _buttonText;

    public Image _buttonIcon;

    [HideInInspector]
    public Button _addElement;

    #endregion
}
