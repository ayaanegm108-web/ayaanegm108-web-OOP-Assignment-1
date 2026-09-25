namespace Part3_BuilderPattern.Composed;
public class AddressBuilder
{
    private string? _street, _city, _state, _zip, _country;

    public AddressBuilder WithStreet(string street) { _street = street; return this; }
    public AddressBuilder WithCity(string city) { _city = city; return this; }
    public AddressBuilder WithState(string state) { _state = state; return this; }
    public AddressBuilder WithZipCode(string zipCode) { _zip = zipCode; return this; }
    public AddressBuilder WithCountry(string country) { _country = country; return this; }

    public Address Build() => new(_street ?? "", _city ?? "", _state ?? "", _zip ?? "", _country ?? "");
}
