namespace bike_mind_quest.Controllers.ClientAuthorizationController
{
    public class ConfirmAuthenticationController
    {
        private static Dictionary<string, string> userCredentials = new Dictionary<string, string>();

        public static bool SignUp(string username, string password)
        {
            if (!userCredentials.ContainsKey(username))
            {
                userCredentials.Add(username, password);
                return true;
            }
            return false; // User already exists
        }

        public static bool Login(string username, string password)
        {
            if (userCredentials.TryGetValue(username, out string storedPassword))
            {
                return storedPassword == password;
            }
            return false; // User not found
        }
    }
}
