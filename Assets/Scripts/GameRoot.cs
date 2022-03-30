using Controllers;
using Identifier;
using Libraries;
using UnityEngine;
using View;

public class GameRoot : MonoBehaviour
{
    [SerializeField] private IdentifierContainer _gameID;
    [SerializeField] private MainUI _mainUI;
    [SerializeField] private Library _library;

    private GameController _gameController;

    private void Start()
    {
        _library.Init();
        _gameController = GameController.CreateInstance(_library, _mainUI, _gameID.Id);
        _gameController.StartGame();
    }
}