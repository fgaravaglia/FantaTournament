
using FantaTournament.Domain.Boards;
using FantaTournament.Domain.Boards.Abstractions;
using Umbrella.Core;

namespace FantaTournament.Infrastructure.Boards
{
    /// <summary>
    /// Implementation of ITeamRepository to persist teams in CSV files
    /// </summary>
    public class CSVTeamRepository : ITeamRepository
    {

        #region Attributes
        readonly string _Path;
        readonly string _FileName;
        static object _Locker = new object();
        #endregion

        internal CSVTeamRepository(string path, string fileName)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            this._Path = path;
            this._FileName = fileName;

            if (!Directory.Exists(this._Path))
                throw new DirectoryNotFoundException($"Folder {this._Path} not found");

            var fullpath = Path.Combine(this._Path, this._FileName);
            if (!File.Exists(fullpath))
                throw new FileNotFoundException($"File {this._FileName} inside {this._Path} not found", fullpath);
        }

        private Team FromPropertyValues(List<string> propertyValues)
        {
            var team = new Team();

            foreach (var p in propertyValues)
            {
                var index = propertyValues.IndexOf(p);
                switch (index)
                {
                    case 0:
                        team.Code = p;
                        break;
                    case 1:
                        team.DisplayName = p;
                        break;
                    case 2:
                        team.Id = p;
                        break;
                    default:
                        break;
                }

            }

            return team;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public Task<Result<IEnumerable<Team>>> GetAllAsync()
        {
            List<Team> list = new List<Team>();
            List<string> lines = new List<string>();
            lock (_Locker)
            {
                lines = File.ReadAllLines(Path.Combine(this._Path, this._FileName)).ToList();
            }

            foreach (var line in lines.Skip(1))
            {
                var propertyValues = line.Split(';').ToList();
                var team = FromPropertyValues(propertyValues);
                list.Add(team);
            }

            return Task.FromResult(Result<IEnumerable<Team>>.Success(list));
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public async Task<Result<Team>> GetByIdAsync(string keyValue)
        {
            var queryResult = await GetAllAsync();
            if (!queryResult.Succeeded)
                return Result<Team>.Failure(queryResult.Errors);

            var team = queryResult.Data?.SingleOrDefault(x => x.Code.Equals(keyValue, StringComparison.InvariantCultureIgnoreCase));
            if (team == null)
                return Result<Team>.NotFound();
            return Result<Team>.Success(team);
        }
    }

}