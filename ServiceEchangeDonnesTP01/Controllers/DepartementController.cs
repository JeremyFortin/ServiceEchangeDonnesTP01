using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceEchangeDonnesTP01.Controleurs;
using ServiceEchangeDonnesTP01.DTOs;

namespace ServiceEchangeDonnesTP01.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartementController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetDepartement(string nomCegep, string nomDepartement)
        {
            DepartementDTO departement = CegepControleur.Instance.ObtenirDepartement(nomCegep, nomDepartement);
            if (departement == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(departement));
        }

        [HttpPost]
        public JsonResult AddDepartement(string nomCegep, DepartementDTO departement)
        {

            CegepControleur.Instance.AjouterDepartement(nomCegep, departement);
            return new JsonResult(Ok(departement));


        }

        [HttpPost]
        public JsonResult EditDepartement(string nomCegep, DepartementDTO departement)
        {

            if (CegepControleur.Instance.ObtenirDepartement(nomCegep, departement.Nom) != null)
            {
                CegepControleur.Instance.ModifierDepartement(nomCegep, departement);
                return new JsonResult(Ok(departement));
            }
            return new JsonResult(NotFound());


        }

        [HttpGet]
        public JsonResult GetAllDepartement(string nomCegep)
        {
            List<DepartementDTO> cegeps = CegepControleur.Instance.ObtenirListeDepartement(nomCegep);
            if (cegeps == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(cegeps));
        }

        [HttpDelete]
        public JsonResult DeleteDepartement(string nomCegep, string nomDepartement)
        {
            DepartementDTO cegep = CegepControleur.Instance.ObtenirDepartement(nomCegep, nomDepartement);
            if (cegep == null)
                return new JsonResult(NotFound());

            CegepControleur.Instance.SupprimerDepartement(nomCegep, nomDepartement);
            return new JsonResult(NoContent());
        }

        [HttpDelete]
        public JsonResult DeleteAllDepartement(string nomCegep)
        {
            CegepControleur.Instance.ViderListeDepartement(nomCegep);
            return new JsonResult(NoContent());
        }
    }
}
