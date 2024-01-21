using bike_mind_quest.Controllers.GBFSDataController;
using bike_mind_quest.Models.QuizModels;
using Microsoft.AspNetCore.Mvc;

namespace bike_mind_quest.Controllers.QuizController
{
    [Route("api/[controller]")]
    public class LyftQuizController
    {
        private readonly GeneralBikeshareFeedSpecificationDataController _generalBikeshareFeedSpecificationDataController;

        public LyftQuizController(GeneralBikeshareFeedSpecificationDataController generalBikeshareFeedSpecificationDataController)
        {
            _generalBikeshareFeedSpecificationDataController = generalBikeshareFeedSpecificationDataController;
        }

        [HttpGet("GenerateQuizQuestions")]
        [Produces("application/json")]
        public async Task<List<LyftQuizModel>> GenerateQuizQuestions()
        {
            try
            {
                var donkeyRepublicData = await _generalBikeshareFeedSpecificationDataController.GetLyftFreeBikeStatus();

                if (donkeyRepublicData == null)
                {
                    return new List<LyftQuizModel>();
                }

                var quizQuestions = new List<LyftQuizModel>();

                // Question 1
                var question1 = new LyftQuizModel
                {
                    QuestionText = LyftQuizModel.QuizQuestions.Question1,
                    Options = LyftQuizModel.QuizOptions.Options1,
                    CorrectAnswer = LyftQuizModel.QuizAnswers.Answer1,
                    ShuffledOptions = ShuffleOptions(LyftQuizModel.QuizOptions.Options1)
                };
                quizQuestions.Add(question1);

                // Question 2
                var question2 = new LyftQuizModel
                {
                    QuestionText = LyftQuizModel.QuizQuestions.Question2,
                    Options = LyftQuizModel.QuizOptions.Options2,
                    CorrectAnswer = LyftQuizModel.QuizAnswers.Answer2,
                    ShuffledOptions = ShuffleOptions(LyftQuizModel.QuizOptions.Options2)
                };
                quizQuestions.Add(question2);

                return quizQuestions;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading GenerateQuizQuestions endpoint: {ex.Message}");
                return new List<LyftQuizModel>();
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
