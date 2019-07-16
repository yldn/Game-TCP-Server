public class DecisionAnswer
{
    private string planetaryId;
    private string playerId;
    private bool vote;

    public DecisionAnswer(string _planetaryId, string _playerId, bool _vote)
    {
        planetaryId = _planetaryId;
        playerId = _playerId;
        vote = _vote;
    }

    public string GetPlanetaryId()
    {
        return planetaryId;
    }

    public string GetPlayerId()
    {
        return playerId;
    }

    public bool GetVote()
    {
        return vote;
    }
}