using CustomTypes;

public class Player
{
    private Client client;

    public Player()
    {

    }

    /*
     * Returns NULL of there is no client connect yet or the player is offline
     */
    public Client GetClient()
    {
        return client;
    }

    public void SetClient(Client _client)
    {
        client = _client;
    }

    public Vector2 GetCoordinates()
    {
        return client.GetCoordinates();
    }
}