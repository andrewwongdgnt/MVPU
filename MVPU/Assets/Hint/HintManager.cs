using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class HintManager : MonoBehaviour
{
    private const int HEIGHT_DEFAULT = 145;
    private const int HEIGHT_WITH_PAGE_INDICATOR = 191;

    public Text content;
    public Text pageIndicator;
    public Button next;
    public Text nextText;
    public Button prev;
    public Text prevText;
    public Text instructionsText;

    private bool isActive;
    private List<List<string>> entireContent;
    private int currentPage;
    private List<LevelUtil.WalkthroughDirection> _walkthroughDirections;

    //intentionally public static
    public static bool adShown;

    void Start()
    {
        if (entireContent == null)
        {
            isActive = false;
            gameObject.SetActive(isActive);
        }
    }

    public void DisplayWalkthrough(List<LevelUtil.WalkthroughDirection> walkthroughDirections)
    {
        isActive = !isActive;
        gameObject.SetActive(isActive);
        _walkthroughDirections = walkthroughDirections;
        if (adShown)
        {
            DisplayWalkthroughContent();
        }
        else
        {
            ShowAdWarning();
        }

    }

    private void DisplayWalkthroughContent()
    {
        Action action = () =>
        {


            entireContent = _walkthroughDirections
            .Select((value, index) => new { Value = value, Index = index })
            .GroupBy(i => i.Index / 10)
            .Select(list => list.Select(x =>
            {
                var wt = x.Value;
                switch (wt)
                {
                    case LevelUtil.WalkthroughDirection.LEFT:
                        return "↙";
                    case LevelUtil.WalkthroughDirection.RIGHT:
                        return "↗";
                    case LevelUtil.WalkthroughDirection.UP:
                        return "↖";
                    case LevelUtil.WalkthroughDirection.DOWN:
                        return "↘";
                    case LevelUtil.WalkthroughDirection.STATIONARY:
                        return "◉";
                }
                return "";
            }).ToList()).ToList();

            HandleContent(currentPage);
        };

        AdUtil.WatchAdWithCondition(action, !adShown);
        adShown = true;
    }

    private void HandleContent(int page)
    {
        if (page < 0 || page >= entireContent.Count)
            return;

        currentPage = page;

        content.text = string.Join("", entireContent[currentPage].ToArray());

        pageIndicator.text = (currentPage + 1) + "/" + entireContent.Count;

        prev.interactable = currentPage > 0;
        next.interactable = currentPage < entireContent.Count - 1;

        prev.gameObject.SetActive(entireContent.Count > 1);
        next.gameObject.SetActive(entireContent.Count > 1);
        pageIndicator.gameObject.SetActive(entireContent.Count > 1);

        prevText.text = "Prev";
        nextText.text = "Next";

        instructionsText.text = "Follow the directions from beginning to end to beat the entire level. Press the help button again to close.";

        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(0, entireContent.Count == 1 ? HEIGHT_DEFAULT : HEIGHT_WITH_PAGE_INDICATOR);

    }

    private void ShowAdWarning()
    {

        pageIndicator.gameObject.SetActive(false);
        prevText.text = "No";
        nextText.text = "Yes";
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(0,  HEIGHT_DEFAULT );

        instructionsText.text = "";
        content.text = "Requires watching an ad. You won't be asked again until you beat or leave the level.";

    }

    public void Next()
    {
        if (adShown)
            HandleContent(currentPage + 1);
        else
            DisplayWalkthroughContent();
    }

    public void Prev()
    {
        if (adShown)
        HandleContent(currentPage - 1);
        else
        {
            isActive = false;
            gameObject.SetActive(isActive);
        }
    }

}
