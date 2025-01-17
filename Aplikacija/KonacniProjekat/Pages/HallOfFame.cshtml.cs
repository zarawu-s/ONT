using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KonacniProjekat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KonacniProjekat
{
    public class HallOfFameModel : PageModel
    {
        public int? SessionId {get; set;}
        public readonly OrganizacijaContext dbContext;

        public HallOfFameModel(OrganizacijaContext db)
        {
            dbContext = db;
        }
        
        public IList<HallOfFame> SviHOF { get;set; }
        public IList<Kvizovi> sviKvizovi {get;set;}
        public Kvizovi trenutniKviz{get;set;}
        public async Task OnGetAsync(int id)
        {
            sviKvizovi = await dbContext.Kvizovi.ToListAsync();   
            SviHOF = await dbContext.HallOfFame.Where(x=>x.IdKvizaHof==id).OrderByDescending(x=>x.Poeni).ToListAsync();
            foreach(var hof in SviHOF)
            {
                    hof.IdKvizaHofNavigation = await dbContext.Kvizovi.Where(x=>x.IdKviza==hof.IdKvizaHof).FirstOrDefaultAsync();
                    hof.IdTuristeHofNavigation = await dbContext.Turisti.Where(x=>x.IdTuriste==hof.IdTuristeHof).FirstOrDefaultAsync();
            }
            trenutniKviz = dbContext.Kvizovi.Where(x=>x.IdKviza == id).FirstOrDefault();
        }

    }
}
