namespace TokenRIng;

public class Token
{
    public string? SourceIp { get; set; }
    public string? DestinationIp { get; set; }
    public string? Message { get; set; }
    public bool IsFree { get; set; }
    public bool ReachedDestination { get; set; }
    
    public Token()
    {
        this.IsFree = true;
        this.ReachedDestination = false;
        this.Message = string.Empty;
    }
}