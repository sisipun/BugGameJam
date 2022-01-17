using UnityEngine;
using UnityEngine.UI;

public class CellSummaryUi : MonoBehaviour
{
    [SerializeField] private Image backIcon;
    [SerializeField] private Image frontIcon;
    [SerializeField] private Text costLabel;

    public void Show(Cell cell)
    {
        gameObject.SetActive(true);
        if (cell.BackTile == null)
        {
            backIcon.gameObject.SetActive(false);
        }
        else
        {
            backIcon.gameObject.SetActive(true);
            backIcon.sprite = cell.BackTile.sprite;
        }

        if (cell.FrontTile == null)
        {
            frontIcon.gameObject.SetActive(false);
        }
        else
        {
            frontIcon.gameObject.SetActive(true);
            frontIcon.sprite = cell.FrontTile.sprite;
        }
        costLabel.text = cell.Cost.ToString();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
