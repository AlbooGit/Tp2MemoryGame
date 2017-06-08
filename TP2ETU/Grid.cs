using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
namespace TP2PROF
{
  public class Grid
  {
      #region Propriétés et Accèsseurs
    /// <summary>
    /// Grille logique du jeu.
    /// Tableau 2D de PacmanElement
    /// </summary>  
      private PacmanElements[,] tabElements;
    /// <summary>
    /// Position de la cage des fantômes
    /// </summary>
      private Vector2i ghostCagePosition;
    /// <summary>
    /// Accesseur du numéro de la ligne où se trouve la cage à fantômes
    /// Propriété C#
    /// </summary>
      public int GhostCagePositionRow
      {
          get { return ghostCagePosition.X; }
      }
    /// <summary>
    /// Accesseur du numéro de la colonne où se trouve la cage à fantômes
    /// Propriété C#
    /// </summary>
      public int GhostCagePositionColumn
      {
          get { return ghostCagePosition.Y; }
      }
    /// <summary>
    /// Position originale du pacman
    /// </summary>
      private Vector2i pacmanStartingPosition;
    /// <summary>
    /// Accesseur du numéro de la ligne où se trouve le pacman au début
    /// Propriété c#
    /// </summary>
      public int PacmanStartingPositionX
      {
          get { return pacmanStartingPosition.X; }
      }
    /// <summary>
    /// Accesseur du numéro de la colonne où se trouve le pacman au début
    /// Propriété C#
    /// </summary>
      public int PacmanStartingPositionY
      {
          get { return pacmanStartingPosition.Y; }
      }
    /// <summary>
    /// Accesseur de la hauteur
    /// Propriété C#
    /// </summary>
      public int Height
      {
          get { return tabElements.GetLength(0); }
      }
    /// <summary>
    /// Accesseur de la largeur
    /// Propriété C#
    /// </summary>
      public int Width
      {
          get { return tabElements.GetLength(1); }
      }
      #endregion

      #region Méthodes
      /// <summary>
    /// Constructeur sans paramètre
    /// </summary>
      public Grid()
      {
          tabElements = new PacmanElements[PacmanGame.DEFAULT_GAME_HEIGHT, PacmanGame.DEFAULT_GAME_WIDTH];
          
          pacmanStartingPosition.X = 1;
          pacmanStartingPosition.Y = 1;

          ghostCagePosition.X = PacmanGame.DEFAULT_GAME_WIDTH / 2;
          ghostCagePosition.Y = PacmanGame.DEFAULT_GAME_HEIGHT / 2;
      }



    /// <summary>
    /// Charge un niveau à partir d'une chaine de caractères en mémoire.
    /// Voir l'énoncé du travail pour le format de la chaîne.
    /// </summary>
    /// <param name="content"> Le contenu du niveau en mémoire</param>
    /// <returns>true si le chargement est correct, false sinon</returns>
    public bool LoadFromMemory(string content)
    {
      bool retval = true;
      





      return retval;
    }

    /// <summary>
    /// Retourne l'élément à la position spécifiée
    /// </summary>
    /// <param name="row">La ligne</param>
    /// <param name="column">La colonne</param>
    /// <returns>L'élément à la position spécifiée</returns>
    // A compléter





    /// <summary>
    /// Modifie le contenu du tableau à la position spécifiée
    /// </summary>
    /// <param name="row">La ligne</param>
    /// <param name="column">La colonne</param>
    /// <param name="element">Le nouvel élément à spécifier</param>
    // A compléter


      #endregion

  }
}
