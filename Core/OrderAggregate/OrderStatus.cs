using System.Runtime.Serialization;

namespace Core.OrderAggregate
{
    
    // sayi deger dondermek yerine text donderiyoruz.
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")] Pending,

        [EnumMember(Value = "PaymentReceived")]
        PaymentReceived,
        [EnumMember(Value = "PaymentFailed")] PaymentFailed,
    }
}