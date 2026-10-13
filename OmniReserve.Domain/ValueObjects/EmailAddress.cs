namespace OmniReserve.Domain.ValueObjects;

public record EmailAddress
{
    public string Value { get; }

    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El email no puede estar vacío.");
        }

        if (!value.Contains("@"))
        {
            throw new ArgumentException("El email debe contener un formato válido.");
        }

        Value = value;
    }

    public override string ToString() => Value;
}
