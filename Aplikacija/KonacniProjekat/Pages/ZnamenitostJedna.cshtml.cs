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
    public class ZnamenitostJednaModel : PageModel
    {
        public int? SessionId {get; set;}
        public readonly OrganizacijaContext dbContext;

        public ZnamenitostJednaModel(OrganizacijaContext db)
        {
            dbContext = db;
        }
        
        [BindProperty]
        public Znamenitosti TrenutnaZnamenitost{get;set;}
          [BindProperty]
        public IList<Slike> Slike{get;set;}
        
        public async Task<IActionResult> OnGetAsync(int? id){

            SessionId = SessionClass.SessionId;            

            TrenutnaZnamenitost=await dbContext.Znamenitosti.Where(x=>x.IdZnamenitosti==id).FirstOrDefaultAsync();
            Slike = await dbContext.Slike.Where(x=>x.IdZnamenitost==id).ToListAsync();

            if(TrenutnaZnamenitost==null){
                return NotFound();
            }
            return Page();
        }
    }
}
