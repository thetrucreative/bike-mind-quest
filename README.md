# Bike Mind Quest Documentation

Immerse yourself in the captivating world of bike-sharing through interactive quizzes with Bike Mind Quest.

## Getting Started
Begin your quest now by visiting [Bike Mind Quest](http://bike-mind-quest.com/bike-mind-quest).
Sequence diagram: [Sequence Diagram](https://sequencediagram.org/index.html?presentationMode=readOnly#initialData=C4S2BsFMAICEEkDSBRaBZeA5AItAigKrIDKAKgFDkEDOkATgLQB8xIA5gHYEAOAXNAAUA8mWgB6AIbcQkgK7AAFmOrsOs7uQkBjUADcJwGK049yxrt2YAZAPZsQHfuZ4AKOpACOsyNWABKTR0QfUNoW3sOcnCHBms7B34AYQVILQBraBAAM2hZWjpoSAAPEF9qTXBgaBp6aAATGx9oDhsq4tLgcmgw+I5YpmjHaABBOrrc-OgtdzrIDlAJcHLuweZnPmgAJUhgWToOaGpZLS0fZfrIbT0DGEHySCWYGoL2sq6eiLXVHn5t3f3oFkJCBwHtIO9ZldgjcPg57hw6pRnl8TBthKJJNI5IoxOBeoFrqF1mZvpYBr1+IM3J5vL4AlCQrd8as4hEkil0hNatNILN5iBFuVFlVEjY6O4dFMZnMFkt3qsWKTfjs9gcjiczhDLkFGbDIg9aNB4BwtGKJVUeXzZecFetlf8DkCQWCtQyYXc5oiqPlmABxCQAW0gAnAEgAnvxfchSOIpDJ-UGQ+GxMRgBI6MAE+C3aEs0mw+Q86Gw8xEunIJAA3hZCAAF6Rub0G7VuvVnygGwcaguek6mFl9yVlu18gDitVmu1v2B4PFhvAMeV1MGECd5e7bu9wkwIvhwsz-P9RcTutJctLtMdjjrvKj88nqfMZ78YfQLzt1dd6BsRt0G5e5EmF3CNoCjGNMXjA9izEX0dkwYpgDbXxPwJaFcygvdgNLe9hwbDgm0MYckKvTdUN1Y9hzvQcH2nRM51AnZj3XT8b1InMdwwgssOw6jcOgJjLxYy9bwoyd+mfaB4KKKovDrN9aSvJEfSAzjI2jWMsWAmC4IQ4iUPY0DOP3Ojw2YbBOzSSAwz42D8L-QjJz0zs2L7UJzI4SzrMnch3M84daNncN518qztm4WQACNwBALRmM7Y0shsHsyJhYDjMCkszIsqy+JCsMwsi6LYsE+KOESnzsq8utxPyfgpJkyd5I-TslPoAL8zU8C4xgzjtOAeqnMiAy0qwgYwyyRDJzwgjICIhTPxc7cwnGya6yiFb-JUkyQNg4ArBWgAxQdYBASzWOS4ajO4saJr4-aJqOisTrO4Tynu1bHyYCT6vkuT32QlqgA)

## Endpoints

### User Authentication
- **Sign Up:** `POST /api/auth/signup`
- **Log In:** `POST /api/auth/login`

### Quiz Gameplay
- **Start Game:** `GET /api/GamePlay/StartGame`
- **Get Next Question:** `GET /api/GamePlay/GetNextQuestion`
- **Submit Answer:** `POST /api/GamePlay/SubmitAnswer`
- **End Game:** `GET /api/GamePlay/EndGame`

## Usage
Explore the Bike Mind Quest website and seamlessly integrate with the API using the specified endpoints. Retrieve dynamic quiz questions for each bike-sharing service with simple GET requests to the relevant endpoints.

- **Generate Quiz Questions:** `GET CareemQuizController "/api/CareemQuiz/GenerateQuizQuestions"`
- **Generate Quiz Questions:** `GET DonkeyRepublicQuizController "/api/DonkeyRepublicQuiz/GenerateQuizQuestions"`
- **Generate Quiz Questions:** `GET LyftQuizController "/api/LyftQuiz/GenerateQuizQuestions"`

## Challenges
- The asynchronous flow of fetching data from GBFS may lead to occasional slowness in the game.

## Future Feature Requests
- Enhance logging for the user on the UI.
- Improve the authentication experience, possibly by integrating with Google.
- Integration with more services from GBFS (enhance DonkeyRepublicQuizController & LyftQuizController endpoints)

## Explore Now
Visit [Bike Mind Quest](http://bike-mind-quest.com/bike-mind-quest) and delve into the excitement of bike-sharing quizzes!
