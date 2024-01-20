using System;
using System.Net.Http;
using System.Threading.Tasks;
using bike_mind_quest.Models.BikeModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace bike_mind_quest.Controllers.GBFSDataController
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralBikeshareFeedSpecificationDataController : Controller
    {
        [HttpGet("GetCareemStationStatus")]
        public async Task<CareemStationStatusModel> GetCareemStationStatus()
        {
            try
            {
                var careemClient = new HttpClient();
                var careemRequest = new HttpRequestMessage(HttpMethod.Get, "https://dubai.publicbikesystem.net/customer/gbfs/v2/en/station_status");
                var careemResponse = await careemClient.SendAsync(careemRequest);

                if (careemResponse.IsSuccessStatusCode)
                {
                    var careemResponseContent = await careemResponse.Content.ReadAsStringAsync();
                    var careemData = JsonConvert.DeserializeObject<CareemStationStatusModel>(careemResponseContent);

                    if (careemData != null)
                    {
                        Console.WriteLine("Careem data is available!");
                        return careemData;
                    }
                }

                Console.WriteLine("Data unavailable!");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error acquiring data from 'GetCareemStationStatusModel': {ex.Message}");
                return null;
            }
        }

        [HttpGet("GetDonkeyRepublicStationInformation")]
        public async Task<ActionResult<DonkeyRepublicStationInformationModel>> GetDonkeyRepublicStationInformation()
        {
            try
            {
                var donkeyRepublicClient = new HttpClient();
                var donkeyRepublicRequest = new HttpRequestMessage(HttpMethod.Get, "https://stables.donkey.bike/api/public/gbfs/2/donkey_rotterdam_den_haag/nl/station_information.json");
                var donkeyRepublicResponse = await donkeyRepublicClient.SendAsync(donkeyRepublicRequest);

                if (donkeyRepublicResponse.IsSuccessStatusCode)
                {
                    var donkeyRepublicContent = await donkeyRepublicResponse.Content.ReadAsStringAsync();

                    if (donkeyRepublicContent != null)
                    {
                        Console.WriteLine("Donkey Republic data is available!");
                        return Ok(donkeyRepublicContent);
                    }
                    else
                    {
                        return NotFound("Data unavailable!");
                    }
                }
                else
                {
                    return StatusCode((int)donkeyRepublicResponse.StatusCode, "Request failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error acquiring data from 'GetCareemStationStatusModel': {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("GetLyftFreeBikeStatus")]
        public async Task<ActionResult<LyftFreeBikeStatusModel>> GetLyftFreeBikeStatus()
        {
            try
            {
                var lyftClient = new HttpClient();
                var lyftRequest = new HttpRequestMessage(HttpMethod.Get, "https://gbfs.lyft.com/gbfs/2.3/dca/en/free_bike_status.json");
                var lyftResponse = await lyftClient.SendAsync(lyftRequest);

                if (lyftResponse.IsSuccessStatusCode)
                {
                    var lyftContent = await lyftResponse.Content.ReadAsStringAsync();

                    if (lyftContent != null)
                    {
                        Console.WriteLine("Lyft data is available!");
                        return Ok(lyftContent);
                    }
                    else
                    {
                        return NotFound("Data unavailable!");
                    }
                }
                else
                {
                    return StatusCode((int)lyftResponse.StatusCode, "Request failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error acquiring data from 'GetCareemStationStatusModel': {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
