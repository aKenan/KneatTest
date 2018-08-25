using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Services;
using App.Models.StarshipModels;

namespace App.Test
{
    public class Tests
    {
        readonly string APIURL;
        StarshipService _starshipService;
        public Tests()
        {
            APIURL = GetApiUrl(Path.GetFullPath(@"..\..\..\..\App")); // gets api url from APP console application
            _starshipService = new StarshipService(APIURL);
        }

        [Fact]
        public void CheckIfApiUrlExists()
        {
            Assert.False(string.IsNullOrEmpty(APIURL));
        }

        [Fact]
        public void CheckIfApiUrlIsSWAPI()
        {
            Assert.True(APIURL.ToLower().Equals("https://swapi.co/api/"));
        }

        [Fact]
        public void CheckGetAllStarshipsIsOk()
        {
            try
            {
                var starships = _starshipService.GetAllStarships().Result;
                Assert.NotNull(starships);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Fact]
        public void CheckGetAllStarshipsHasData()
        {
            try
            {
                var starships = _starshipService.GetAllStarships().Result;
                Assert.NotEmpty(starships);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Fact]
        public void CheckStarshipDetailedMapping()
        {
            try
            {
                var starships = _starshipService.GetAllStarships().Result;
                var starshipsDetailed = starships.ToListStarshipDetailedModel();
                Assert.NotEmpty(starshipsDetailed);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region config

        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }

        public static string GetApiUrl(string outputPath)
        {
            var iConfig = GetIConfigurationRoot(outputPath);

            var apiUrlSection = iConfig.GetSection("API:URL");
            if (apiUrlSection != null)
                return apiUrlSection.Value;
            else
                return null;
        }

        #endregion

    }
}
