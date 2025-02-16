using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceEchangeDonnesTP01.Controleurs;
using ServiceEchangeDonnesTP01.DTOs;

namespace ServiceEchangeDonnesTP01.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EnseignantController : ControllerBase
    {
        /// <summary>
        /// récupère un enseignant à partir des noms de cégep et de enseignant
        /// </summary>
        /// <param name="nomCegep">nom du cégep</param>
        /// <param name="nomEnseignant">nom du enseignant</param>
        /// <returns>le enseignant trouvé ou une réponse not found</returns>
        [HttpGet]
        public JsonResult GetEnseignant(string nomCegep, string nomDepartement, int noEnseignant)
        {
            EnseignantDTO enseignant = CegepControleur.Instance.ObtenirEnseignant(nomCegep, nomDepartement, noEnseignant);
            if (enseignant == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(enseignant));
        }

        /// <summary>
        /// ajoute un enseignant à un cégep
        /// </summary>
        /// <param name="nomCegep">nom du cégep</param>
        /// <param name="enseignant">données du enseignant à ajouter</param>
        /// <returns>le enseignant ajouté</returns>
        [HttpPost]
        public JsonResult AddEnseignant(string nomCegep, string nomDepartement, EnseignantDTO enseignant)
        {
            CegepControleur.Instance.AjouterEnseignant(nomCegep, nomDepartement, enseignant);
            return new JsonResult(Ok(enseignant));
        }

        /// <summary>
        /// modifie un enseignant existant pour un cégep
        /// </summary>
        /// <param name="nomCegep">nom du cégep</param>
        /// <param name="enseignant">données du enseignant à modifier</param>
        /// <returns>le enseignant modifié ou une réponse not found</returns>
        [HttpPost]
        public JsonResult EditEnseignant(string nomCegep, string nomDepartement, EnseignantDTO enseignant)
        {
            if (CegepControleur.Instance.ObtenirEnseignant(nomCegep,nomDepartement, enseignant.NoEmploye) != null)
            {
                CegepControleur.Instance.ModifierEnseignant(nomCegep,nomDepartement, enseignant);
                return new JsonResult(Ok(enseignant));
            }
            return new JsonResult(NotFound());
        }

        /// <summary>
        /// récupère tous les enseignants d'un cégep
        /// </summary>
        /// <param name="nomCegep">nom du cégep</param>
        /// <returns>la liste des enseignants du cégep ou une réponse not found</returns>
        [HttpGet]
        public JsonResult GetAllEnseignant(string nomCegep, string nomDepartement)
        {
            List<EnseignantDTO> cegeps = CegepControleur.Instance.ObtenirListeEnseignant(nomCegep, nomDepartement);
            if (cegeps == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(cegeps));
        }

        /// <summary>
        /// supprime un enseignant d'un cégep
        /// </summary>
        /// <param name="nomCegep">nom du cégep</param>
        /// <param name="nomEnseignant">nom du enseignant à supprimer</param>
        /// <returns>une réponse no content si le enseignant est supprimé</returns>
        [HttpDelete]
        public JsonResult DeleteEnseignant(string nomCegep, string nomDepartement, int noEnseignant)
        {
            EnseignantDTO cegep = CegepControleur.Instance.ObtenirEnseignant(nomCegep, nomDepartement, noEnseignant);
            if (cegep == null)
                return new JsonResult(NotFound());

            CegepControleur.Instance.SupprimerEnseignant(nomCegep, nomDepartement, noEnseignant);
            return new JsonResult(NoContent());
        }

        /// <summary>
        /// supprime tous les enseignants d'un cégep
        /// </summary>
        /// <param name="nomCegep">nom du cégep</param>
        /// <returns>une réponse no content si tous les enseignants sont supprimés</returns>
        [HttpDelete]
        public JsonResult DeleteAllEnseignant(string nomCegep, string nomDepartement)
        {
            CegepControleur.Instance.ViderListeEnseignant(nomCegep, nomDepartement);
            return new JsonResult(NoContent());
        }
    }
}
