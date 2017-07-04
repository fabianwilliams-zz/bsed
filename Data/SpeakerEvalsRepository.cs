using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using app.Models;

namespace app.Data
{
    public class SpeakerEvalsRepository : iSpeakerEvalsRepository
    {
		private readonly ApplicationDbContext context;

		public SpeakerEvalsRepository(ApplicationDbContext context)
		{
			this.context = context;
		}

		public async Task<SpeakerEval> AddAsync(SpeakerEval item)
		{
			context.Add(item);
			await context.SaveChangesAsync();

			return item;
		}

		public async Task<SpeakerEval> FindAsync(int id)
		{
			return await context.SpeakerEvaluations.FirstOrDefaultAsync(p => p.Id == id);
		}


		public IEnumerable<SpeakerEval> GetAll()
		{
			return context.SpeakerEvaluations;
		}

		public async Task RemoveAsync(int id)
		{
			var entity = await context.SpeakerEvaluations.SingleOrDefaultAsync(p => p.Id == id);
			if (entity == null)
				throw new InvalidOperationException("No Speaker EVals found matching id " + id);

			context.SpeakerEvaluations.Remove(entity);
			await context.SaveChangesAsync();
		}

		public async Task UpdateAsync(SpeakerEval se)
		{
			if (se == null)
				throw new ArgumentNullException(nameof(se));

			context.SpeakerEvaluations.Update(se);
			await context.SaveChangesAsync();
		}
    }
}
