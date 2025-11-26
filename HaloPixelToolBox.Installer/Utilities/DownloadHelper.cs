using System.Net;
using System.Net.Http;

namespace HaloPixelToolBox.Installer.Utilities
{
    public static class DownloadHelper
    {
        /// <summary>
        /// 获取服务器响应
        /// </summary>
        /// <param name="postBody">响应体</param>
        /// <returns></returns>
        public static async Task<(string, HttpStatusCode)> GetServerResponse(string postBody, string requestAddress)
        {
            using var client = new HttpClient();
            var result = await client.PostAsync(requestAddress, new StringContent(postBody));
            try
            {
                return (await result.Content.ReadAsStringAsync(), result.StatusCode);
            }
            catch (Exception ex)
            {
                return (ex.Message, result.StatusCode);
            }
        }
    }
}
