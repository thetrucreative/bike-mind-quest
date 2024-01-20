namespace bike_mind_quest.Models.QuizModels
{
    public class DonkeyRepublicQuizModel
    {
        public string? QuestionText { get; set; }
        public List<string>? Options { get; set; }
        public string? CorrectAnswer { get; set; }
        public List<string>? ShuffledOptions { get; set; }

        public static class QuizQuestions
        {
            public static string? Question1 => "What does the \"is_virtual_station\" field indicate in the payload?";
            public static string? Question2 => "What information is stored in the \"rental_uris\" field?";
        }

        public static class QuizOptions
        {
            public static List<string>? Options1 => new List<string> { "The station's name", "The station's region", "Whether the station is virtual", "The station's capacity" };
            public static List<string>? Options2 => new List<string> { "Bike rental capacity", "Station's location", "Deep links for renting bikes", "Last update timestamp" };
        }

        public static class QuizAnswers
        {
            public static string? Answer1 => "Whether the station is virtual";
            public static string? Answer2 => "Deep links for renting bikes";
        }
    }
}
