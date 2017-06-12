using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Nancy;
using Nancy.Owin;
using System.Collections.Generic;
using System;
using wheresmymovies.Data;

namespace wheresmymovies
{
    public static class Config
    {
        public static string IInfoUrl => "http://www.omdbapi.com/";

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            { 
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        public static DefaultFilesOptions GetDefaultFileOptions()
        {
            return new DefaultFilesOptions
            {
                DefaultFileNames = new List<string>
                {
                    "where.html"
                }
            };
        }
    }
}
