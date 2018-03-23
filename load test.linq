<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

string url = "https://loadtestbaltomsdn.azurewebsites.net/api/HttpTriggerCSharp1?value=";
int requestCount = 10;

void Main()
{
    var requests = new List<HttpRequestMessage>();
    var random = new Random();
    
    for (int i = 0; i < requestCount; i++)
    {
        requests.Add(new HttpRequestMessage
        {
            RequestUri = new Uri($"{url}{random.Next()}"),
            Method = HttpMethod.Get
        });
    }
    Parallel.ForEach(requests, async x => (await ExecuteWebRequest(x)).Dump());
}

// Define other methods and classes here
async Task<string> ExecuteWebRequest(HttpRequestMessage request)
{
    var httpClient = new HttpClient();
    var response = await httpClient.SendAsync(request);
    return await response.Content.ReadAsStringAsync();
}