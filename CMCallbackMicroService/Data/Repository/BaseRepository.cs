using AccountManagementMicroService.Helpers;
using CMCallbackMicroService.Domain.Interface;
using CMCallbackMicroService.Domain.Model.Response;

namespace CMCallbackMicroService.Data.Repository
{
    public class BaseRepository : IBaseRepository
    {
        public async Task<T> SendAsync<T>(string request, string URL)
        {
            try
            {
                StringContent req = new StringContent(request, null, "text/xml");
                var client = new HttpClient();

                var requestHeader = new HttpRequestMessage(HttpMethod.Post, URL) { Version = new Version(2, 0) };

                requestHeader.Content = req;
                var response = await client.SendAsync(requestHeader);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var resp = Utilities.XmlToObject<CPSResponse>(responseContent);

                return resp;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
