using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*************************************************************************
*  Copyright © 2019-2020 Hypnogic. All rights reserved.
*------------------------------------------------------------------------
*  File         :  GalleryManager.cs
*  Description  :  Contiene los metodos necesarios para crear una galeria
*------------------------------------------------------------------------
*  Author       :  Erick
*  Version      :  2.0
*  Date         :  03/09/2020
*  Description  :  Servidor
*************************************************************************/

public class GalleryManager : MonoBehaviour
{
    #region VARIABLES DE INICIALIZACION

    public Gallery_UI _galleryUI;
    public CreateList _createList;

    [Tooltip("Lista base para la galeria")]
    public List<MediaFile> _mediaFiles = new List<MediaFile>();

    [Tooltip("Extensiones de archivo compatibles con la escena actual")]
    public string[] _fileExtension;

    public Sprite[] _icons;

    [Tooltip("Archivo actual seleccionado")]
    private string _currentFile;

    public string _currenteExtension;

    [HideInInspector]
    public int _currenteExtensionValue;

    [Header("ACCESOS DIRECTOS")]
    [Tooltip("Cantidad de accesos directos enla escena")]
    public int _quickLinkAmount;

    [Tooltip("Nombre del acceso directo")]
    public string[] _folderFiles;

    [Tooltip("Ruta de la carpeta del acceso directo")]
    public string[] _folderPath;

    #endregion

    #region METODOS DE INICIALIZACION

    //Creamos acceso directos para los archivos que usaremos en la respectiva escena
    private void QuickLinks()
    {
        for (int i = 0; i < _quickLinkAmount; i++) FileBrowser.AddQuickLink(_folderFiles[i], _folderPath[i]);
    }

    #endregion

    #region METODOS PARA ABRIR(INSTANCIAR) EXPLORADOR

    // TODO: [VMX-550] Abrir ventana explorador de archivos
    public void StartFileBrowser()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Fotos", ".jpg", ".png"), new FileBrowser.Filter("Videos", ".mp4"),
             new FileBrowser.Filter("Modelos", ".obj"));

        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

        QuickLinks();

        StartCoroutine(ShowLoadDialogCoroutine());
    }

    // TODO: [VMX-551] Seleccionar elemento y guardar referencia
    IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(false, true, null, "Load File", "ACEPTAR");

        if (FileBrowser.Success)
        {
            for (int i = 0; i < FileBrowser.Result.Length; i++)
            {
                _currentFile = FileBrowser.Result[0];
                Debug.Log(FileBrowser.Result[0]);
            }

            GetExtension(FileBrowser._currentFile, _fileExtension);

            _createList.AddButtonList();
            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);
        }

        AddFile2Gallery();
        InsertMediaFile2List(_createList._index - 1, FileBrowser._currentFile.Replace(_currenteExtension, string.Empty), _currenteExtension, FileBrowser._sharedPath);
    }

    #endregion

    #region METODOS PARA INSERTAR ELMENTOS A LA GALERIA

    //creamos un nuevo espacio en la lista y mandamos la instruccion por red para que el cliente cree un espacio en su propia lista
    public void AddFile2Gallery()
    {
        _mediaFiles.Add(new MediaFile());
    }

    // TODO: [VMX-552] Agregar nombre de archivo al botón
    //Metodo para insertar los archivos a la lista base
    public void InsertMediaFile2List(int index, string name, string extension, string path)
    {
        _mediaFiles[index]._fileName = name;
        _galleryUI._buttonText.text = _mediaFiles[index]._fileName;
        _createList._galleryUI._buttonIcon.sprite = _icons[_currenteExtensionValue];
        _mediaFiles[index]._fileExtension = extension;
        _mediaFiles[index]._path = path;
    }

    //Obtenermos el tipo de exrencion del archivo
    public string[] GetExtension(string currentFiile, string[] extension)
    {
        for (int i = 0; i < extension.Length; i++)
        {
            if (currentFiile.Contains(extension[i]))
            {
                _currenteExtension = extension[i];
                _currenteExtensionValue = i;
            }
        }
        return extension;
    }

    #endregion
}

#region Clase anidada

//Clase para los formatos de archivos
[System.Serializable]
public class MediaFile
{
    public string _fileName;
    public string _fileExtension;
    public string _path;
    public bool isLoaded;
}

#endregion

