using System;
namespace TokenRIng;

public class Calculator
{
    public string? Buffer { get; set; }
    public string? IpAddress { get; set; }
    
    public Calculator(string? ipAddress)
    {
        this.IpAddress = ipAddress;
        this.Buffer = null;
    }
}