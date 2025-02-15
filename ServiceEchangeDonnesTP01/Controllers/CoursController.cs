using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceEchangeDonnesTP01.Controleurs;
using ServiceEchangeDonnesTP01.DTOs;

namespace ServiceEchangeDonnesTP01.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CoursController : ControllerBase
    {
        /// <summary>
        /// récupère un cours à partir des noms de cégep et de cours
        /// </summary>
        /// <param name="nomCegep">nom du cégep</param>
        /// <param name="nomCours">nom du cours</param>
        /// <returns>le cours trouvé ou une réponse not found</returns>
        [HttpGet]
        public JsonResult GetCours(string nomCegep, string nomDepartement, string nomCours)
        {
            CoursDTO cours = CegepControleur.Instance.ObtenirCours(nomCegep, nomDepartement, nomCours);
            if (cours == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(cours));
        }

        /// <summary>
        /// ajoute un cours à un cégep
        /// </summary>
        /// <param name="nomCegep">nom du cégep</param>
        /// <param name="cours">données du cours à ajouter</param>
        /// <returns>le cours ajouté</returns>
        [HttpPost]
        public JsonResult AddCours(string nomCegep, string nomDepartement, CoursDTO cours)
        {
            CegepControleur.Instance.AjouterCours(nomCegep, nomDepartement, cours);
            return new JsonResult(Ok(cours));
        }

        /// <summary>
        /// modifie un cours existant pour un cégep
        /// </summary>
        /// <param name="nomCegep">nom du cégep</param>
        /// <param name="cours">données du cours à modifier</param>
        /// <returns>le cours modifié ou une réponse not found</returns>
        [HttpPost]
        public JsonResult EditCours(string nomCegep, string nomDepartement, CoursDTO cours)
        {
            if (CegepControleur.Instance.ObtenirCours(nomCegep, nomDepartement, cours.Nom) != null)
            {
                CegepControleur.Instance.ModifierCours(nomCegep, nomDepartement, cours);
                return new JsonResult(Ok(cours));
            }
            return new JsonResult(NotFound());
        }

        /// <summary>
        /// récupère tous les courss d'un cégep
        /// </summary>
        /// <param name="nomCegep">nom du cégep</param>
        /// <returns>la liste des courss du cégep ou une réponse not found</returns>
        [HttpGet]
        public JsonResult GetAllCours(string nomCegep, string nomDepartement)
        {
            List<CoursDTO> cegeps = CegepControleur.Instance.ObtenirListeCours(nomCegep, nomDepartement);
            if (cegeps == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(cegeps));
        }

        /// <summary>
        /// supprime un cours d'un cégep
        /// </summary>
        /// <param name="nomCegep">nom du cégep</param>
        /// <param name="nomCours">nom du cours à supprimer</param>
        /// <returns>une réponse no content si le cours est supprimé</returns>
        [HttpDelete]
        public JsonResult DeleteCours(string nomCegep, string nomDepartement, string nomCours)
        {
            CoursDTO cegep = CegepControleur.Instance.ObtenirCours(nomCegep, nomDepartement, nomCours);
            if (cegep == null)
                return new JsonResult(NotFound());

            CegepControleur.Instance.SupprimerCours(nomCegep, nomDepartement, nomCours);
            return new JsonResult(NoContent());
        }

        /// <summary>
        /// supprime tous les courss d'un cégep
        /// </summary>
        /// <param name="nomCegep">nom du cégep</param>
        /// <returns>une réponse no content si tous les courss sont supprimés</returns>
        [HttpDelete]
        public JsonResult DeleteAllCours(string nomCegep, string nomDepartement)
        {
            CegepControleur.Instance.ViderListeCours(nomCegep, nomDepartement);
            return new JsonResult(NoContent());
        }
    }
}
