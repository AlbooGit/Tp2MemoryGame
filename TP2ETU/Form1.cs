/*
 * Écrire ici la documentation générale. Indiquez quel est le but de votre application.
 * Cet application est un jeu de mémoire créé selon les demandes du TP2. L'utilisateur choisi le nombre de mots qu'il veut rechercher et ensuite le jeu montre ce
 * nombre de mots est affiché dans les divers cartes sur le jeu qui disparaîssent après quelques secondes. Le joueur peut ensuite inscrire les mots qu'il a mémorisé dans une boîte
 * et valider pour voir quels mots étaient manquants puis combien de bonnes réponses il a eut.
 * Indiquez aussi qui est (sont) l' (les) auteur(s).
 * Auteur: Albert Ouellet
 * Co-auteur (aide): Yannick Gibeault, Mike Vezina, Samuel Cloutier, Jade Loupret, François Picard du gr.0002 et Peter McCormick du gr. 0001
 * */

using System;
using System.Drawing;
using System.Windows.Forms;
using TP2ETU.Properties;
using WMPLib;
using ExtentionMethods;
using System.Media;

namespace TP2ETU
{
  public partial class frmMemoryGameMain : Form
  {


    #region Propriétés /  variables partagées par toutes les méthodes.
    Random rnd = new Random();
    int[] indexes = new int[11]; //Tableau des 11 indexes possibles des mots.
    int[] indexesMotsUtilises = new int[] { };  //Tableau contenant les indexes des mots utilisé par le programme dans leurs tableaux.
    string[] motsRecherches = new string[] { }; //Le tableau avec les réponses, les mots choisi par le programme.
    string[] tableauMotsManquant = new string[] { }; //Tableau qui se fait remplir par les mots oubliés de l'utilisateur.
    SoundPlayer musiqueDeFond = new SoundPlayer(Properties.Resources.troubledeconcentration); //Déclaration de la musique utilisée dans les fonctions plus tard

    /// <summary>
    /// Tableau contenant tous les pictures box utilisées. Ce tableau est construit lors du chargement de la
    /// fenêtre (méthode Load)
    /// </summary>
    PictureBox[] tousLesPicturesBox = null;

    /// <summary>
    /// Tableau contenant toutes les images possibles pour le jeu.
    /// </summary>
    Bitmap[] toutesLesImagesAffichees = new Bitmap[] {   Resources.imgHiddenCard,  Resources.chat, Resources.chien,  Resources.lapin,  Resources.furet,  Resources.grenouille, Resources.colibri,  Resources.rat,  Resources.souris, Resources.hamster, Resources.poisson};
    
    /// <summary>
    /// Tableau contenant les mots associés aux images du tableau toutesLesImagesAffichees.
    /// </summary>
    string[] tousLesTextesAssociesAuxImages = new string[] { "Cachée",                 "Blitzcrank",         "Bard",          "Leona",          "Alistar",          "Thresh",         "Braum",          "Taric",          "Ziggs",         "Trundle",         "Gragas" };    
    #endregion
    
    /// <summary>
    /// Appelée au chargement de l'application pour constituer le tableau des picturebox
    /// et initialiser correctement les valeurs
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void frmMemoryGameMain_Load(object sender, EventArgs e)
    {
      // Création du tableau des picturebox
      tousLesPicturesBox = new PictureBox[] 
      {
        pbImg0,pbImg1,pbImg2,pbImg3,pbImg4,pbImg5,pbImg6,pbImg7,
        pbImg8,pbImg9,pbImg10,pbImg11,pbImg12,pbImg13,pbImg14,pbImg15,
        pbImg16,pbImg17,pbImg18,pbImg19,pbImg20,pbImg21,pbImg22,pbImg23,
        pbImg24,pbImg25,pbImg26,pbImg27,pbImg28,pbImg29,pbImg30,pbImg31
      };

      // A COMPLETER AU BESOIN
      
    }
    
    #region Méthodes de gestion des événements et/ou appelées automatiquement
    public frmMemoryGameMain()
    {
      InitializeComponent();
    }



    #endregion

    #region Appels Inutiles
    private void richTextBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {

    }

    private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
    {
   
    }
    
    private void niveauDifficulte_Scroll(object sender, EventArgs e)
    {

    }


    #endregion //Je ne veux pas perdre mon form en effaçant quelque chose par accident.

    #region Méthodes
    /// <summary>
    /// Fonction pour mettre toutes les index du tableau d'indexes à -1 qui signifie non-utilisé.
    /// </summary>
    void InitialiserTableauIndexes()
    {    
      for (int i = 1; i < indexes.Length; i++)
      {
        indexes[i] = -1;
      }
    }
    /// <summary>
    /// Fonction qui prends les mots dont leurs indexes ont été sélectionnés par la méthode ChoisirIndexesNonUtilisés pour les mettre dans un tableau de "bonnes réponses".
    /// </summary>
    void SelectionnerMotsAUtiliser() 
    {
      Array.Resize(ref motsRecherches, (int)numericUpDown1.Value);
      for (int i = 0; i < indexesMotsUtilises.Length; i++)
      {
        motsRecherches[i] = tousLesTextesAssociesAuxImages[indexesMotsUtilises[i]].ToUpper();
      }
    }
    /// <summary>
    /// Fonction qui utilise les numéros d'indexes des mots utilisé et ensuite remplace des PictureBox aléatoires ayant des tags non-utilisés avec la photo qui est associé à l'image.
    /// </summary>
    void AssignerImagesDesMotsAuxPictureBox()
    {
      for (int i = 0; i < indexesMotsUtilises.Length; i++)
      {
        int aleatoire = rnd.Next(0, tousLesPicturesBox.Length);
        if (tousLesPicturesBox[aleatoire].Tag.ToString() == "not used") //Vérifie que l'image est bien non utilisée pour rentrer dans la boucle.
        {
          tousLesPicturesBox[aleatoire].BackgroundImage = toutesLesImagesAffichees[indexesMotsUtilises[i]];
          tousLesPicturesBox[aleatoire].Tag = "used"; //Si l'image se fait utilisé le tag est changé pour utilisé.
        }
        else //Si jamais elle ne rentre pas dans le if, pour ne pas que ce "tour de compteur" disparaisse/ait servi à rien, nous faisons i-- pour refaire l'essai.
        { 
          i--; 
        }
      }
    }
    /// <summary>
    /// Cette fonction s'assure que toutes les images sont bel et bien celle de l'image cachée et comportent le tag non utilisé.
    /// </summary>
    void InitialiserImage() 
    {
      for (int i = 0; i < tousLesPicturesBox.Length; i++)
      {
        tousLesPicturesBox[i].BackgroundImage = toutesLesImagesAffichees[0];
        tousLesPicturesBox[i].Tag = "not used"; //Met un tag "non utilisé" sur chaque picturebox.
      }
    }
    /// <summary>
    /// Cette fonction choisi des indexes qui ne sont pas utilisés (les -1) puis les mets en état utilisé (1). Ensuite il retourne un tableau de int avec quels valeurs sont maintenant utilisés.
    /// </summary>
    /// <returns></returns> tableau de int, les entiers sont les numéros des indexes qui sonts utilisés.
    int[] ChoisirIndexNonUtilise()
    {
      int indexAleatoire = 0; // int utilisé pour un aléatoire
      Array.Resize(ref indexesMotsUtilises, (int)numericUpDown1.Value); //Le tableau n'a pas de grandeur alors ceci change sa grandeur selon le nombre de mots choisi par l'utilsateur.
      bool nouvelIndex = false; //
      for (int compteur = 0; compteur < numericUpDown1.Value; compteur++)
      {
        nouvelIndex = false; 
        while (!nouvelIndex) 
        {
          indexAleatoire = rnd.Next(1, indexes.Length);
          if (indexes[indexAleatoire] == -1)
          {
            indexes[indexAleatoire] = 1;
            nouvelIndex = true;
            indexesMotsUtilises[compteur] = indexAleatoire ;
          }
        }
      }
      return indexesMotsUtilises; 
    }
    /// <summary>
    /// Cette fonction change les images de tous les PicturesBox à celle de l'image cachée.
    /// </summary>
    void MasquerMots() 
    {
      for (int i = 0; i < tousLesPicturesBox.Length; i++)
      {
        tousLesPicturesBox[i].BackgroundImage = toutesLesImagesAffichees[0]; //L'image 0 c'est celui de l'image caché. 
      }
    }
    /// <summary>
    /// Cette fonction vérifie quelles réponses sont bonnes en comparant le tableau des réponses de l'utilisateur avec celui des bonnes réponses de la partie. Elle affiche un compteur des bonnes réponses dans un label de la boîte résultats et met les mots que l'utilisateur n'a pas trouvé dans un tableau de strings. 
    /// </summary>
    /// <param name="reponsesJoueur"></param> Un tableau contenant les réponses fourni par l'utilisateur.
    void ValiderRéponses(string[] reponsesJoueur) 
    { 
      tableauMotsManquant = new string[(int)numericUpDown1.Value];
      int compteur1 = 0; //compteur utilisé dans le if pour qu'il se déroule correctement
      int compteurBonnesReponses = 0; //augmente avec chaque bon mot
      for (int i = 0; i < motsRecherches.Length; i++)
      {
      bool ajouterMotALaListeMotsManquants = true; //Un booléen pour que le if se déroule bien.
        for (int j = 0; j < reponsesJoueur.Length; j++)
        {
          if (motsRecherches[i].ToUpper() == reponsesJoueur[j].ToUpper())
          {
            compteurBonnesReponses++;
            ajouterMotALaListeMotsManquants = false;
          }
        }
        if (ajouterMotALaListeMotsManquants)
          {
            tableauMotsManquant[compteur1] = motsRecherches[i];
            compteur1++;
          }
      }
      if (compteurBonnesReponses == 1) //Afficher un pluriel si jamais plus qu'une bonne réponse
      {
        labelMotsReussi.Text = string.Format("Vous avez trouvé {0} bonne réponse.", compteurBonnesReponses);
      }
      else 
      {
        labelMotsReussi.Text = string.Format("Vous avez trouvé {0} bonnes réponses.", compteurBonnesReponses);
      }
      if (compteurBonnesReponses == motsRecherches.Length)
      {
          MessageBox.Show("Bravo vous avez une partie parfaite!");
      }
    }
    /// <summary>
    /// Cette fonction utilise le tableau contenant les mots qui manquaient et les affiche dans un label de la boîte résultats.
    /// </summary>
    void AfficherMotsManquants() 
    {
      labelMotsManquants.Text = "Les mots manquant sont:"; //le texte de base

      for (int i = 0; i < tableauMotsManquant.Length; i++)
      {
        if (tableauMotsManquant[i] != null)
        {
          labelMotsManquants.Text = labelMotsManquants.Text + " " + tableauMotsManquant[i].ToLower().Capitalize(); //Ajouter les mots manquants au label.
        }
      }
    }
    /// <summary>
    /// Cette fonction vérifie s'il existe des doublons dans le tableau des réponses du joueur. S'il en existe elle les change en cases vide. 
    /// </summary>
    /// <returns></returns>
    string[] ExtraireElementsUniques() 
    {
      string[] reponsesJoueur = new string[motsRecherches.Length];
      reponsesJoueur = textBox1.Text.ToUpper().Split(' ');

      for (int i = 0; i < reponsesJoueur.Length; i++)
      {
        for (int j = 0; j < reponsesJoueur.Length; j++)
        {
          if (i != j)
          {
            if (reponsesJoueur[i] == reponsesJoueur[j]) 
            {
               reponsesJoueur[j] = String.Empty;  
            }
          }
        }
      }
      return reponsesJoueur;
    }
    /// <summary>
    /// Cette fonction "redémarre" le jeu en mettant des paramètres similaires au départ.
    /// </summary>
    void ReinitialiserPartie()
    {
      musiqueDeFond.Stop();
      textBox1.Enabled = false; //Le textbox ne doit pas être disponible au départ.
      startGame.Enabled = true; //Le bouton startGame doit être disponible au départ
      boutonValider.Enabled = false; //Le bouton valider ne doit pas être disponible au départ
    }
    #endregion

    #region Événements par le programme
    /// <summary>
    /// Le bouton valider appelle les fonctions qui valide les réponses, affiches les résultats, change l'état de certains boutons et arrête la musique.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void boutonValider_Click(object sender, EventArgs e)
    {
      string[] reponsesJoueur = ExtraireElementsUniques(); //tableau qui se fait donner les réponses du joueur sans doublons.
      ValiderRéponses(reponsesJoueur);
      AfficherMotsManquants();
      textBox1.Enabled = false; //On empêche l'utilisateur de ré-écrire dans la boîte après validation.
      boutonValider.Enabled = false; //Le bouton valider se disable pour ne pas pouvoir le réutiliser.
      startGame.Enabled = true;
      musiqueDeFond.Stop();
    }
    /// <summary>
    /// Cette fonction est le click du bouton startgame qui appelle les fonctions des initialisations pour le jeu, la sélection des mots, change l'état de certain boutons et débute la musique.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void startGame_Click(object sender, EventArgs e)
    {
      InitialiserImage(); //Faire les fonctions d'un début de partie.
      InitialiserTableauIndexes();
      ChoisirIndexNonUtilise();
      SelectionnerMotsAUtiliser();
      AssignerImagesDesMotsAuxPictureBox();
      startGame.Enabled = false; //Le bouton startgame se disable pour ne faire qu'une partie.
      //boutonValider.Enabled = false;
      timer1.Start(); //Débuter le timer avec le bouton
    }
    /// <summary>
    /// Cette fonction permet d'effacer le contenu de la textbox1 lorsque qu'on la sélectionne pour écrire.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void textBox1_Enter(object sender, EventArgs e)
    {
      textBox1.Text = string.Empty; 
    }
    /// <summary>
    /// Cette fonction permet de recommencer une nouvelle partie. Lorsqu'on clique sur le bouton nouvelle partie des menustrips, la fonction efface le contenu des labels résultats, le contenu du texbot, arrête la musique et change l'états de certains boutons et le textbox1.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void nouvellePartieToolStripMenuItem_Click(object sender, EventArgs e)
    {
      
      labelMotsReussi.Text = String.Empty; //Vider le label.
      labelMotsManquants.Text = String.Empty; //Vider le label des réponses possibles.
      textBox1.Text = "Entrez texte ici."; //Le texte initial du textbox1.
      textBox1.Enabled = false; //On ne doit pas pouvoir écrire au début d'une nouvelle partie.
      startGame.Enabled = true; //On doit pouvoir débuter au début d'une nouvelle partie.
      boutonValider.Enabled = false; //On ne doit pas pouvoir valider au début d'une partie.
      musiqueDeFond.Stop(); 
    }
    /// <summary>
    /// Cette fonction a été rajoutée pour s'assurer que lorsqu'on quitte le programme il se ferme complètement et correctement. Mit en place par Peter McCormick Gr.0001
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void frmMemoryGameMain_FormClosed(object sender, FormClosedEventArgs e)
    {
      Application.Exit();
    }
    /// <summary>
    /// Cette fonction permet d'activer la musique lorsque le bouton de menustrip est "clické". 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void musiqueToolStripMenuItem_Click(object sender, EventArgs e)
    {
      musiqueDeFond.PlayLooping();
    }
    /// <summary>
    /// Cette fonction permet d'appeler la fonction masquer mots qui change tous les images en images cachés et active le bouton valider et la textbot quand le timer1 a terminé.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void timer1_Tick(object sender, EventArgs e)
    {
      MasquerMots();
      boutonValider.Enabled = true; //On doit pouvoir valider après avoir vu les cartes.
      textBox1.Enabled = true; //On doit pouvoir écrire après que les cartes disparaissent.
    }
    /// <summary>
    /// Cette fonction change le temps que prends le timer pour terminer lorsqu'on clique sur le menustrip de 5 secondes.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void secondesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      timer1.Interval = 5000;
    }
    /// <summary>
    /// Cette fonction change le temps que prends le timer pour terminer lorsqu'on clique sur le menustrip de 4 secondes.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void secondesToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      timer1.Interval = 4000;
    }
    /// <summary>
    /// Cette fonction change le temps que prends le timer pour terminer lorsqu'on clique sur le menustrip de 3 secondes.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void secondesToolStripMenuItem2_Click(object sender, EventArgs e)
    {
      timer1.Interval = 3000;
    }
    /// <summary>
    /// Cette fonction change le temps que prends le timer pour terminer lorsqu'on clique sur le menustrip de 2 secondes.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void secondesToolStripMenuItem3_Click(object sender, EventArgs e)
    {
      timer1.Interval = 2000;
    }
    /// <summary>
    /// Cette fonction change le temps que prends le timer pour terminer lorsqu'on clique sur le menustrip de 1 seconde.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void secondeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      timer1.Interval = 1000;
    }
    /// <summary>
    /// La fonction permet que lorsqu'on clique sur le bouton de menu quitter, l'application ce ferme.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Application.Exit();   
    }

    #endregion

    private void button1_Click(object sender, EventArgs e)
    {
      
    }
  }
}

