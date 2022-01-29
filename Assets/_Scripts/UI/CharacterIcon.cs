using System.Collections;
using System.Collections.Generic;
using _Scripts.Player;
using _Scripts.ScriptableObjects.Channels;
using UnityEngine;
using UnityEngine.UI;

public class CharacterIcon : MonoBehaviour
{
    [SerializeField] PlayerChannel _PlayerChannel;
    [SerializeField] Image _characterIconImage;
    [SerializeField] Sprite HumanImage;
    [SerializeField] Sprite BearImage;
    [SerializeField] Sprite RabbitImage;

    private Dictionary<PlayerShape, Sprite> shapeToSprite = new Dictionary<PlayerShape, Sprite>();

    private void Awake()
    {
        _PlayerChannel.PlayerChangeShapeEvent += OnChangeShape;
        shapeToSprite[PlayerShape.Human] = HumanImage;
        shapeToSprite[PlayerShape.Bear] = BearImage;
        shapeToSprite[PlayerShape.Rabbit] = RabbitImage;
    }

    public void OnChangeShape(PlayerShape shape)
    {
        _characterIconImage.sprite = shapeToSprite[shape];
    }
}
