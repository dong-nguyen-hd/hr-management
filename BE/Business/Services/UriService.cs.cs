using Business.Domain.Services;

namespace Business.Services;

public class UriService : IUriService
{
    #region Property
    private readonly string _baseUri;
    #endregion

    #region Constructor
    public UriService(string baseUri)
    {
            this._baseUri = baseUri;
        }
    #endregion

    #region Method
    public Uri GetRouteUri(string route) => new Uri(string.Concat(_baseUri, route));
    #endregion
}