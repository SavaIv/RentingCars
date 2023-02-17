using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentingCars.Areas.Admin.Controllers
{
    [Area(AdminConstants.AreaName)]
    [Authorize(Roles = WebConstants.AdministratorRoleName)]
    public abstract class AdminController : Controller
    {
    }
}
