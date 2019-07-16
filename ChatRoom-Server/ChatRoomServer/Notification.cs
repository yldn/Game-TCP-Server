public class Notification
{
    private string head = "";
    private string body = "";
    private string planetId = "";

    public Notification(string _head, string _body, string _planetId)
    {
        head = _head;
        body = _body;
        planetId = _planetId;
    }

    public string ToMessageString()
    {
        return "***notification\r\n"
        + "pid:" + planetId + "\r\n"
        + "h:" + head + "\r\n"
        + "b:" + body;
    }
}