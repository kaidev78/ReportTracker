using System;
using WebAPI_Authentication.Models;

namespace WebAPI_Authentication.Authentication
{
    public interface ITokenRefresher
    {
        AuthenticationResponse refresher(RefreshCred refreshCred);
    }
}
