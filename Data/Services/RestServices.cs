using QuickVisualWebWood.Core.serviceModels;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace QuickVisualWebWood.Data.Services
{
	public class RestServices
	{

		public RestServices()
		{
		}

		private HttpClient Client()
		{
			HttpClientHandler clientHandler = new HttpClientHandler();
			clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
			return new HttpClient(clientHandler);
		}

		public T Get<T>(ParamsAPI obj)
		{
			HttpClient _client = Client();
			string responseBody = string.Empty;
			try
			{

				if (obj != null)
				{
					foreach (var item in obj.Header)
					{
						_client.DefaultRequestHeaders.Add(item.key, item.values);
					}
					HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, obj.Url);
					requestMessage.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, obj.ContentType);
					var response = _client.SendAsync(requestMessage).Result;
					responseBody = response.Content.ReadAsStringAsync().Result;
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			return JsonConvert.DeserializeObject<T>(responseBody);
		}

		public T Post<T>(ParamsAPI obj)
		{
			HttpClient _client = Client();
			string responseBody = string.Empty;
			try
			{
				if (obj != null)
				{
					if (obj.Header != null)
					{
						foreach (var item in obj.Header)
						{
							_client.DefaultRequestHeaders.Add(item.key, item.values);
						}
					}
					HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, obj.Url);
					var content = (!string.IsNullOrEmpty(obj.ContentType)) ? new StringContent(obj.Data, Encoding.UTF8, obj.ContentType) : new StringContent(obj.Data, Encoding.UTF8);
					requestMessage.Content = content;

					var response = _client.SendAsync(requestMessage).Result;
					responseBody = response.Content.ReadAsStringAsync().Result;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return JsonConvert.DeserializeObject<T>(responseBody);
		}

        public T Put<T>(ParamsAPI obj)
        {
            HttpClient _client = Client();
            string responseBody = string.Empty;
            try
            {
                if (obj != null)
                {
                    if (obj.Header != null)
                    {
                        foreach (var item in obj.Header)
                        {
                            _client.DefaultRequestHeaders.Add(item.key, item.values);
                        }
                    }
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, obj.Url);
                    var content = (!string.IsNullOrEmpty(obj.ContentType)) ? new StringContent(obj.Data, Encoding.UTF8, obj.ContentType) : new StringContent(obj.Data, Encoding.UTF8);
                    requestMessage.Content = content;

                    var response = _client.SendAsync(requestMessage).Result;
                    responseBody = response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.DeserializeObject<T>(responseBody);
        }

        public T Delete<T>(ParamsAPI obj)
        {
            HttpClient _client = Client();
            string responseBody = string.Empty;
            try
            {
                if (obj != null)
                {
                    if (obj.Header != null)
                    {
                        foreach (var item in obj.Header)
                        {
                            _client.DefaultRequestHeaders.Add(item.key, item.values);
                        }
                    }
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, obj.Url);
                    var content = (!string.IsNullOrEmpty(obj.ContentType)) ? new StringContent(obj.Data, Encoding.UTF8, obj.ContentType) : new StringContent(obj.Data, Encoding.UTF8);
                    requestMessage.Content = content;

                    var response = _client.SendAsync(requestMessage).Result;
                    responseBody = response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return JsonConvert.DeserializeObject<T>(responseBody);
        }

        public async Task<T> PostAsync<T>(ParamsAPI obj)
		{
			HttpClient _client = Client();
			string responseBody = string.Empty;
			try
			{
				if (obj != null)
				{
					if (obj.Header != null)
					{
						foreach (var item in obj.Header)
						{
							_client.DefaultRequestHeaders.Add(item.key, item.values);
						}
					}
					HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, obj.Url);

					var encodedContent = new FormUrlEncodedContent(obj.Data2);

					var response = await _client.PostAsync(obj.Url, encodedContent);
					responseBody = await response.Content.ReadAsStringAsync();

				}
			}
			catch (Exception ex)
			{
				throw;
			}
			return JsonConvert.DeserializeObject<T>(responseBody);
		}

		public async Task<T> PostAsyncStream<T>(ParamsAPI obj)
		{
			HttpClient _client = Client();
			string responseBody = string.Empty;
			try
			{
				if (obj != null)
				{
					if (obj.Header != null)
					{
						foreach (var item in obj.Header)
						{
							_client.DefaultRequestHeaders.Add(item.key, item.values);
						}
					}
					var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture));
					content.Add(new StreamContent(obj.DataStream));
					var response = await _client.PostAsync(obj.Url, content);




					response.EnsureSuccessStatusCode();
					responseBody = await response.Content.ReadAsStringAsync();

				}
			}
			catch (Exception ex)
			{
				throw;
			}
			return JsonConvert.DeserializeObject<T>(responseBody);
		}
	}
}
