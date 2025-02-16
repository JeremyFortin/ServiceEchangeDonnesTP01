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

        /// <summary>
        /// récupère un cégep à partir de son nom
        /// </summary>
        /// <param name="nom">nom du cégep</param>
        /// <returns>le cégep trouvé ou une réponse not found</returns>
        [HttpGet]
        public JsonResult GetCegep(string nom)
        {
            CegepDTO cegep = CegepControleur.Instance.ObtenirCegep(nom);
            if (cegep == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(cegep));
        }

        /// <summary>
        /// ajoute un cégep
        /// </summary>
        /// <param name="cegep">données du cégep à ajouter</param>
        /// <returns>le cégep ajouté</returns>
        [HttpPost]
        public IActionResult AddCegep(CegepDTO cegep)
        {
            CegepControleur.Instance.AjouterCegep(cegep);
            return Ok(cegep);
        }


        /// <summary>
        /// modifie un cégep existant
        /// </summary>
        /// <param name="cegep">données du cégep à modifier</param>
        /// <returns>le cégep modifié ou une réponse not found</returns>
        [HttpPost]
        public IActionResult EditCegep(CegepDTO cegep)
        {
            if (CegepControleur.Instance.ObtenirCegep(cegep.Nom) != null)
            {
                CegepControleur.Instance.ModifierCegep(cegep);
                return Ok(cegep); 
            }
            return NotFound();
        }


        /// <summary>
        /// récupère tous les cégeps
        /// </summary>
        /// <returns>la liste de tous les cégeps ou une réponse not found</returns>
        [HttpGet]
        public JsonResult GetAllCegep()
        {
            List<CegepDTO> cegeps = CegepControleur.Instance.ObtenirListeCegep();
            if (cegeps == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(cegeps));
        }

        /// <summary>
        /// supprime un cégep
        /// </summary>
        /// <param name="nom">nom du cégep à supprimer</param>
        /// <returns>une réponse no content si le cégep est supprimé</returns>
        [HttpDelete]
        public IActionResult DeleteCegep(string nom)
        {
            CegepDTO cegep = CegepControleur.Instance.ObtenirCegep(nom);
            if (cegep == null)
                return NotFound(); 

            CegepControleur.Instance.SupprimerCegep(nom);
            return NoContent(); 
        }


        /// <summary>
        /// supprime tous les cégeps
        /// </summary>
        /// <returns>une réponse no content si tous les cégeps sont supprimés</returns>
        [HttpDelete]
        public IActionResult DeleteAllCegep()
        {
            CegepControleur.Instance.ViderListeCegep();
            return NoContent();
        }

    }
}
