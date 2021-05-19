using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using knight.Models;

namespace knight.Repositories
{
  public class KnightsRepository
  {
    private readonly IDbConnection _db;
    public KnightsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Knight> GetAll()
    {
      string sql = "SELECT * FROM knights";
      return _db.Query<Knight>(sql);
    }

    internal Knight GetById(int id)
    {
      string sql = "SELECT * FROM knights WHERE id = @id";
      return _db.QueryFirstOrDefault<Knight>(sql, new { id });
    }

    internal Knight Create(Knight newKnight)
    {
      string sql = @"
        INSERT INTO knights
        (name, birthyear, deathyear)
        VALUES
        (@Name, @BirthYear, @DeathYear);
        SELECT LAST_INSERT_ID()";

      newKnight.Id = _db.ExecuteScalar<int>(sql, newKnight);
      return newKnight;
    }

    internal bool Update(Knight original)
    {
      string sql = @"
      UPDATE knights
      SET
        name = @Name,
        birthyear = @BirthYear,
        deathyear = @DeathYear
      WHERE id=@Id
      ";
      int affectedRows = _db.Execute(sql, original);
      return affectedRows == 1;
    }

    internal bool Delete(int id)
    {
      // Dapper uses '@' to indicate a variable that can be safeley injectected in the Query arguments
      string sql = "DELETE FROM knights WHERE id = @id LIMIT 1";
      //   Query first or default returns a single item or null
      int affectedRows = _db.Execute(sql, new { id });
      return affectedRows == 1;
    }

  }
}