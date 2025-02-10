using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceEchangeDonnesTP01.Modeles;
using ServiceEchangeDonnesTP01.DTOs;
using ServiceEchangeDonnesTP01.Controleurs;

namespace ServiceEchangeDonnesTP01.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CegepController : ControllerBase
    {



        [HttpGet]
        public JsonResult GetCegep(string nom)
        {
            CegepDTO cegep = CegepControleur.Instance.ObtenirCegep(nom);
            if (cegep == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(cegep));
        }

        [HttpPost]
        public JsonResult AddCegep(CegepDTO cegep)
        {

            CegepControleur.Instance.AjouterCegep(cegep);
            return new JsonResult(Ok(cegep));


        }

        [HttpPost]
        public JsonResult EditCegep(CegepDTO cegep)
        {

            if (CegepControleur.Instance.ObtenirCegep(cegep.Nom) !=null) 
            {
                CegepControleur.Instance.ModifierCegep(cegep);
                return new JsonResult(Ok(cegep));
            }
                return new JsonResult(NotFound());

            
        }

        [HttpGet]
        public JsonResult GetAllCegep()
        {
            List<CegepDTO> cegeps = CegepControleur.Instance.ObtenirListeCegep();
            if (cegeps == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(cegeps));
        }

        [HttpDelete]
        public JsonResult DeleteCegep(string nom)
        {
            CegepDTO cegep = CegepControleur.Instance.ObtenirCegep(nom);
            if (cegep == null)
                return new JsonResult(NotFound());

            CegepControleur.Instance.SupprimerCegep(nom);
            return new JsonResult(NoContent());
        }

        [HttpDelete]
        public JsonResult DeleteAllCegep()
        {
            CegepControleur.Instance.ViderListeCegep();
            return new JsonResult(NoContent());
        }
    }
}
