using SED.DAL;
using SED.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Http.Cors;

namespace SED.Services.Controllers
{
    //[EnableCors(origins:"*", headers: "*", methods: "*")]
    public class SportEventsController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public SportEventsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("api/SportEvents/GetUpcomming")]
        public IEnumerable<SportEvent> GetUpcomming()
        {
            //var result = unitOfWork.SportEvents.GetList(
            //    s => s.Date.Value.Date > DateTime.Today
            //    , s => s.Location);
            var result = unitOfWork.SportEvents.GetAll(s => s.Location);
            return result;
        }

        [HttpGet()]
        public IHttpActionResult GetById(int id)
        {
            var sportEvent = unitOfWork.SportEvents.GetSingle(
                s => s.SportEventId == id,
                s => s.Activities,
                s => s.Location,
                s => s.Activities.Select(a => a.ActivityType),
                s => s.Comments);
            if (sportEvent == null)
            {
                return NotFound();
            }
            return Ok(sportEvent);
        }

        [HttpGet]
        [Route("api/sportEvents/search")]
        public IEnumerable<SportEvent> Search([FromUri]SportEventSearchOptions searchOptions)
        {
            IEnumerable<SportEvent> result;
            result = unitOfWork.SportEvents.GetList(s =>
                (!string.IsNullOrEmpty(searchOptions.SporEventName) && s.Name.Contains(searchOptions.SporEventName))
                || (!string.IsNullOrEmpty(searchOptions.LocationName) && s.Location != null && s.Location.Name.Contains(searchOptions.LocationName))
                || (!string.IsNullOrEmpty(searchOptions.ActivityTypeName) && s.Activities != null
                        && s.Activities.Any(a => a.ActivityType != null && !string.IsNullOrEmpty(a.ActivityType.Name)
                            && a.ActivityType.Name.Contains(searchOptions.ActivityTypeName))),
                            
                s => s.Location);
            return result;
        }


        [HttpPost]
        public IHttpActionResult Create(HttpRequestMessage request, [FromBody] SportEvent sportEvent)
        {
            if (sportEvent == null)
            {
                return BadRequest(Resources.ErrorMessages.NullSportEvent);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (sportEvent.Date.HasValue && sportEvent.Date.Value.Date <= DateTime.Today.Date)
            {
                return BadRequest(Resources.ErrorMessages.PastDate);
            }

            try
            {
                unitOfWork.SportEvents.Insert(sportEvent);
                unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPut]
        public IHttpActionResult Update(HttpRequestMessage request, [FromBody] SportEvent sportEvent)
        {
            if (sportEvent.SportEventId == 0)
            {
                 return NotFound();
            }
            if (sportEvent == null)
            {
                return BadRequest(Resources.ErrorMessages.NullSportEvent);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (sportEvent.Date.HasValue && sportEvent.Date.Value.Date <= DateTime.Today.Date)
            {
                return BadRequest(Resources.ErrorMessages.PastDate);
            }

            try
            {
                unitOfWork.SportEvents.Update(sportEvent);
                unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            try
            {
                unitOfWork.SportEvents.Delete(id);
                unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/sportEvents/subscribe")]
        public IHttpActionResult SubscribeToNotifications(HttpRequestMessage request, [FromBody] Subscriber subscriber)
        {
            if (subscriber == null)
            {
                return BadRequest(Resources.ErrorMessages.NullSportEvent);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                unitOfWork.Subscribers.Insert(subscriber);
                unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/host/notify")]
        public IHttpActionResult NotifySubscribers(HttpRequestMessage request, [FromBody] Email email)
        {
            if (email == null)
            {
                return BadRequest(Resources.ErrorMessages.NullSportEvent);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var subscriberEmails = unitOfWork.Subscribers.GetList(
                    s => s.SportEventId == email.SportEventId)
                    .Select(s =>s.Email);

                if (subscriberEmails.Count() == 0)
                {
                    return Content(HttpStatusCode.InternalServerError, Resources.ErrorMessages.NoSubscribersToNotify);
                }

                var toAddress = string.Join(",", subscriberEmails);
                bool success = SED.Utilities.EmailUtility.SendEmail(toAddress, email.Subject, email.Body);

                if (success)
                {
                    return Ok();
                }
                return Content(HttpStatusCode.InternalServerError, Resources.ErrorMessages.EmailNotSent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
