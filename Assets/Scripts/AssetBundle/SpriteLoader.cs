using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.Networking;

public class SpriteLoader : MonoBehaviour
{
    private const string UrlAssetBundleSprites = "https://picsum.photos/184/184";

    private Texture2D _texture;
    private Queue<ICardView> _viewQueue = new();

    public void LoadRandomSprite(ICardView view)
    {
        _viewQueue.Enqueue(view);
        StartCoroutine(DownloadAndSetSpriteBundle());
    }

    private IEnumerator DownloadAndSetSpriteBundle()
    {
        yield return GetSpritesAssetBundle();

        var sprite = ConvertToSprite(_texture);

        var view = _viewQueue.Dequeue();
        view.SetPortrait(sprite);

        yield return null;
    }

    private Sprite ConvertToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(new Vector2(), new Vector2(184, 184)), new Vector2(0.5f, 0.5f));
    }

    private IEnumerator GetSpritesAssetBundle()
    {
        var request = UnityWebRequestTexture.GetTexture(UrlAssetBundleSprites);

        yield return request.SendWebRequest();

        while (!request.isDone)
            yield return null;

        StateRequest(request, ref _texture);
    }

    private void StateRequest(UnityWebRequest request, ref Texture2D assetBundle)
    {
        if (request.error == null)
        {
            assetBundle = DownloadHandlerTexture.GetContent(request);
        }
        else
        {
            Debug.LogError(request.error);
        }
    }
}