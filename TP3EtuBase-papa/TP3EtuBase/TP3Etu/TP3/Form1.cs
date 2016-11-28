using System;
using System.Drawing;
using System.Windows.Forms;



namespace TP3
{
  public partial class Form1 : Form
  {
    public Form1( )
    {
      InitializeComponent( );
    }

    #region Code fourni
    
    // Représentation visuelles du jeu en mémoire.
    PictureBox[,] toutesImagesVisuelles = null;
    const int nbLignes = 20;
    const int nbColonnes = 10;
    int[] positionYRelative = new int[4];
    int[] positionXRelative = new int[4];
    int colonneCourante;
    int ligneCourante;
    Random rnd = new Random();
    TypeBloc[,] tabLogique = new TypeBloc[nbLignes, nbColonnes];
    TypeBloc bloc;
    Color blocCouleur;
    mouvement deplacement;


    /// <summary>
    /// Gestionnaire de l'événement se produisant lors du premier affichage 
    /// du formulaire principal.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void frmLoad( object sender, EventArgs e )
    {
      // Ne pas oublier de mettre en place les valeurs nécessaires à une partie.
      ExecuterTestsUnitaires();
      InitialiserSurfaceDeJeu(nbLignes,nbColonnes);
      run();
    }

    private void InitialiserSurfaceDeJeu(int nbLignes, int nbCols)
    {
      // Création d'une surface de jeu 10 colonnes x 20 lignes
      toutesImagesVisuelles = new PictureBox[nbLignes, nbCols];
      tableauJeu.Controls.Clear();
      tableauJeu.ColumnCount = toutesImagesVisuelles.GetLength(1);
      tableauJeu.RowCount = toutesImagesVisuelles.GetLength(0);
      for (int i = 0; i < tableauJeu.RowCount; i++)
      {
        tableauJeu.RowStyles[i].Height = tableauJeu.Height / tableauJeu.RowCount;
        for (int j = 0; j < tableauJeu.ColumnCount; j++)
        {
          tableauJeu.ColumnStyles[j].Width = tableauJeu.Width / tableauJeu.ColumnCount;
          // Création dynamique des PictureBox qui contiendront les pièces de jeu
          PictureBox newPictureBox = new PictureBox();
          newPictureBox.Width = tableauJeu.Width / tableauJeu.ColumnCount;
          newPictureBox.Height = tableauJeu.Height / tableauJeu.RowCount;
          newPictureBox.BackColor = Color.Black;
          newPictureBox.Margin = new Padding(0, 0, 0, 0);
          newPictureBox.BorderStyle = BorderStyle.FixedSingle;
          newPictureBox.Dock = DockStyle.Fill;
          
          // Assignation de la représentation visuelle.
          toutesImagesVisuelles[i, j] = newPictureBox;
          // Ajout dynamique du PictureBox créé dans la grille de mise en forme.
          // A noter que l' "origine" du repère dans le tableau est en haut à gauche.
          tableauJeu.Controls.Add(newPictureBox, j, i);
        }
      }
    }
    #endregion
    
    void InitialiserTableau()
    {
      for (int i = 0; i < tabLogique.GetLength(0); i++)
      {
        for (int j = 0; j < tabLogique.GetLength(1); j++)
        {
          tabLogique[i,j] = TypeBloc.None;
        }
      }
    }

    void run()
    {
      InitialiserPartie();
    }
    void InitialiserPartie()
    {
      colonneCourante = tabLogique.GetLength(1) / 2 -1;
      ligneCourante = 0;
      GererTypeBlocs();
      GererCreationBlocDansJeu();
    }
    void MettreAJourPositionBlocDansTabLogique()
    {
      for (int i = 0; i < positionXRelative.Length; i++)
      {
        tabLogique[ligneCourante + positionYRelative[i], colonneCourante + positionXRelative[i]] = bloc;  
      }
    }
    void DetruireBlocCourant()
    {
      for (int i = 0; i < positionXRelative.Length; i++)
      {
        tabLogique[ligneCourante + positionYRelative[i], colonneCourante + positionXRelative[i]] = TypeBloc.None;
      }
    }
    void MettreAJourAffichageCourant()
    {
      for (int i = 0; i < positionXRelative.Length; i++)
      {
        toutesImagesVisuelles[ligneCourante + positionYRelative[i], colonneCourante + positionXRelative[i]].BackColor = Color.Black;
      }
    }
    void AfficherBloc()
    {
      for (int i = 0; i < positionXRelative.Length; i++)
      {
        toutesImagesVisuelles[ligneCourante + positionYRelative[i], colonneCourante + positionXRelative[i]].BackColor = blocCouleur; 
      }
    }
    void GererCouleurBloc()
    {

      //Optimiser sans utiliser une variable.
      if (bloc == TypeBloc.Carre)
      {
        blocCouleur = Color.Yellow;
      }
      else if (bloc == TypeBloc.Ligne)
      {
        blocCouleur = Color.Turquoise;
      }
      else if(bloc == TypeBloc.T)
      {
        blocCouleur = Color.Purple;
      }
      else if(bloc == TypeBloc.L)
      {
        blocCouleur = Color.Orange;
      }
      else if(bloc == TypeBloc.J)
      {
        blocCouleur = Color.Blue;
      }
      else if(bloc == TypeBloc.S)
      {
        blocCouleur = Color.Green;
      }
      else
      {
        blocCouleur = Color.Red;
      }
    }
    void GererCreationBlocDansJeu()
    {
      if (bloc == TypeBloc.Carre)
      {
        positionYRelative[0] = 0;
        positionYRelative[1] = 1;
        positionYRelative[2] = 0;
        positionYRelative[3] = 1;
        positionXRelative[0] = 0;
        positionXRelative[1] = 0;
        positionXRelative[2] = 1;
        positionXRelative[3] = 1;
      }
      if (bloc == TypeBloc.Ligne)
      {
        positionYRelative[0] = 0;
        positionYRelative[1] = 0;
        positionYRelative[2] = 0;
        positionYRelative[3] = 0;
        positionXRelative[0] = 0;
        positionXRelative[1] = 1;
        positionXRelative[2] = 2;
        positionXRelative[3] = 3;
      }
      if (bloc == TypeBloc.T)
      {
        positionYRelative[0] = 0;
        positionYRelative[1] = 1;
        positionYRelative[2] = 1;
        positionYRelative[3] = 1;
        positionXRelative[0] = 1;
        positionXRelative[1] = 0;
        positionXRelative[2] = 1;
        positionXRelative[3] = 2;
      }
      if (bloc == TypeBloc.L)
      {
        positionYRelative[0] = 0;
        positionYRelative[1] = 1;
        positionYRelative[2] = 1;
        positionYRelative[3] = 1;
        positionXRelative[0] = 2;
        positionXRelative[1] = 0;
        positionXRelative[2] = 1;
        positionXRelative[3] = 2;
      }
      if (bloc == TypeBloc.J)
      {
        positionYRelative[0] = 0;
        positionYRelative[1] = 0;
        positionYRelative[2] = 0;
        positionYRelative[3] = 1;
        positionXRelative[0] = 0;
        positionXRelative[1] = 1;
        positionXRelative[2] = 2;
        positionXRelative[3] = 2;
      }
      if (bloc == TypeBloc.S)
      {
        positionYRelative[0] = 0;
        positionYRelative[1] = 0;
        positionYRelative[2] = 1;
        positionYRelative[3] = 1;
        positionXRelative[0] = 1;
        positionXRelative[1] = 2;
        positionXRelative[2] = 0;
        positionXRelative[3] = 1;
      }
      if (bloc == TypeBloc.Z)
      {
        positionYRelative[0] = 0;
        positionYRelative[1] = 0;
        positionYRelative[2] = 1;
        positionYRelative[3] = 1;
        positionXRelative[0] = 0;
        positionXRelative[1] = 1;
        positionXRelative[2] = 1;
        positionXRelative[3] = 2;
      }
      MettreAJourPositionBlocDansTabLogique();
      GererCouleurBloc();
      AfficherBloc();
    }
    void GererTypeBlocs()
    {
      bloc = (TypeBloc)rnd.Next(2, 9);  //TypeBloc.Carre; //
    }
    void GererDeplacement()
    {
      if(deplacement == mouvement.Droite)
      {
        DetruireBlocCourant();
        MettreAJourAffichageCourant();
        colonneCourante++;
      }
      else if(deplacement == mouvement.Gauche)
      {
        DetruireBlocCourant();
        MettreAJourAffichageCourant();
        colonneCourante--;
      }
      else if(deplacement == mouvement.Bas)
      {
        DetruireBlocCourant();
        MettreAJourAffichageCourant();
        ligneCourante++;
      }
      else if(deplacement == mouvement.RotationAntihoraire)
      {
        DetruireBlocCourant();
        MettreAJourAffichageCourant();
        int temp = 0;
        for (int i = 0; i < positionXRelative.Length; i++)
        {
          temp = -positionXRelative[i];
          positionXRelative[i] = positionYRelative[i];
          positionYRelative[i] = temp;
        }
      }
      else
      {
        DetruireBlocCourant();
        MettreAJourAffichageCourant();
        int temp = 0;
        for (int i = 0; i < positionXRelative.Length; i++)
        {
          temp = -positionYRelative[i];
          positionYRelative[i] = positionXRelative[i];
          positionXRelative[i] = temp;
        }
      }

      MettreAJourPositionBlocDansTabLogique();
      AfficherBloc();
    }

    #region Code à développer
    /// <summary>
    /// Faites ici les appels requis pour vos tests unitaires.
    /// </summary>
    void ExecuterTestsUnitaires()
    {      
      ExecuterTestABC();
      // A compléter...
    }

    // A renommer et commenter!
    void ExecuterTestABC()
    {
      // Mise en place des données du test
      
      // Exécuter de la méthode à tester
      
      // Validation des résultats
      
      // Clean-up
    }

    #endregion

    private void Form1_KeyPress(object sender, KeyPressEventArgs e)
    {
      deplacement = mouvement.Rien;
      if (e.KeyChar == 's')
      {
        deplacement = mouvement.Bas;
      }
      else if (e.KeyChar == 'd')
      {
        deplacement = mouvement.Droite;
      }
      else if (e.KeyChar == 'a')
      {
        deplacement = mouvement.Gauche;
      }
      else if (e.KeyChar == 'n')
      {
        deplacement = mouvement.RotationAntihoraire;
      }
      else if (e.KeyChar == 'm')
      {
        deplacement = mouvement.RotationHoraire;
      }

      if(deplacement != mouvement.Rien)
      {
        GererDeplacement();
      }
    }
  }
  enum TypeBloc
  {
  None,
  Gele,
  Carre, 
  Ligne, 
  T, 
  L, 
  J, 
  S, 
  Z
  }
  enum mouvement
  {
  Rien,
  RotationAntihoraire,
  RotationHoraire,
  Gauche,
  Droite,
  Bas
  }
}
