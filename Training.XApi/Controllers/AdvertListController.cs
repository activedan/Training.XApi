using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Training.XApi.Engine.Handlers.Queries.Adverts;
using Training.XApi.Engine.Settings;
using Training.XApi.Infrastructure.CQRS;
using Training.XApi.UiFactories.Desktop;

namespace Training.XApi.Controllers
{
    [Route("adverts")]
    public class AdvertListController : Controller
    {
        private AdvertListUiFactory _uiFactory;
        private IQueryDispatcher _queryDispatcher;
        private ISettings _apiSettings;

        public AdvertListController(AdvertListUiFactory uiFactory, IQueryDispatcher queryDispatcher, ISettings apiSettings)
        {
            _apiSettings = apiSettings;
            _uiFactory = uiFactory;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet, Route("listings/ui")]
        public async Task<IActionResult> GetAdvertListUi(Guid memberId)
        {
            var adverts = await _queryDispatcher.GetAdvertsByMemberId(memberId);

            return Ok(_uiFactory.GetAdvertListUi(adverts, _apiSettings));
        }
    }
}