using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using app.Data;
using app.Models;

namespace app.Controllers
{
	[Route("api/[controller]")]
    public class SpeakerEvalController : Controller
    {
		private readonly iSpeakerEvalsRepository repository;

		public SpeakerEvalController (iSpeakerEvalsRepository repository)
		{
		    this.repository = repository;
		}

		// GET api/values
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(repository.GetAll());
		}

	}
}
