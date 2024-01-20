namespace bike_mind_quest.Models.QuizModels
{
    public class LyftQuizModel
    {
        public string? QuestionText { get; set; }
        public List<string>? Options { get; set; }
        public string? CorrectAnswer { get; set; }
        public List<string>? ShuffledOptions { get; set; }

        public static class QuizQuestions
        {
            public static string? Question1 => "Can you select the unique identifier for bikes?";
            public static string? Question2 => "Which platform's URI is provided for the last mile QR scan?";
        }

        public static class QuizOptions
        {
            public static List<string>? Options1 => new List<string> { "I don't know", "38.8meters", "c8bf697327e67711aad5fdeb0eb4984c", "21meters" };
            public static List<string>? Options2 => new List<string> { "iOS", "Android", "Both iOS and Android", "Vehicle Type ID" };
        }

        public static class QuizAnswers
        {
            public static string? Answer1 => "c8bf697327e67711aad5fdeb0eb4984c";
            public static string? Answer2 => "Both iOS and Android";
        }
    }
}
