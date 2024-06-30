namespace Telephony;

public class SmartPhone : ICaller, IBrowser
{
    public string Call(string phoneNumber) =>   $"Calling... {phoneNumber}";
    public string Browse(string website) => $"Browsing: {website}!";
}