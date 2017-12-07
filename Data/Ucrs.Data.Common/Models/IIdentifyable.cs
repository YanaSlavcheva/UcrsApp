namespace Ucrs.Data.Common.Models
{
    public interface IIdentifyable<TKey>
    {
        TKey Id { get; set; }
    }
}
