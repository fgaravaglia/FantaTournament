namespace FantaTournament.Api.Tests.Controllers;

[TestFixture]
public class BoardsControllerTests
{
    private IBoardQueries _boardQueries;
    private IBoardCommands _boardCommands;
    private BoardsController _controller;

    [SetUp]
    public void SetUp()
    {
        //****************** ARRANGE
        _boardQueries = Substitute.For<IBoardQueries>();
        _boardCommands = Substitute.For<IBoardCommands>();
        _controller = new BoardsController(_boardQueries, _boardCommands);
    }

    [Test]
    public async Task SearchBoards_WithValidName_ReturnsOkWithBoards()
    {
        //****************** ARRANGE
        var boards = new List<BoardDto> { new BoardDto { Id = "1", Name = "Test Board" } };
        _boardQueries.SearchBoardsAsync("Test").Returns(Result<IEnumerable<BoardDto>>.Success(boards));

        //****************** ACT
        var result = await _controller.SearchBoards("Test");

        //****************** ASSERT
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo(boards));
        Assert.Pass();
    }

    [Test]
    public async Task GetBoardMatches_WithExistingId_ReturnsOkWithMatches()
    {
        //****************** ARRANGE
        var boardId = "board1";
        var matches = new BoardMatchesDto { BoardId = boardId, Matches = [] };
        _boardQueries.GetBoardMatchesAsync(boardId).Returns(Result<BoardMatchesDto>.Success(matches));

        //****************** ACT
        var result = await _controller.GetBoardMatches(boardId);

        //****************** ASSERT
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo(matches));
        Assert.Pass();
    }

    [Test]
    public async Task GetBoardTeams_WithExistingId_ReturnsOkWithTeams()
    {
        //****************** ARRANGE
        var boardId = "board1";
        var teams = new List<TeamDto> { new TeamDto { Id = "t1", Name = "Team 1" } };
        _boardQueries.GetBoardTeamsAsync(boardId).Returns(Result<IEnumerable<TeamDto>>.Success(teams));

        //****************** ACT
        var result = await _controller.GetBoardTeams(boardId);

        //****************** ASSERT
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo(teams));
        Assert.Pass();
    }

    [Test]
    public async Task UpdateMatchStatus_WithValidData_ReturnsOkWithId()
    {
        //****************** ARRANGE
        var boardId = "board1";
        var matchId = "match1";
        var status = MatchStatus.InProgress;
        _boardCommands.UpdateMatchStatusAsync(boardId, matchId, status).Returns(Result<string>.Success(matchId));

        //****************** ACT
        var result = await _controller.UpdateMatchStatus(boardId, matchId, status);

        //****************** ASSERT
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo(matchId));
        Assert.Pass();
    }

    [Test]
    public async Task ImportMatches_WithValidData_ReturnsOkWithId()
    {
        //****************** ARRANGE
        var boardId = "board1";
        var matches = new List<MatchDto>();
        _boardCommands.ImportMatchesAsync(boardId, matches).Returns(Result<string>.Success(boardId));

        //****************** ACT
        var result = await _controller.ImportMatches(boardId, matches);

        //****************** ASSERT
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.Value, Is.EqualTo(boardId));
        Assert.Pass();
    }
}
