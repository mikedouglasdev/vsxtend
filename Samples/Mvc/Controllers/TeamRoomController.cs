using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vsxtend.Entities;
using Vsxtend.Interfaces;
using Vsxtend.Resources;
using Vsxtend.Samples.Mvc.ViewModels;

namespace Vsxtend.Samples.Mvc.Controllers
{
    public class TeamRoomController : Controller
    {
        private ITeamRoomClient _teamRoomClient;
        //
        // GET: /TeamRoom/
        public TeamRoomController(ITeamRoomClient teamRoomClient)
        {
            _teamRoomClient = teamRoomClient;
        }
        public async Task<ActionResult> Index(int id = 0)
        {
            if (id == 0)
            {
                var result = await _teamRoomClient.GetRoomsAsync();

                return View(result);
            }
            else
            {

                //var messages = await _teamRoomClient.GetMessagesAsync(id, DateTime.Parse("02/02/2014"));

                TeamRoomViewModel model = new TeamRoomViewModel
                {
                    Messages = new CollectionResult<TeamRoomMessage>()
                };

                return View("Messages", model);
            }
        }

        public async Task<IEnumerable<TeamRoomMessage>> GetMessagesSinceLastRequest(int roomId, string dateTime)
        {
            var messages = await _teamRoomClient.GetMessagesAsync(roomId, DateTime.Parse(dateTime));

            return messages.value;
        }


        public async Task<ActionResult> Join(int id)
        {
            await _teamRoomClient.JoinTeamRoomAsync(id);

            return RedirectToAction("Index", new { id = id });
        }

        public async Task<ActionResult> Details(int id)
        {
            var result = await _teamRoomClient.GetRoomAsync(id);

            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(TeamRoom room)
        {
            if (ModelState.IsValid)
            {
                var result = await _teamRoomClient.CreateTeamRoomAsync(room);

                return RedirectToAction("Index");
            }

            return View(room);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _teamRoomClient.GetRoomAsync(id);

            return View(result);
        }

[HttpGet]
public async Task<ActionResult> Delete(int id)
{
    await _teamRoomClient.DeleteTeamRoomAsync(id);

    return RedirectToAction("Index");
}

    [HttpPost]
    public async Task<ActionResult> Edit(int id, TeamRoom room)
    {
        if (ModelState.IsValid)
        {
            var result = await _teamRoomClient.UpdateTeamRoomAsync(id, room);

            return RedirectToAction("Index");
        }

        return View(room);
    }
    }
}
