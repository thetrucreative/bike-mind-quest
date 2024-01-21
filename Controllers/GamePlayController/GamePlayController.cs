using Microsoft.AspNetCore.Mvc;
using bike_mind_quest.Controllers.QuizController;
using bike_mind_quest.Models.GameStateServiceModel;
using bike_mind_quest.Models.QuizModels;

namespace bike_mind_quest.Controllers.GamePlayController
{
    [ApiController]
    [Route("api/GamePlay")]
    public class GamePlayController : ControllerBase
    {
        private List<CareemQuizModel> careemQuizQuestions;
        private List<DonkeyRepublicQuizModel> donkeyRepublicQuizQuestions;
        private List<LyftQuizModel> lyftQuizQuestions;

        private readonly CareemQuizController _careemQuizController;
        private readonly DonkeyRepublicQuizController _donkeyRepublicQuizController;
        private readonly LyftQuizController _lyftQuizController;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GameStateService _gameStateService;

        private int currentQuestionIndex = 0;
        private int points = 0;
        private CancellationTokenSource _cancellationTokenSource;
        private int adjustedIndex = 0;
        private int totalQuestions;

        public GamePlayController(
            CareemQuizController careemQuizController,
            DonkeyRepublicQuizController donkeyRepublicQuizController,
            LyftQuizController lyftQuizController,
            IHttpContextAccessor httpContextAccessor,
            GameStateService gameStateService)
        {
            _careemQuizController = careemQuizController;
            _donkeyRepublicQuizController = donkeyRepublicQuizController;
            _lyftQuizController = lyftQuizController;
            _httpContextAccessor = httpContextAccessor;
            _gameStateService = gameStateService;

            InitializeQuestions().Wait();
        }

        private async Task InitializeQuestions()
        {
            careemQuizQuestions = await _careemQuizController.GenerateQuizQuestions();
            donkeyRepublicQuizQuestions = await _donkeyRepublicQuizController.GenerateQuizQuestions();
            lyftQuizQuestions = await _lyftQuizController.GenerateQuizQuestions();
            totalQuestions = careemQuizQuestions.Count + donkeyRepublicQuizQuestions.Count + lyftQuizQuestions.Count;
        }

        [HttpGet("StartGame")]
        public async Task<IActionResult> StartGame()
        {
            try
            {
                await InitializeQuestions();

                if (careemQuizQuestions.Count == 0 || donkeyRepublicQuizQuestions.Count == 0 || lyftQuizQuestions.Count == 0)
                {
                    return BadRequest(new { Message = "No questions available. Unable to start the game." });
                }

                _gameStateService.CurrentQuestionIndex = 0;
                points = 0;
                adjustedIndex = 0;
                _cancellationTokenSource = new CancellationTokenSource();
                await Task.Delay(TimeSpan.FromMinutes(1), _cancellationTokenSource.Token);

                return Ok(new { Message = "Game started successfully." });
            }
            catch (TaskCanceledException)
            {
                return BadRequest(new { Message = "Game unable to start" });
            }
        }

        [HttpGet("GetNextQuestion")]
        public IActionResult GetNextQuestion()
        {
            try
            {
                if (careemQuizQuestions != null && donkeyRepublicQuizQuestions != null && lyftQuizQuestions != null)
                {
                    totalQuestions = careemQuizQuestions.Count + donkeyRepublicQuizQuestions.Count + lyftQuizQuestions.Count;
                    Console.WriteLine($"Entering GetNextQuestion - Current Question Index: {_gameStateService.CurrentQuestionIndex}");

                    if (_gameStateService.CurrentQuestionIndex < totalQuestions)
                    {
                        var nextQuestion = GetNextQuestionByModelIndex();

                        return Ok(new
                        {
                            nextQuestion.QuestionText,
                            nextQuestion.Options,
                            nextQuestion.CorrectAnswer,
                            nextQuestion.ShuffledOptions,
                        });
                    }
                    else
                    {
                        //return BadRequest(new { Message = "No more questions available." });
                        _gameStateService.CurrentQuestionIndex = 0;//reset index as i have few questions

                        var nextQuestion = GetNextQuestionByModelIndex();

                        return Ok(new
                        {
                            nextQuestion.QuestionText,
                            nextQuestion.Options,
                            nextQuestion.CorrectAnswer,
                            nextQuestion.ShuffledOptions,
                        });
                    }
                }
                else
                {
                    return BadRequest(new { Message = "Questions from one or more models are not available." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error getting the next question: {ex.Message}" });
            }
        }

        private dynamic GetNextQuestionByModelIndex()
        {
            int totalQuestions = careemQuizQuestions.Count + donkeyRepublicQuizQuestions.Count + lyftQuizQuestions.Count;
            Console.WriteLine($"GetNextQuestionByModelIndex START - Current Question Index: {_gameStateService.CurrentQuestionIndex}");

            if (_gameStateService.CurrentQuestionIndex < totalQuestions)
            {
                dynamic nextQuestion;

                if (_gameStateService.CurrentQuestionIndex < careemQuizQuestions.Count)
                {
                    nextQuestion = careemQuizQuestions[_gameStateService.CurrentQuestionIndex];
                    Console.WriteLine($"Selected from Careem Quiz: {nextQuestion.QuestionText}");
                }
                else if (_gameStateService.CurrentQuestionIndex < careemQuizQuestions.Count + donkeyRepublicQuizQuestions.Count)
                {
                    nextQuestion = donkeyRepublicQuizQuestions[_gameStateService.CurrentQuestionIndex - careemQuizQuestions.Count];
                    Console.WriteLine($"Selected from Donkey Republic Quiz: {nextQuestion.QuestionText}");
                }
                else if (_gameStateService.CurrentQuestionIndex < totalQuestions)
                {
                    nextQuestion = lyftQuizQuestions[_gameStateService.CurrentQuestionIndex - careemQuizQuestions.Count - donkeyRepublicQuizQuestions.Count];
                    Console.WriteLine($"Selected from Lyft Quiz: {nextQuestion.QuestionText}");
                }
                else
                {
                    return BadRequest(new { Message = "No more questions available." });
                }

                _gameStateService.CurrentQuestionIndex++;
                Console.WriteLine($"GetNextQuestionByModelIndex END - Current Question Index: {_gameStateService.CurrentQuestionIndex}");
                Console.WriteLine($"Exiting GetNextQuestionByModelIndex");

                return nextQuestion;
            }
            else
            {
                return BadRequest(new { Message = "No more questions available." });
            }
        }
    }
}
