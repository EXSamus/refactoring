using System;
using System.Threading;
namespace refactoring
{
    unsafe public class Circuit
    {
        Secteur secteur = new Secteur();
        Mutex sem = new Mutex();
        public void simu(int id, int idVoiture, int[][] classement, Mutex sem)
        {
            //initialisation des variables

            Random rand = new Random();
            Circuit circuit = new Circuit();
            Console.WriteLine("initialisation du sémaphore");

            Console.WriteLine("initialisation de la voiture " + id);
            Voiture voiture = new Voiture(id, idVoiture, 2, 0);
            Console.WriteLine("lancement de la course");
            Course(7, voiture, sem, classement);
        }

        int tour(Voiture maVoiture, int numeroTour, Mutex sem, int[][] classement)
        {

            int total = 0;                    //temps total
            int s = 0;                      //temps pour un secteur
            int i = 1;
            while (i <= 3)
            {

                s = secteur.secteur(100, 250, 49);
                Thread.Sleep(s * 10);       // endormir le processus pendant s*10 milliseconde

                if (i == 1)
                {
                    refreshSecteurs(maVoiture);
                }

                if (s == 0)
                {                 //test si il y a un crash
                    maVoiture.Status = 0;
                    maVoiture.crash = true;
                    maVoiture.meilleurTemps = int.MaxValue;
                    maVoiture.tempsTotal = int.MaxValue;
                    maVoiture.changeOrdre = true;
                    refreshSecteurs(maVoiture);
                    return 0;
                }
                if ((i % 2) == 0)
                {                   //si il passe dans le secteur 2
                    maVoiture.tempSecteur2 = s;

                }
                else if ((i % 3) == 0)
                {              //si il passe dans le secteur 3
                    s += 150;

                    maVoiture.Status = 1;
                    maVoiture.passageAuStand = 1;
                    maVoiture.tours += 1;
                    Thread.Sleep(s * 10);                  // endormir le processus pendant s*10 milliseconde

                    maVoiture.Status = 2;
                    maVoiture.tempSecteur3 = s;
                }
                else
                {
                    maVoiture.tempSecteur1 = s;   //si il passe dans le secteur 1
                }
                total += s;                       //ajout au temps total de la voiture dans le circuit
                i++;
            }
            maVoiture.tempsTotal += total;
            int[] tabTemp = { maVoiture.Id, maVoiture.tours, maVoiture.tempSecteur1, maVoiture.tempSecteur2,
                            maVoiture.tempSecteur3, maVoiture.Status, maVoiture.tempsTotal, maVoiture.meilleurTemps, maVoiture.idVoiture};
            sem.WaitOne();
            classement[maVoiture.Id] = tabTemp;
            sem.ReleaseMutex();
            return total;
        }
        public void essaiLibreQuali(int chrono, Voiture maVoiture, Mutex sem, int[][] classement)
        {

            int temps1 = 0;
            int temps2 = 0;
            maVoiture.tours += 1;
            do
            {
                temps1 = tour(maVoiture, maVoiture.tours, sem, classement);
                temps2 += temps1;

                if (temps1 == 0)
                {
                    maVoiture.Ready = -1;
                    return;
                }
                maVoiture.tours += 1;

                //verifie si la voiture a fait un meilleur temps que ce qu'elle avait precedemment fait
                if (maVoiture.meilleurTemps > temps1 || maVoiture.meilleurTemps == 0)
                {
                    maVoiture.meilleurTemps = temps1;        //sauvegarde la valeur en memoire partagee
                    if (!maVoiture.changeOrdre)
                    {
                        maVoiture.changeOrdre = true;          //indique que le temps de la voiture a changeOrdre
                    }
                }

                Console.WriteLine("\ntemps du tour: " + temps1 +"| Voiture : "+ maVoiture.idVoiture);
                if (maVoiture.tours > 2500)
                {
                    classement[maVoiture.Id][5] = 0;
                }
                
            } while (temps2 < chrono && temps1 != 0);
            maVoiture.Ready = -1;
            classement[maVoiture.Id][5] = 0;
        }






        /** simule un tour dans un circuit pour la course principale
        *
        * @param voiture* mavoiture pointeur vers l'emplacement memoire de la voiture
        *                           simulee par le processus
        * @param int numeroTour     permet de savoir a quel tour on est
        * @param int tourMax        nombre de tour max de la course
        * @param sem_t* sem         semaphore de la voiture permettant de garder les
        *                           zones d'ecriture a risque
        *
        * @return int               retourne le temps total qu'a pris la voiture pour faire
        *                           un tour
        */
        int tourCourse(Voiture maVoiture, int numeroTour, int tourMax, Mutex sem, int[][] classement)
        {

            int total = 0;                    //temps total
            int s = 0;                      //temps pour un secteur
            int i = 1;
            //Console.WriteLine("\ndébut secteur");
            while (i <= 3)
            {

                s = secteur.secteur(100, 250, 49);
                Thread.Sleep(s * 10);       // endormir le processus pendant s*10 milliseconde

                if (i == 1)
                {
                    refreshSecteurs(maVoiture);
                }

                if (s == 0)
                {                 //test si il y a un crash
                    maVoiture.Status = 0;
                    maVoiture.crash = true;
                    maVoiture.meilleurTemps = int.MaxValue;
                    maVoiture.tempsTotal = int.MaxValue;
                    maVoiture.changeOrdre = true;
                    refreshSecteurs(maVoiture);
                    return 0;
                }
                if ((i % 2) == 0)
                {                   //si il passe dans le secteur 2
                    maVoiture.tempSecteur2 = s;

                }
                else if ((i % 3) == 0)
                {              //si il passe dans le secteur 3
                    if (secteur.stand() || ((int)(tourMax / numeroTour) == 3 && maVoiture.passageAuStand < 1) || ((int)(tourMax / numeroTour) == 2 && maVoiture.passageAuStand < 2))
                    {
                        s += 150;
                        maVoiture.Status = 1;
                        maVoiture.passageAuStand = 1;
                        maVoiture.tours += 1;
                        Thread.Sleep(s * 10);                  // endormir le processus pendant s*10 milliseconde

                    }
                    maVoiture.Status = 2;
                    maVoiture.tempSecteur3 = s;
                }
                else
                {
                    maVoiture.tempSecteur1 = s;   //si il passe dans le secteur 1
                }
                maVoiture.changeOrdre = true;
                total += s;                       //ajout au temps total de la voiture dans le circuit
                i++;
            }
            maVoiture.tempsTotal += total;
            int[] tabTemp = { maVoiture.Id, maVoiture.tours, maVoiture.tempSecteur1, maVoiture.tempSecteur2,
                            maVoiture.tempSecteur3, maVoiture.Status, maVoiture.tempsTotal, maVoiture.meilleurTemps, maVoiture.idVoiture};
            sem.WaitOne();
            classement[maVoiture.Id] = tabTemp;
            sem.ReleaseMutex();
            return total;
        }

        /** simule le deroulement de l'entierete de la course
        *
        * @param int tours          le nombre de tours que comporte la course
        * @param voiture* mavoiture pointeur vers l'emplacement memoire de la voiture
        *                           simulee par le processus
        * @param Mutex sem mutex qui permet de synchroniser les données
        *
        */
        public void Course(int tours, Voiture maVoiture, Mutex sem, int[][] classement)
        {

            int temps1 = 0;
            int temps2 = -1;
            maVoiture.tours += 1;
            do
            {
                //Console.WriteLine("\ntour : " + tours);
                //effectue un tour puis incremente le temps total que la voiture aura passe en course
                temps1 = tourCourse(maVoiture, maVoiture.tours, tours, sem, classement);
                temps2 += temps1;

                if (temps1 == 0)
                {
                    maVoiture.Ready = -1;
                    return;
                }

                maVoiture.tours += 1;

                //verifie si la voiture a fait un meilleur temps que ce qu'elle avait precedemment fait
                if (maVoiture.meilleurTemps > temps1 || maVoiture.meilleurTemps == 0)
                {
                    maVoiture.meilleurTemps = temps1;        //sauvegarde la valeur en memoire partagee
                    if (!maVoiture.changeOrdre)
                    {
                        maVoiture.changeOrdre = true;          //indique que le temps de la voiture a changeOrdre
                    }
                }
                //Console.WriteLine("\ntemps du tour: " + temps1 +"| Voiture : "+ maVoiture.Id);
                if (maVoiture.tours > tours)
                {
                    classement[maVoiture.Id][5] = 0;
                }
            } while (maVoiture.tours < tours && temps1 != 0 || maVoiture.changeOrdre);
            maVoiture.Ready = -1;
            classement[maVoiture.Id][5] = 0;
        }
        /** remets les secteurs de la voiture a zero.  Cela permet de simuler la fin d'un tour
        *   sur le circuit
        *
        * @param Voiture maVoiture remet à zero les temps de secteur de la voiture
        *
        */
        void refreshSecteurs(Voiture maVoiture)
        {
            maVoiture.tempSecteur1 = 0;
            maVoiture.tempSecteur2 = 0;
            maVoiture.tempSecteur3 = 0;
        }
    }
}