using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/******************************
*  Author       :  Erick
*  Version      :  0.1
*  Date         :  07/03/2021
*******************************/

public class Browser : MonoBehaviour
{

    public GameObject _listItemPrefab;

    public GameObject _driveCanvas;
    public GameObject _folderCanvas;
    public GameObject _pathInputFiled;

    public string _currentDirectory;
    public string[] _drives;
    public List<string> _directories;
    public GameObject[] _elements;
    public bool _selectDrive;

    private void Awake()
    {
        _directories = new List<string>();

        _drives = Directory.GetLogicalDrives();
        Debug.Log("There are: " + _drives.Length + " storage media");

        _selectDrive = (string.IsNullOrEmpty(_currentDirectory) || !Directory.Exists(_currentDirectory));

        BuiltDrives();
    }

    //It is in charge of building the file structure
    private void BuiltDrives()
    {
        if (_selectDrive)
        {
            _directories.AddRange(_drives);
            StartCoroutine(refreshDirectories());
            return;
        }
    }

    private IEnumerator refreshDirectories()
    {
        for (int i = 0; i < _directories.Count; i++)
        {
            AddDirectories(i);
            yield return null;
        }
    }

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
    }
}
