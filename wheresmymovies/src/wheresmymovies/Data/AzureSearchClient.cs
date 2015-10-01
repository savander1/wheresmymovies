using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace wheresmymovies.Data
{
    public class AzureSearchClient
    {
    	private readonly WebClient _client;
    	public AzureSearchClient()
	    {
	        _client = new WebClient();   
	    }
    }
}
