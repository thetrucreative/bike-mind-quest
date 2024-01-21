# Immerse yourself in the world of bike-sharing with interactive quizzes.

Begin your quest now: [http://bike-mind-quest.com/bike-mind-quest](http://bike-mind-quest.com/bike-mind-quest)

# Endpoints

## User Authentication
### Sign Up: POST /api/auth/signup
### Log In: POST /api/auth/login

## Quiz Gameplay
### Start Game: GET /api/GamePlay/StartGame
### Get Next Question: GET /api/GamePlay/GetNextQuestion
### Submit Answer: POST /api/GamePlay/SubmitAnswer
### End Game: GET /api/GamePlay/EndGame

# Usage
- You can xxplore the Bike Mind Quest website and integrate with the API through the specified endpoints.
- Retrieve dynamic quiz questions for each bike-sharing service with a simple GET request to the relevant endpoint.

### Generate Quiz Questions: GET CareemQuizController "/api/CareemQuiz/GenerateQuizQuestions"
### Generate Quiz Questions: GET DonkeyRepublicQuizController "/api/DonkeyRepublicQuiz/GenerateQuizQuestions"
### Generate Quiz Questions: GET LyftQuizController "/api/LyftQuiz/GenerateQuizQuestions"

# Challenges
- The asynchronous flow of fetching data from GBFS makes the game slow at times.

# Feature requests
- Enhance logging for the user on the UI
- Enhance Auth experience - possibly integrate with Google

[http://bike-mind-quest.com/bike-mind-quest](http://bike-mind-quest.com/bike-mind-quest)
