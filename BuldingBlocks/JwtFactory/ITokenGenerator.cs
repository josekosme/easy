namespace JwtFactory
{
    public interface ITokenGenerator
    {
        object generate(User usuario, TokenConfigurations tokenConfigurations);
    }
}
