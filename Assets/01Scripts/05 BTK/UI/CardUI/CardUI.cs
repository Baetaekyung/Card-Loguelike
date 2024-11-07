
using System.Linq.Expressions;
using UnityEngine;

public class CardUI : BaseCard
{
    public bool isOnDeck = false;

    public bool throwUnder = false;
    public bool throwGround = false;

    private void OnEnable()
    {
        OnPointerDownEvent += OnPointerDownHandler;
        isOnDeck = false;
    }

    private void OnPointerDownHandler()
    {
        CardManager cardManager = CardManager.Instance;

        isOnDeck = !isOnDeck;

        //if(isOnDeck)
            //cardManager.deckCnt++;
        //else
            //cardManager.deckCnt--;
    }

    protected override void UpdatePosition()
    {
        base.UpdatePosition();
        CheckYPos();
    }

    private void CheckYPos()
    {
        if(transform.position.y < -230f)
        {
            if(throwGround)
            {
                throwUnder = true;
            }
        }
        else if(transform.position.y > -230f)
        {
            throwGround = true;
        }

        if(throwGround && throwUnder)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        OnPointerDownEvent -= OnPointerDownHandler;
    }
}
