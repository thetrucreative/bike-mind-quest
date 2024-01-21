
const signUpBtn = document.getElementById('signUpBtn');
const loginBtn = document.getElementById('loginBtn');
document.addEventListener('DOMContentLoaded', function () {
    const quizContainer = document.getElementById('quiz-container');
    const questionText = document.getElementById('question-text');
    const optionsList = document.getElementById('options-list');
    const nextBtn = document.getElementById('next-btn');
    const resultContainer = document.getElementById('result-container');
    const resultText = document.getElementById('result-text');
    const finalPoints = document.getElementById('final-points');
    const restartBtn = document.getElementById('restart-btn');
    const timer = document.getElementById('timer');
    const authContainer = document.getElementById('auth-container');
    const storedGameState = sessionStorage.getItem('quizGameState');

    let currentQuestionIndex = 0;
    let points = 0;
    let timerValue = 60;
    let timerInterval;
    let quizQuestions = [];
    let isLoggedIn = false;

    //nextBtn.disabled = true;
    //nextBtn.disabled == true;
    quizContainer.style.display = 'none';
    resultContainer.style.display = 'none';

    if (storedGameState) {
        // Restore game state if available
        const parsedGameState = JSON.parse(storedGameState);
        currentQuestionIndex = parsedGameState.currentQuestionIndex;
        points = parsedGameState.points;
        timerValue = parsedGameState.timerValue;
        quizQuestions = parsedGameState.quizQuestions;
        updateTimerDisplay();
        updatePointsDisplay();
        nextBtn.addEventListener('click', handleNextQuestion);
        timerInterval = setInterval(updateTimer, 1000);
        getNextQuestion();
    } else {
        startGame();
    }

    signUpBtn.addEventListener('click', initiateSignUp);
    loginBtn.addEventListener('click', initiateLogin);

    function initiateSignUp() {
        const username = prompt('Enter your username:');
        const password = prompt('Enter your password:');
        signUpUser(username, password);
    }

    function initiateLogin() {
        const username = prompt('Enter your username:');
        const password = prompt('Enter your password:');
        loginUser(username, password);
    }

    function signUpUser(username, password) {
        fetch('/api/auth/signup', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ Username: username, Password: password }),
        })
            .then(response => response.json())
            .then(data => {
                console.log('Signup response:', data);
                alert(data.Message);
            })
            .catch(error => console.error(error));

        authContainer.style.display = 'none';
        quizContainer.style.display = 'block';
        //startGame();
    }

    function loginUser(username, password) {
        fetch('/api/auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ Username: username, Password: password }),
        })
            .then(response => response.json())
            .then(data => {
                alert(data.Message);
                isLoggedIn = true;
                signUpBtn.disabled = true;
                loginBtn.disabled = true;
                startGame();
                //getNextQuestion();
            })
            .catch(error => console.error(error));

        authContainer.style.display = 'none';
        quizContainer.style.display = 'block';   
        //getNextQuestion();
    }

    function startGame() {
        if (isLoggedIn) {  
            clearInterval(timerInterval);
            //getNextQuestion();
            nextBtn.disabled = false;
            currentQuestionIndex = 0;
            points = 0;
            timerValue = 60;
            resultContainer.style.display = 'none';
            quizContainer.style.display = 'block';
            updateTimerDisplay();
            quizQuestions = [];
            nextBtn.removeEventListener('click', handleNextQuestion);
            nextBtn.addEventListener('click', handleNextQuestion);
            timerInterval = setInterval(updateTimer, 1000);
        }
    }

    function handleNextQuestion() {
        clearInterval(timerInterval);
        timerInterval = setInterval(updateTimer, 1000);
        getNextQuestion();
    }

    function updateTimer() {
        timerValue--;
        updateTimerDisplay();
        if (timerValue <= 0) {
            endGame();
        }
    }

    function updateTimerDisplay() {
        timer.value = timerValue;
    }

    function endGame() {
        clearInterval(timerInterval);
        quizContainer.style.display = 'none';
        resultContainer.style.display = 'flex';
        const resultMessage = timerValue < 0 && points < 0 ? 'Sorry, you lost!' : (points >= 0 ? 'Congratulations! You won!' : 'Sorry, you lost!');
        resultText.innerText = resultMessage;
        finalPoints.innerText = `Your final score: ${points} points`;
        sessionStorage.removeItem('quizGameState');
    }

    function getNextQuestion() {
        fetch('/api/GamePlay/GetNextQuestion')
            .then(response => {
                if (!response.ok) {
                    throw new Error(`Failed to fetch the next question. Server responded with status ${response.status}`);
                }
                return response.json();
            })
            .then(data => {
                console.log('Received data:', data);
                const actualShuffledOptions = Array.isArray(data.shuffledOptions) ? data.shuffledOptions : [];
                if (actualShuffledOptions.length > 0) {
                    questionText.innerText = data.questionText;
                    optionsList.innerHTML = '';
                    actualShuffledOptions.forEach((option, index) => {
                        const listItem = document.createElement('li');
                        listItem.innerText = option;
                        listItem.addEventListener('click', function () {
                            submitAnswer(option, data.correctAnswer);
                        });
                        optionsList.appendChild(listItem);
                    });
                    quizQuestions.push({
                        QuestionText: data.questionText,
                        CorrectAnswer: data.correctAnswer,
                    });
                } else {
                    console.error('ShuffledOptions is not an array or is empty:', actualShuffledOptions);
                }
            })
            .catch(error => {
                console.error('Fetch error:', error);
            });
    }

    function submitAnswer(selectedOption, correctAnswer) {
        console.log(`User's answer: ${selectedOption}`);
        console.log(`Correct answer: ${correctAnswer}`);
        const isCorrect = selectedOption === correctAnswer;

        if (isCorrect) {
            points += 50;
            console.log('Correct answer! +50 points');
        } else {
            points -= 20;
            console.log('Wrong answer! -20 points');
        }

        console.log(`Current points: ${points}`);
        updatePointsDisplay();
        if (currentQuestionIndex === quizQuestions.length - 1) {
            console.log("Sorry, we ran out of questions.");
            getNextQuestion();
        } else {
            currentQuestionIndex++;
            getNextQuestion();
        }
    }

    function updatePointsDisplay() {
        const pointsText = document.getElementById('points-text');

        if (pointsText) {
            pointsText.innerText = `Points: ${points}`;
        } else {
            console.error('Element with ID "points-text" not found.');
        }
    }

    function restartGame() {
        clearInterval(timerInterval);
        sessionStorage.removeItem('quizGameState');
        startGame();
    }

    restartBtn.addEventListener('click', restartGame);
    //startGame();
});
