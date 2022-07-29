namespace CarRentalSystem.Domain.Models.CarAds;

using Common;

public class TransmissionType : Enumeration
{
    public static readonly TransmissionType Manual = new(1, nameof(Manual));
    public static readonly TransmissionType Automatic = new(2, nameof(Automatic));

    private TransmissionType(int value)
        : base(value, FromValue<TransmissionType>(value).Name)
    {
    }

    private TransmissionType(int value, string name)
        : base(value, name)
    {
    }
}