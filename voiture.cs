using System;
/*
*
* Ce fichier .c est compile puis lance lorsqu'un processus fils est cree
* il fait appel a des headers pour utiliser les fonctions de voitures, du circuits, ...
* il simule une course selon les parametres entree avec une voiture
*
*/

namespace voiture
{
    
    //Objet Voiture
    public class Voiture
    {
        public int Id, Status, Ready, idVoiture;
        public int tempSecteur1 { get; set; }
        public int tempSecteur2 { get; set; }
        public int tempSecteur3 { get; set; }

        public int meilleurTemps { get; set; }  //meilleur temps pour un tour complet
        public int tempsTotal { get; set; }     //temps de tous les tours effectue cumule

        public int tours { get; set; }
        /* permet de savoir si le temps totale de la voiture a ete mis à jour => car necessitera une reorganisation du classement
        *   changerOrdre == 0 (FALSE) le temps n'a pas ete mis a jour
        *   changerOrdre == 1 (TRUE) le temps a ete mis à jour
        */
        public bool changeOrdre { get; set; }
        /* permet de savoir si la voiture est crashee
        *   crash == 0 (FALSE) la voiture est toujours en course
        *   crash == 1 (TRUE) la voiture est OUT
        */
        public bool crash { get; set; }
        public int passageAuStand { get; set; }

        
        public Voiture(int id, int IDVoiture, int ready, int status)
        {

            Id = id;		//numero de la voiture

            idVoiture = IDVoiture;
            /*
            status == 0 => le processus est termine
            status == 1 => la voiture est au stand
            status == 2 => la voiture est dans la course !
            */
            Status = status;

            /*
            permet de synchroniser les voitures entre elles (grace au processus main)
            ready == 0 la voiture est en course
            ready == 1 la voiture est prete a partir
            ready == -1 la voiture est en attente
            ready == 2 la voiture ne participe pas a la course
            */
            Ready = ready;
        }
    }
        
}
