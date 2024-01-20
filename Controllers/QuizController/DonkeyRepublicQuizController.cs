using bike_mind_quest.Controllers.GBFSDataController;
using bike_mind_quest.Models.BikeModels;
using bike_mind_quest.Models.QuizModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bike_mind_quest.Controllers.QuizController
{
    [Route("api/[controller]")]
    public class DonkeyRepublicQuizController : ControllerBase
    {
        private readonly GeneralBikeshareFeedSpecificationDataController _generalBikeshareFeedSpecificationDataController;

        public DonkeyRepublicQuizController(GeneralBikeshareFeedSpecificationDataController generalBikeshareFeedSpecificationDataController)
        {
            _generalBikeshareFeedSpecificationDataController = generalBikeshareFeedSpecificationDataController;
        }

        [HttpGet("GenerateQuizQuestions")]
        [Produces("application/json")]
        public async Task<List<DonkeyRepublicQuizModel>> GenerateQuizQuestions()
        {
            try
            {
                var donkeyRepublicData = await _generalBikeshareFeedSpecificationDataController.GetDonkeyRepublicStationInformation();

                if (donkeyRepublicData == null)
                {
                    return new List<DonkeyRepublicQuizModel>();
                }

                var quizQuestions = new List<DonkeyRepublicQuizModel>();

                // Question 1
                var question1 = new DonkeyRepublicQuizModel
                {
                    QuestionText = DonkeyRepublicQuizModel.QuizQuestions.Question1,
                    Options = DonkeyRepublicQuizModel.QuizOptions.Options1,
                    CorrectAnswer = DonkeyRepublicQuizModel.QuizAnswers.Answer1,
                    ShuffledOptions = ShuffleOptions(DonkeyRepublicQuizModel.QuizOptions.Options1)
                };
                quizQuestions.Add(question1);

                // Question 2
                var question2 = new DonkeyRepublicQuizModel
                {
                    QuestionText = DonkeyRepublicQuizModel.QuizQuestions.Question2,
                    Options = DonkeyRepublicQuizModel.QuizOptions.Options2,
                    CorrectAnswer = DonkeyRepublicQuizModel.QuizAnswers.Answer2,
                    ShuffledOptions = ShuffleOptions(DonkeyRepublicQuizModel.QuizOptions.Options2)
                };
                quizQuestions.Add(question2);

                return quizQuestions;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading GenerateQuizQuestions endpoint: {ex.Message}");
                return new List<DonkeyRepublicQuizModel>();
            }
        }

        private static List<string> ShuffleOptions(List<string>? options)
        {
            if (options == null)
            {
                return new List<string>();
            }

            Random random = new Random();
            List<string> shuffledOptions = options.OrderBy(x => random.Next()).ToList();
            return shuffledOptions;
        }
    }
}
