Immerse yourself in the captivating world of bike-sharing through interactive quizzes with Bike Mind Quest.

Getting Started
Begin your quest now by visiting Bike Mind Quest.

Endpoints
User Authentication
Sign Up: POST /api/auth/signup
Log In: POST /api/auth/login
Quiz Gameplay
Start Game: GET /api/GamePlay/StartGame
Get Next Question: GET /api/GamePlay/GetNextQuestion
Submit Answer: POST /api/GamePlay/SubmitAnswer
End Game: GET /api/GamePlay/EndGame
Usage
Explore the Bike Mind Quest website and seamlessly integrate with the API using the specified endpoints. Retrieve dynamic quiz questions for each bike-sharing service with simple GET requests to the relevant endpoints.

Generate Quiz Questions: GET CareemQuizController "/api/CareemQuiz/GenerateQuizQuestions"
Generate Quiz Questions: GET DonkeyRepublicQuizController "/api/DonkeyRepublicQuiz/GenerateQuizQuestions"
Generate Quiz Questions: GET LyftQuizController "/api/LyftQuiz/GenerateQuizQuestions"
Challenges
The asynchronous flow of fetching data from GBFS may lead to occasional slowness in the game.
Feature Requests
Enhance logging for the user on the UI.
Improve the authentication experience, possibly by integrating with Google.
Explore Now
Visit Bike Mind Quest and delve into the excitement of bike-sharing quizzes!
