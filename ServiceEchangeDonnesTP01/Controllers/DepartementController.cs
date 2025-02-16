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
        /// <summary>
        /// récupère un département à partir des noms de cégep et de département
        /// </summary>
        /// <param name="nomCegep">nom du cégep</param>
        /// <param name="nomDepartement">nom du département</param>
        /// <returns>le département trouvé ou une réponse not found</returns>
        [HttpGet]
        public JsonResult GetDepartement(string nomCegep, string nomDepartement)
        {
            DepartementDTO departement = CegepControleur.Instance.ObtenirDepartement(nomCegep, nomDepartement);
            if (departement == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(departement));
        }

        /// <summary>
        /// ajoute un département à un cégep
        /// </summary>
        /// <param name="nomCegep">nom du cégep</param>
        /// <param name="departement">données du département à ajouter</param>
        /// <returns>le département ajouté</returns>
        [HttpPost]
        public JsonResult AddDepartement(string nomCegep, DepartementDTO departement)
        {
            CegepControleur.Instance.AjouterDepartement(nomCegep, departement);
            return new JsonResult(Ok(departement));
        }

        /// <summary>
        /// modifie un département existant pour un cégep
        /// </summary>
        /// <param name="nomCegep">nom du cégep</param>
        /// <param name="departement">données du département à modifier</param>
        /// <returns>le département modifié ou une réponse not found</returns>
        [HttpPost]
        public IActionResult EditDepartement(string nomCegep, DepartementDTO departement)
        {
            if (CegepControleur.Instance.ObtenirDepartement(nomCegep, departement.Nom) != null)
            {
                CegepControleur.Instance.ModifierDepartement(nomCegep, departement);
                return Ok(departement);
            }
            return NotFound();
        }


        /// <summary>
        /// récupère tous les départements d'un cégep
        /// </summary>
        /// <param name="nomCegep">nom du cégep</param>
        /// <returns>la liste des départements du cégep ou une réponse not found</returns>
        [HttpGet]
        public JsonResult GetAllDepartement(string nomCegep)
        {
            List<DepartementDTO> cegeps = CegepControleur.Instance.ObtenirListeDepartement(nomCegep);
            if (cegeps == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(cegeps));
        }

        /// <summary>
        /// supprime un département d'un cégep
        /// </summary>
        /// <param name="nomCegep">nom du cégep</param>
        /// <param name="nomDepartement">nom du département à supprimer</param>
        /// <returns>une réponse no content si le département est supprimé</returns>
        [HttpDelete]
        public IActionResult DeleteDepartement(string nomCegep, string nomDepartement)
        {
            DepartementDTO departement = CegepControleur.Instance.ObtenirDepartement(nomCegep, nomDepartement);
            if (departement == null)
                return NotFound();

            CegepControleur.Instance.SupprimerDepartement(nomCegep, nomDepartement);
            return NoContent();
      
        }


    /// <summary>
    /// supprime tous les départements d'un cégep
    /// </summary>
    /// <param name="nomCegep">nom du cégep</param>
    /// <returns>une réponse no content si tous les départements sont supprimés</returns>
    [HttpDelete]
    public IActionResult DeleteAllDepartement(string nomCegep)
    {
        CegepControleur.Instance.ViderListeDepartement(nomCegep);
        return NoContent();
    }

    }



}

