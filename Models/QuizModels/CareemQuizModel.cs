namespace bike_mind_quest.Models.QuizModels
{
    public class CareemQuizModel
    {
        public string? QuestionText { get; set; }
        public List<string>? Options { get; set; } 
        public string? CorrectAnswer { get; set; }
        public List<string>? ShuffledOptions { get; set; } 

        public static class QuizQuestions
        {
            public static string? Question1 => "Which station has the highest number of available docks in the provided payload response?";
            public static string? Question2 => "Which station is identified as a charging station in this list?";
        }
    }
}
