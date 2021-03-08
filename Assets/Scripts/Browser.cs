using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/******************************
*  Author       :  Erick
*  Version      :  0.1
*  Date         :  07/03/2021
*  Credits      :  https://github.com/Hello-Meow
*******************************/

public class Browser : MonoBehaviour
{
    #region INITIALIZATION VARIABLES

    public event Action<string> FileSelected;

    public List<string> _extensions;

    public GameObject _listItemPrefab;

    public GameObject _driveCanvas;
    public GameObject _fileCanvas;
    public GameObject _pathInputFiled;

    public string[] _drives;
    public string _currentDirectory;
    public bool _selectDrive;

    public List<string> _directories;

    public List<string> _files;
    public string _currentFile;

    #endregion

    #region INITIALIZATION METHODS

    private void Awake()
    {
        _directories = new List<string>();
        _files = new List<string>();

        _drives = Directory.GetLogicalDrives();

        _selectDrive = (string.IsNullOrEmpty(_currentDirectory) || !Directory.Exists(_currentDirectory));

        Build();
    }

    #endregion

    #region BUILD EXPLORER

    //It is in charge of building the file structure
    private void Build()
    {
        _directories.Clear();
        _files.Clear();

        if (_selectDrive)
        {
            _directories.AddRange(_drives);
            StopAllCoroutines();
            StartCoroutine(RefreshDirectories());
            return;
        }

        try
        {
            _directories.AddRange(Directory.GetDirectories(_currentDirectory));

            foreach (string file in Directory.GetFiles(_currentDirectory))
            {
                if (_extensions.Contains(Path.GetExtension(file)))
                {
                    _files.Add(file);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }

        StopAllCoroutines();
        StartCoroutine(RefreshFiles());
        StartCoroutine(RefreshDirectories());

        //EventSystem.current.SetSelectedGameObject();
    }

    private void ClearContent()
    {
        Button[] children = _fileCanvas.GetComponentsInChildren<Button>();

        foreach (Button child in children)
        {
            Destroy(child.gameObject);
        }

        children = _driveCanvas.GetComponentsInChildren<Button>();

        foreach (Button child in children)
        {
            Destroy(child.gameObject);
        }
    }

    #endregion

    #region DIRECTORIES

    private void AddDirectories(int index)
    {
        GameObject directories = Instantiate(_listItemPrefab);

        Button button = directories.GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            OnDirectorySelected(index);
        });

        if (_selectDrive)
            directories.GetComponentInChildren<Text>().text = _directories[index];
        else
            directories.GetComponentInChildren<Text>().text = Path.GetFileName(_directories[index]);

        directories.transform.SetParent(_driveCanvas.transform, false);
    }

    private void OnDirectorySelected(int index)
    {
        if (_selectDrive)
        {
            _currentDirectory = _drives[index];
            _selectDrive = false;
        }
        else
            _currentDirectory = _directories[index];

        ClearContent();
        Build();
    }

    private IEnumerator RefreshDirectories()
    {
        for (int i = 0; i < _directories.Count; i++)
        {
            AddDirectories(i);
            yield return null;
        }
    }

    #endregion

    #region FILES

    private void AddFileItem(int index)
    {
        GameObject files = Instantiate(_listItemPrefab);

        Button button = files.GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            OnFileSelected(index);
        });

        files.GetComponentInChildren<Text>().text = Path.GetFileName(_files[index]);

        files.transform.SetParent(_fileCanvas.transform, false);
    }

    private void OnFileSelected(int index)
    {
        string path = _files[index];

        if (FileSelected != null)
        {
            FileSelected.Invoke(path);
        }

        _currentFile = Path.GetFileName(_files[index]);
    }

    private IEnumerator RefreshFiles()
    {
        for (int i = 0; i < _files.Count; i++)
        {
            AddFileItem(i);
            yield return null;
        }
    }

    #endregion
}
