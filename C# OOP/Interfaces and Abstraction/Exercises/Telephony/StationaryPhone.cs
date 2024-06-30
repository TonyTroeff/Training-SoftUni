namespace Telephony;

public class StationaryPhone : ICaller
{
    public string Call(string phoneNumber) => $"Dialing... {phoneNumber}";
}