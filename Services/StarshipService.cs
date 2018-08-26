using System;
using System.Collections.Generic;
using System.Text;
using App.Models.StarshipModels;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using App.Models.SWAPIModels;
using App.Services.Helpers;

namespace Services
{
    public class StarshipService
    {
        private readonly string _apiUrl;
        private Helper _helper;
        const string getAllStarshipsURL = "starships";

        public StarshipService(string apiUrl)
        {
            if (string.IsNullOrEmpty(apiUrl))
                throw new Exception("API URL is empty");

            _apiUrl = apiUrl;
            _helper = new Helper();
        }

        /// <summary>
        /// Gets all starships from API
        /// </summary>
        /// <returns></returns>
        public async Task<List<StarshipModel>> GetAllStarships()
        {
            List<StarshipModel> returnModel = new List<StarshipModel>();
            var apiModel = new GetAllStarshipsModel();
            try
            {
                while(returnModel.Count < apiModel.Count || apiModel.Count == 0) //if first itteration or return model is not full
                {
                    var url = string.IsNullOrEmpty(apiModel.Next) ? string.Concat(_apiUrl, getAllStarshipsURL) : apiModel.Next; //basic address or next page address
                    var data = _helper.GetJsonString(url).Result; //http response message

                    if (data.IsSuccessStatusCode)
                    {
                        var json = await data.Content.ReadAsStringAsync();//convert response to json string
                        apiModel = JsonConvert.DeserializeObject<GetAllStarshipsModel>(json); //map json string to model

                        if (apiModel.Count == 0)
                            break;

                        returnModel.AddRange(apiModel.Starships); //add starships to return model

                        if (string.IsNullOrEmpty(apiModel.Next)) //if last page break while loop
                            break;
                    }
                    else
                    {
                        throw new Exception(_helper.GetApiUnsuccessfullMessage(data)); //if error, throw exception
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return returnModel;
        }
    }
}
