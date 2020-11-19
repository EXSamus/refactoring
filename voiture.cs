using System;
/*
*
* Classe pour l'objet Voiture
*
*/

namespace refactoring
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
        *   changerOrdre == false le temps n'a pas ete mis a jour
        *   changerOrdre == true le temps a ete mis à jour
        */
        public bool changeOrdre { get; set; }
        /* permet de savoir si la voiture est crashee
        *   crash == false la voiture est toujours en course
        *   crash == true la voiture est OUT
        */
        public bool crash { get; set; }
        public int passageAuStand { get; set; }

        
        public Voiture(int id, int IDVoiture, int ready, int status)
        {

            Id = id;//numero de la voiture

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
