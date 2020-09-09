using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/******************************
*  Author       :  Erick
*  Version      :  0.1
*  Date         :  020/08/2020
*******************************/

public class CreateGallery : MonoBehaviour
{
    #region INITIALIZATION VARIABLES

    //SCRIPTS
    public Gallery_UI _galleryUI;

    public GalleryManager _gallerymanager;

    //UI
    public GameObject _buttonPrefab;
   
    [HideInInspector]
    public Button _delete;

    public RectTransform _contentGallery;

    //VARIABLES
    public List<GameObject> _buttonFileList = new List<GameObject>();

    public int _index;

    #endregion

    #region INITIALIZATION METHODS

    private void Start()
    {
        _galleryUI._addElement = GameObject.Find("AddElement").GetComponent<Button>();
        _galleryUI._addElement.onClick.AddListener(() => _gallerymanager.StartFileBrowser());

        _index = 0;
    }

    #endregion

    #region METHODS FOR THE GALLERY

    //Create and add the button to the gallery
    public void AddButton()
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
        _delete.onClick.AddListener(() => DeleteButton(_spamButton));

        _buttonFileList.Add(_spamButton);

        _index += 1;
    }

    //Remove the gallery button
    private void DeleteButton(GameObject button)
    {
        _gallerymanager.CheckDeletion(button.transform.GetSiblingIndex());
        _galleryUI._listCounter.text = "Files: " + _gallerymanager._mediaFiles.Count.ToString();
        _buttonFileList.Remove(button);
        Destroy(button);
        _index -= 1;
    }

    //Play button
    public void ClikButtonList(GameObject button)
    {
        Debug.Log(button.name + " index: " + button.transform.GetSiblingIndex());
    }

    #endregion
}
