using System.Collections.Generic;
using System.Linq;

public class TopCharacterManager
{
    private List<Character> _characters = new List<Character>();
    private TopCharacterUI _topCharacterUI;

    public void RegisterShark(Character character)
    {
        if (!_characters.Contains(character))
        {
            _characters.Add(character);
            character.OnScoreChanged += UpdateTopSharks;
        }
    }

    public void UnregisterShark(Character character)
    {
        if (_characters.Contains(character))
        {
            _characters.Remove(character);
            character.OnScoreChanged -= UpdateTopSharks;
        }
    }

    public void SetUI(TopCharacterUI topSharksUI)
    {
        _topCharacterUI = topSharksUI;
        UpdateTopSharks();
    }

    private void UpdateTopSharks()
    {
        var sortedSharks = _characters.OrderByDescending(s => s.ScoreLevel).ToList();

        foreach (var shark in _characters)
        {
            shark.SetCrown(shark == sortedSharks.FirstOrDefault());
        }

        _topCharacterUI?.UpdateSharkList(sortedSharks);
    }
}
