using System;
using System.Collections.Generic;
using knight.Models;
using knight.Services;
using Microsoft.AspNetCore.Mvc;

namespace knight.Controllers
{

  [ApiController]
  [Route("api/[controller]")]

  public class KnightsController : ControllerBase
  {
    private readonly KnightsService _knightsService;
    public KnightsController(KnightsService knightsService)
    {
      _knightsService = knightsService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Knight>> GetAll()
    {
      try
      {
        IEnumerable<Knight> knights = _knightsService.GetAll();
        return Ok(knights);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Knight> GetById(int id)
    {
      try
      {
        Knight knight = _knightsService.GetById(id);
        return Ok(knight);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    public ActionResult<Knight> Create([FromBody] Knight newKnight)
    {
      try
      {
        Knight knight = _knightsService.Create(newKnight);
        return Ok(knight);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpPut("{id}")]
    public ActionResult<Knight> Update(int id, [FromBody] Knight update)
    {
      try
      {
        update.Id = id;
        Knight updated = _knightsService.Update(update);
        return Ok(updated);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    public ActionResult<String> DeleteKnight(int id)
    {
      try
      {
        _knightsService.DeleteKnight(id);
        return Ok("Deleted Knight");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}