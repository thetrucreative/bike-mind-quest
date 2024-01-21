using bike_mind_quest.Controllers.GBFSDataController;
using bike_mind_quest.Models.BikeModels;
using bike_mind_quest.Models.QuizModels;
using Microsoft.AspNetCore.Mvc;

namespace bike_mind_quest.Controllers.QuizController
{
    [Route("api/[controller]")]
    public class CareemQuizController : ControllerBase
    {
        private readonly GeneralBikeshareFeedSpecificationDataController _dataController;

        public CareemQuizController(GeneralBikeshareFeedSpecificationDataController dataController)
        {
            _dataController = dataController;
        }

        [HttpGet("GenerateQuizQuestions")]
        public async Task<List<CareemQuizModel>> GenerateQuizQuestions()
        {
            try
            {
                var careemDataActionResult = await _dataController.GetCareemStationStatusModel();
                Console.WriteLine($"careemDataActionResult: {careemDataActionResult}");
                if (careemDataActionResult == null || careemDataActionResult.Result is not OkObjectResult)
                {
                    Console.WriteLine("CareemDataActionResult is not OK or is null.");
                    return new List<CareemQuizModel>();
                }
                // Extracting CareemStationStatusModel from the ActionResult
                var careemData = (careemDataActionResult.Result as OkObjectResult)?.Value as CareemStationStatusModel;
                if (careemData == null)
                {
                    Console.WriteLine("careemData is null.");
                    return new List<CareemQuizModel>();
                }
                var quizQuestions = new List<CareemQuizModel>();
                Console.WriteLine($"quizQuestions: {quizQuestions}");
                // Question 1
                var question1 = new CareemQuizModel
                {
                    QuestionText = CareemQuizModel.QuizQuestions.Question1,
                    CorrectAnswer = GetStationWithHighestDocks(careemData, new List<string>())
                };
                PopulateOptions(question1, careemData);
                quizQuestions.Add(question1);

                // Question 2
                var question2 = new CareemQuizModel
                {
                    QuestionText = CareemQuizModel.QuizQuestions.Question2,
                    CorrectAnswer = GetChargingStationAnswer(careemData, new List<string>())
                };
                PopulateOptions(question2, careemData);
                quizQuestions.Add(question2);

                return quizQuestions;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading GenerateQuizQuestions endpoint: {ex.Message}");
                return new List<CareemQuizModel>();
            }
        }

        private void PopulateOptions(CareemQuizModel question, CareemStationStatusModel careemData)
        {
            var allStations = careemData?.Data?.Stations;

            if (allStations != null && allStations.Length >= 4)
            {
                var random = new Random();
                var shuffledStations = allStations.OrderBy(x => random.Next()).ToList();
                question.Options = shuffledStations.Take(4).Select(station => $"Station {station.StationId}").ToList();
                question.ShuffledOptions = question.Options.ToList();

                // Assigning correct answers based on question type
                if (question.QuestionText.Contains("highest number of available docks"))
                {
                    question.CorrectAnswer = GetStationWithHighestDocks(careemData, question.ShuffledOptions);
                }
                else
                {
                    question.CorrectAnswer = GetChargingStationAnswer(careemData, question.ShuffledOptions);
                }
            }
            else
            {
                question.Options = new List<string>();
                question.ShuffledOptions = new List<string>();
                question.CorrectAnswer = "No station found";
            }
        }

        private string GetStationWithHighestDocks(CareemStationStatusModel careemData, List<string> shuffledOptions)
        {
            int maxDocks = 0;
            string stationWithMaxDocks = "";

            foreach (var option in shuffledOptions)
            {
                var stationId = option.Split(' ')[1]; // Extract station id from the option
                var station = careemData.Data?.Stations?.FirstOrDefault(s => s.StationId == stationId);

                if (station != null && station.NumDocksAvailable > maxDocks)
                {
                    maxDocks = station.NumDocksAvailable;
                    stationWithMaxDocks = option;
                }
            }

            return string.IsNullOrEmpty(stationWithMaxDocks) ? "No station found" : stationWithMaxDocks;
        }

        private string GetChargingStationAnswer(CareemStationStatusModel careemData, List<string> shuffledOptions)
        {
            var nonChargingStations = careemData.Data?.Stations?
                .Where(station => station.IsChargingStation == false)
                .Select(station => $"Station {station.StationId}")
                .ToList();

            var optionsWithoutChargingStation = shuffledOptions.Except(nonChargingStations).ToList();

            if (optionsWithoutChargingStation.Count > 0)
            {
                var random = new Random();
                return optionsWithoutChargingStation[random.Next(optionsWithoutChargingStation.Count)];
            }

            return "No charging station found";
        }
    }
}
