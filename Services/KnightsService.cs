using System;
using System.Collections.Generic;
using knight.Repositories;
using knight.Models;

namespace knight.Services
{
  public class KnightsService
  {
    private readonly KnightsRepository _knightRepo;
    public KnightsService(KnightsRepository knightRepo)
    {
      _knightRepo = knightRepo;
    }
    internal IEnumerable<Knight> GetAll()
    {
      return _knightRepo.GetAll();
    }

    internal Knight GetById(int id)
    {
      Knight knight = _knightRepo.GetById(id);
      if (knight == null)
      {
        throw new Exception("Invalid Id");
      }
      return knight;
    }

    internal Knight Create(Knight newKnight)
    {
      Knight artist = _knightRepo.Create(newKnight);
      return artist;
    }

    internal Knight Update(Knight update)
    {
      Knight original = GetById(update.Id);
      original.Name = update.Name.Length > 0 ? update.Name : original.Name;
      original.BirthYear = update.BirthYear > 0 ? update.BirthYear : original.BirthYear;
      original.DeathYear = update.DeathYear > 0 ? update.DeathYear : original.DeathYear;
      if (_knightRepo.Update(original))
      {
        return original;
      }
      throw new Exception("Error... Update Not Successful");
    }

    internal void DeleteKnight(int id)
    {
      GetById(id);
      _knightRepo.Delete(id);
    }

  }
}