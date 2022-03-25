using Controllers;
using Identifier;
using Libraries;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    [SerializeField] private IdentifierContainer _gameID;
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private Library _library;
    [SerializeField] private SpriteLoader _spriteLoader;

    private GameController _gameController;

    private void Start()
    {
        _library.Init();
        _gameController = GameController.CreateGameController(_library, _mainCanvas, _gameID.Id);
        _gameController.StartGame();
    }
}
