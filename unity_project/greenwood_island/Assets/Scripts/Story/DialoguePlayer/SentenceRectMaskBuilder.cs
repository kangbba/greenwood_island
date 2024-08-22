using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class SentenceRectMaskBuilder
{
    private Transform _rectMaskParent;
    private SentenceRectMask _sentenceRectMaskPrefab;

    public SentenceRectMaskBuilder(Transform rectMaskParent, SentenceRectMask sentenceRectMaskPrefab)
    {
        _rectMaskParent = rectMaskParent;
        _sentenceRectMaskPrefab = sentenceRectMaskPrefab;
    }

    public SentenceRectMask AddRectMask(string text, ref float xOffset, ref float yOffset, float maxWidth)
    {
        SentenceRectMask rectMask = Object.Instantiate(_sentenceRectMaskPrefab, _rectMaskParent);
        rectMask.Init(text);

        RectTransform rectTransform = rectMask.GetComponent<RectTransform>();
        float rectWidth = rectTransform.rect.width;

        if (xOffset + rectWidth > maxWidth)
        {
            xOffset = 0f;
            yOffset -= 80f;
        }

        rectMask.SetPosition(xOffset, yOffset);
        xOffset += rectWidth;

        return rectMask;
    }

    public int FindSplitIndex(string text, float availableWidth)
    {
        int splitIndex = 0;
        float width = 0f;

        for (int i = 0; i < text.Length; i++)
        {
            width = GetCharacterWidth(text.Substring(0, i + 1));
            if (width > availableWidth)
            {
                break;
            }
            splitIndex = i + 1;
        }

        return splitIndex;
    }

    public float GetCharacterWidth(string text)
    {
        SentenceRectMask tempRectMask = Object.Instantiate(_sentenceRectMaskPrefab, _rectMaskParent);
        tempRectMask.Init(text);

        RectTransform rectTransform = tempRectMask.GetComponent<RectTransform>();
        float width = rectTransform.rect.width;

        Object.Destroy(tempRectMask.gameObject);

        return width;
    }

    public List<SentenceRectMask> CreateRectMask(Line line, ref int currentMaskIndex)
    {
        currentMaskIndex = 0;
        List<SentenceRectMask> createdRectMasks = new List<SentenceRectMask>();

        string sentence = line.Sentence;
        float xOffset = 0f;
        float yOffset = 0f;
        float maxWidth = 1600f;

        string currentPart = "";
        foreach (char letter in sentence)
        {
            currentPart += letter;
            float currentWidth = GetCharacterWidth(currentPart);

            if (xOffset + currentWidth > maxWidth)
            {
                float availableWidth = maxWidth - xOffset;
                int splitIndex = FindSplitIndex(currentPart, availableWidth);
                
                string part1 = currentPart.Substring(0, splitIndex);
                SentenceRectMask rectMask1 = AddRectMask(part1, ref xOffset, ref yOffset, maxWidth);
                rectMask1.FragmentReason = SentenceRectMask.EFragmentReason.Overflow;
                createdRectMasks.Add(rectMask1);

                string part2 = currentPart.Substring(splitIndex);
                xOffset = 0f;
                yOffset -= 80f;
                currentPart = part2;
            }
            else if (new Regex(@"\.").IsMatch(letter.ToString()))
            {
                SentenceRectMask rectMask = AddRectMask(currentPart, ref xOffset, ref yOffset, maxWidth);
                rectMask.FragmentReason = SentenceRectMask.EFragmentReason.Regex;
                createdRectMasks.Add(rectMask);
                currentPart = "";
            }
        }

        if (!string.IsNullOrEmpty(currentPart))
        {
            SentenceRectMask rectMask = AddRectMask(currentPart, ref xOffset, ref yOffset, maxWidth);
            rectMask.FragmentReason = SentenceRectMask.EFragmentReason.LastFragment;
            createdRectMasks.Add(rectMask);
        }

        return createdRectMasks;
    }

    public int CalculateEndIndex(List<SentenceRectMask> createdRectMasks, int startIndex)
    {
        int endIndex = createdRectMasks.Count - 1;

        for (int i = startIndex; i < createdRectMasks.Count; i++)
        {
            if (createdRectMasks[i].FragmentReason == SentenceRectMask.EFragmentReason.Regex)
            {
                endIndex = i;
                break;
            }
        }

        return endIndex;
    }
}
