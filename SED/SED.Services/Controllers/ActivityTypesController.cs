using SED.DAL;
using SED.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SED.Services.Controllers
{
    public class ActivityTypesController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public ActivityTypesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("api/ActivityTypes")]
        public IEnumerable<ActivityType> Get()
        {
            var result = unitOfWork.ActivityTypes.GetAll();
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
