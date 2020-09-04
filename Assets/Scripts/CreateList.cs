using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*************************************************************************
*  Copyright © 2019-2020 Hypnogic. All rights reserved.
*------------------------------------------------------------------------
*  File         :  CreateList.cs
*  Description  :  Crea los botones en la galeria y manda a llamar
*                   el explorador de archivos
*------------------------------------------------------------------------
*  Author       :  Erick
*  Version      :  2.0
*  Date         :  03/09/2020
*  Description  :  Servidor
*************************************************************************/

public class CreateList : MonoBehaviour
{
    #region VARIABLES DE INCIALIZACION
    public Gallery_UI _galleryUI;
    public GalleryManager _gallerymanager;

    //Referencia del boton que se instanciara en la galeria
    public GameObject _buttonPrefab;
   
    //Boton a eliminar
    [HideInInspector]
    public Button _delete;

    //Area donde se emparentaran los botones dentro de la galeria
    public RectTransform _contentGallery;

    //Lista donde se alamacenaran los botones que se vayan creando
    public List<GameObject> _buttonFileList = new List<GameObject>();

    //ID de boton
    public int _index;

    #endregion

    #region METODOS DE INICIALIZACION

    private void Start()
    {
        _galleryUI._addElement = GameObject.Find("AddElement").GetComponent<Button>();
        _galleryUI._addElement.onClick.AddListener(() => _gallerymanager.StartFileBrowser());
        _index = 0;
    }

    #endregion

    #region METODOS PARA AGREGAR BOTONES A LA GALERIA

    // TODO: [VMX-545] Establecer función agregar botón
    public void AddButtonList()
    {
        int tempIndex = _index;

        GameObject _spamButton = Instantiate(_buttonPrefab, _contentGallery, false);
        _spamButton.name = "Button" + "_" + (_index);
        Button _btnPlay = _spamButton.GetComponent<Button>();
        _btnPlay.onClick.AddListener(() => ClikButtonList(_spamButton));

        _galleryUI._buttonText = _spamButton.GetComponentInChildren<TextMeshProUGUI>();
        _galleryUI._buttonText.text = "Button_" + _index.ToString();
        _galleryUI._buttonIcon = _spamButton.GetComponentInChildren<Image>();

        _delete = GameObject.Find("ButtonPrefabDelete").GetComponent<Button>();
        _delete.name = "Delete" + "_" + (_index);
        _delete.onClick.AddListener(() => DeleteButtonList(_spamButton));

        _buttonFileList.Add(_spamButton);

        _index += 1;
    }

    public void ClikButtonList(GameObject button)
    {
        Debug.Log(button.name + " index:" + button.transform.GetSiblingIndex());
    }

    #endregion

    #region METODOS PARA ELIMINAR LOS BOTONES DE LA GALERIA

    // TODO: [VMX-547] Crear función eliminar
    public void DeleteButtonList(GameObject button)
    {
        _buttonFileList.Remove(button);
        Destroy(button);
        _index -= 1;
    }

    #endregion
}
