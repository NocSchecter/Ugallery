using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************
*  Author       :  Erick
*  Version      :  0.1
*  Date         :  020/08/2020
*******************************/


public class GalleryManager : MonoBehaviour
{
    #region INITIALIZATION VARIABLES

    //SCRIPTS
    public Gallery_UI _galleryUI;

    public CreateGallery _createGallery;

    //VARIABLES
    public List<MediaFile> _mediaFiles = new List<MediaFile>();

    private string _currentFile;

    public string[] _fileExtension;

    private string _currenteExtension;

    [HideInInspector]
    public int _currenteExtensionValue;

    public Sprite[] _icons;

    public int _quickLinkAmount;

    public string[] _folderFiles;

    public string[] _folderPath;

    #endregion

    #region INITIALIZATION METHODS

    //Create QuickLinks
    private void QuickLinks()
    {
        for (int i = 0; i < _quickLinkAmount; i++) FileBrowser.AddQuickLink(_folderFiles[i], _folderPath[i]);
    }

    #endregion

    #region METHODS TO OPEN (INSTANCE) EXPLORER

    public void StartFileBrowser()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Fotos", ".jpg", ".png"), new FileBrowser.Filter("Videos", ".mp4"),
             new FileBrowser.Filter("Modelos", ".obj"));

        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

        QuickLinks();

        StartCoroutine(ShowLoadDialogCoroutine());
    }

    // Credits: https://github.com/yasirkula/UnitySimpleFileBrowser
    IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(false, true, null, "Load File", "Ok");

        if (FileBrowser.Success)
        {
            for (int i = 0; i < FileBrowser.Result.Length; i++)
            {
                _currentFile = FileBrowser.Result[0];
                Debug.Log(FileBrowser.Result[0]);
            }

            GetExtension(FileBrowser._currentFile, _fileExtension);

            _createGallery.AddButton();
            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);
        }

        AddFile2Gallery();
        InsertMediaFile2List(_createGallery._index - 1, FileBrowser._currentFile.Replace(_currenteExtension, string.Empty), _currenteExtension, FileBrowser._sharedPath);
    }

    #endregion

    #region METHODS TO INSERT ITEMS TO THE GALLERY

    //Create a new space in the list
    public void AddFile2Gallery()
    {
        _mediaFiles.Add(new MediaFile());
        _galleryUI._listCounter.text = "Files: " + _mediaFiles.Count.ToString();
    }

    //Insert files to base list
    public void InsertMediaFile2List(int index, string name, string extension, string path)
    {
        _mediaFiles[index]._fileName = name;
        _galleryUI._buttonText.text = _mediaFiles[index]._fileName;
        _createGallery._galleryUI._buttonIcon.sprite = _icons[_currenteExtensionValue];
        _mediaFiles[index]._fileExtension = extension;
        _mediaFiles[index]._path = path;
    }

    public void CheckDeletion(int index)
    {
        _mediaFiles.RemoveAt(index);
        if (_mediaFiles.Count == 0) _currenteExtension = "";
        Debug.Log("Element deleted: " + index);
    }

    //Gets the type of file extension
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

#region NESTED CLASS

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

