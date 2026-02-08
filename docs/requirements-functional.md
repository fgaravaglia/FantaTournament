# Functional Requirements

## Core Concepts

1.  **Board**: Represents a specific tournament (e.g., World Cup 2026, Euro 2028).
    *   A Board consists of a series of **Matches**.
    *   Matches are categorized by **Phases**.
    *   **Teams**: Teams participate in the board (e.g., Germany, Scotland).
    *   **Placeholders**: In later phases (e.g., Round of 16), matches are initially defined by placeholders (e.g., "Winner Group A" vs "Runner-up Group C") until the actual teams are determined.

2.  **Match**: A specific game in the board.
    *   Defined by a **Code** and **Phase**.
    *   Participants can be **Teams** (if known) or **Placeholders** (if not yet determined).
    *   Has a **Result** (Regular Time, Extra Time).
    *   Has a **Status** (Scheduled, In Progress, Played).

3.  **Forecast**: A user's prediction for a specific Board.
    *   Every user can create a forecast for a board.
    *   Inside a forecast, the user specifies a predicted result for each match.
    *   **Scores**: Each forecast receives a score based on the accuracy of the predictions against real match results.

4.  **League**: A private or public mini-tournament within a Board.
    *   Users can join multiple Leagues.
    *   Each League has its own **Ranking**, calculated based on the forecasts of its members only.
    *   A Global Ranking exists for the Board alongside League Rankings.

## Scoring & Ranking

1.  **Scoring Engine**:
    *   Scores are calculated at the end of every match.
    *   Only played and finished matches are considered.
    *   Matches not yet played are ignored (score 0).
    *   The system compares the **Actual Result** with the **Predicted Result** using a defined policy/table.

1.1 **Scoring Policy**:
    *   The scoring policy is defined by a Code, a DisplayName, a brief description.
    *   each policy can be or not applied to a specific match, based on its MatchPhase.

1.1.1 **Extact Match Policy**
    * Code: EXACT_MATCH
    * DisplayName: Exact Match
    * Description: The predicted result must be exactly the same as the actual result.
    * MatchPhases: All  
    * Score: 
      * 3 points if the predicted result is exactly the same as the actual result for Round Matches
      * 5 points if the predicted result is exactly the same as the actual result for other Matches

1.1.2 **Regular Match Policy**
    * Code: REGULAR_SCORE
    * DisplayName: Regular Score 
    * Description: Pronostico per il risultato al termine dei tempi regolamentari
    * MatchPhases: Not GroupStage  
    * Score: 
      * 3 points if the predicted result is exactly the same as the actual result for GroupStage Matches
      * 1.5 points if the Goal Difference is the same as the actual result for other Matches

1.1.3 **Extended Match Policy**
    * Code: EXTENDED_SCORE
    * DisplayName: Extended SCore 
    * Description: Pronostico per il risultato al termine dei tempi supplementari/Rigori; Valido solo per la fase finale del torneo
    * MatchPhases: Not GroupStage  
    * Score: 
      * 3 points if the predicted result is exactly the same as the actual result for GroupStage Matches
      * 1.5 points if the Goal Difference is the same as the actual result for other Matches

1.1.4 **Match Winner Policy**
    * Code: MATCH_WINNER
    * DisplayName: Match Winner 
    * Description: Pronostico per il vincitore del match; Valido solo per la fase finale del torneo
    * MatchPhases: All
    * Score: 
      * 3 points if the predicted result is exactly the same as the actual result for GroupStage Matches
      * 5 points if the predicted result is exactly the same as the actual result for other Matches

2.  **Ranking**:
    *   Users are ranked based on their **Total Score**.
    *   **Tie-Breaker**: In case of a tie, the user who updated their forecast **earliest** is ranked higher (First-Come-First-Served logic on updates).
    *   **Scope**: Rankings can be calculated Globally (all users of a Board) or per **League** (subset of users).


## Application Layer

The following operations must be supported by the system's Application Layer to drive the UI or API clients.

### 1. Board Queries

1.  **Search Boards**:
    *   The system must allow searching for Boards by name (fuzzy search / like).
    *   Example: Searching for "World" matches "World Cup 2026".

2.  **Get Board Matches**:
    *   The system must allow retrieving all matches for a specific Board.
    *   The response must include match details: Code, Phase, Date, Status, Team Names (or Placeholders), and Results.

3.  **Get Board Teams**:
    *   The system must allow retrieving the list of Teams participating in a specific Board.
    *   The list should be distinct (no duplicates) and derived from the matches configured on the board.

### 2. Board Commands

1.  **Update Match Result**:
    *   The system must allow updating the result (score) of a match.
    *   Input: BoardId, MatchId, HomeScore, AwayScore.

2.  **Update Match Status**:
    *   The system must allow updating the status of a match (e.g., Scheduled -> InProgress -> Played).
    *   Input: BoardId, MatchId, NewStatus.

3.  **Import Matches**:
    *   The system must allow importing a list of matches to initialize the schedule for a Board.
    *   This sets the matches "To Be Played".

### 3. Forecast Queries

1.  **Get Forecast by ID**:
    *   Retrieves a specific forecast and all its associated predictions.
    *   Includes calculated scores for each prediction and the total forecast score.

2.  **Get Forecasts by User**:
    *   Retrieves all forecasts created by a specific user across different boards.

### 4. Forecast Commands

1.  **Create Forecast**:
    *   Allows a user to create a new forecast for a specific board.
    *   Input: UserId, BoardId, List of Predictions.

2.  **Update Forecast**:
    *   Allows a user to update an existing forecast.
    *   Input: ForecastId, List of Predictions.

3.  **Delete Forecast**:
    *   Allows a user (or admin) to delete a specific forecast.
    *   Input: ForecastId.
